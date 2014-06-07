Imports LaGiga.TheCompleteList
Imports LaGiga.TheListServices

Namespace LaGiga
    Public Class TheListController
        Inherits System.Web.Mvc.Controller

        Property _repository As TheListRepository

        Public Sub New()
            _repository = New TheListRepository()
        End Sub

        <HttpGet()>
        Function GetTheList() As ActionResult

            Dim mu As MembershipUser = Membership.GetUser()
            Dim competitionId = Helpers.CurrentCompetition.CompetitionID

            Dim theListPlayerService = New GetPlayersForTheList(competitionId, 1, 0, 0, String.Empty)
            Dim playerList As List(Of TheList) = theListPlayerService.GetPlayersForTheList()

            Dim UserTeamPlayers As New Dictionary(Of Integer, UserPlayer)
            If Not IsNothing(mu) Then
                Dim userTeam = Helpers.GetUserTeamByUserId(mu.ProviderUserKey)

                '' dictionary of playerID, userTeamID
                UserTeamPlayers = _repository.GetUserTeamPlayer(userTeam.UserGroupID)
            End If

            Dim theListAdapter = New TheListModelAdapter(UserTeamPlayers)

            Dim positionsToChoose As New List(Of SelectListItem)
            positionsToChoose.Add(New SelectListItem With {.Text = "All", .Value = 0})
            positionsToChoose.AddRange(Helpers.GetPositions())

            Dim teamsToChoose As New List(Of SelectListItem)
            If Not IsNothing(mu) Then
                teamsToChoose.Add(New SelectListItem With {.Text = "My Team", .Value = -2})
            End If
            teamsToChoose.AddRange(Helpers.GetClubTeamsForOptionList())
            teamsToChoose = teamsToChoose.OrderBy(Function(o) o.Text).ToList

            Dim model As New TheListIndexModel With {
                    .PlayerList = theListAdapter.AdaptTheListDataToModel(playerList),
                    .ClubTeams = teamsToChoose,
                    .Positions = positionsToChoose,
                    .TeamId = 0,
                    .PageCount = Math.Floor((theListPlayerService.TotalActivePlayers / 10) + 0.9),
                    .NextPage = IIf(1 < .PageCount, 1 + 1, .PageCount),
                    .PreviousPage = IIf(1 > 1, 1 - 1, 1),
                    .SelectedPosition = 0,
                    .CurrentPage = 1,
                    .TenPagesLowLimit = 1,
                    .TenPagesHighLimit = 10,
                    .TeamTypeId = Helpers.CurrentCompetition.TeamTypeID,
                    .LaGigaLogon = New LogOnModel,
                    .UserTeamPlayers = UserTeamPlayers,
                    .CompetitionId = competitionId,
                    .CompetitionList = Helpers.GetCompetionList,
                    .IsAuthenticatedUser = IIf(IsNothing(mu), False, True)
                }

            Return View("TheList", model)

        End Function

        <HttpPost()>
        Function GetTheList(ByVal CompetitionId As Integer,
                             ByVal PositionId As Integer,
                             ByVal TeamID As Integer,
                             Optional ByVal SearchText As String = "",
                             Optional ByVal PageIndex As Integer = 0) As ActionResult

            Dim mu As MembershipUser = Membership.GetUser()

            Dim theListPlayerService = New GetPlayersForTheList(CompetitionId, PageIndex, TeamID, PositionId, SearchText)
            Dim playerList As List(Of TheList) = theListPlayerService.GetPlayersForTheList()

            Dim userTeamPlayers As New Dictionary(Of Integer, UserPlayer)

            If Not IsNothing(mu) Then
                Dim userTeam = Helpers.GetUserTeamByUserId(mu.ProviderUserKey)

                '' dictionary of playerID, userTeamID
                userTeamPlayers = _repository.GetUserTeamPlayer(userTeam.UserGroupID)
            End If

            Dim positionsToChoose As New List(Of SelectListItem)
            positionsToChoose.Add(New SelectListItem With {.Text = "All", .Value = 0})
            positionsToChoose.AddRange(Helpers.GetPositions())

            Dim teamsToChoose As New List(Of SelectListItem)
            If Not IsNothing(mu) Then
                teamsToChoose.Add(New SelectListItem With {.Text = "My Team", .Value = -2})
            End If
            teamsToChoose.AddRange(Helpers.GetClubTeamsForOptionList())
            teamsToChoose = teamsToChoose.OrderBy(Function(o) o.Text).ToList

            Dim theListAdapter = New TheListModelAdapter(userTeamPlayers)

            Dim pageCount = Math.Floor((theListPlayerService.TotalActivePlayers / 10) + 0.9)
            Dim paging = New PagingCalculator(PageIndex, pageCount)

            Dim model As New TheListIndexModel With {
                    .PlayerList = theListAdapter.AdaptTheListDataToModel(playerList),
                    .ClubTeams = teamsToChoose,
                    .Positions = positionsToChoose,
                    .TeamId = TeamID,
                    .PageCount = pageCount,
                    .NextPage = IIf(PageIndex < .PageCount, PageIndex + 1, .PageCount),
                    .PreviousPage = IIf(PageIndex > 1, PageIndex - 1, 1),
                    .TenPagesLowLimit = paging.LowPageLimit,
                    .TenPagesHighLimit = paging.HighPageLimit,
                    .SelectedPosition = PositionId,
                    .CurrentPage = PageIndex,
                    .TeamTypeId = Helpers.CurrentCompetition.TeamTypeID,
                    .LaGigaLogon = New LogOnModel,
                    .UserTeamPlayers = userTeamPlayers,
                    .CompetitionId = CompetitionId,
                    .CompetitionList = Helpers.GetCompetionList,
                    .IsAuthenticatedUser = IIf(IsNothing(mu), False, True)
                }

            If Request.IsAjaxRequest Then
                Return PartialView("TheListPlayers", model)
            End If

            Return View("TheList", model)

        End Function

        <HttpGet()>
        Function GetEvents(ByVal playerId As Integer, ByVal competitionId As Integer) As ActionResult

            Dim resultAndEventsModel As List(Of ResultEventModel) = _repository.GetEventInfo(playerId, competitionId)

            Dim player = Helpers.GetPlayerById(playerId)

            Dim model As New PlayerHomeEventIndexModel With {
                .PlayerID = player.PlayerID,
                .PlayerName = player.FirstName & " " & player.Surname,
                .REMS = resultAndEventsModel,
                .CurrentListData = _repository.GetCurrentListDataForPlayer(playerId),
                .CurrentFixture = _repository.GetCurrentFixtureForPlayer(playerId)
                }

            Return PartialView("TheListPlayerEvents", model)
        End Function

        <HttpGet()>
        Function GetPlayerBasicInfo(ByVal playerId As Integer) As ActionResult

            Dim player = Helpers.GetPlayerById(playerId)

            Dim model As New TheListPlayerBasicInfoModel With {
                .PlayerName = player.FirstName & " " & player.Surname,
                .PlayerImageLocation = _repository.FindPlayerImage(player.FirstName & " " & player.Surname)
                }

            Return PartialView("PlayerBasicInfo", model)
        End Function

    End Class
End Namespace