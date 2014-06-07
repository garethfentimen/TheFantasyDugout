Imports LaGiga.Helpers

Namespace LaGiga
    Public Class PlayerHomeController
        Inherits System.Web.Mvc.Controller

        Private _service As IPlayerHomeService

        Sub New()
            _service = New PlayerHomeService(New ModelStateWrapper(ModelState))
        End Sub

        Sub New(ByVal service As IPlayerHomeService)
            _service = service
        End Sub

        <Authorize(Roles:="Player,Administrator")> _
        Function PlayerHomeIndex() As ActionResult
            Dim Username As String = UCase(Left(User.Identity.Name, 1)) + Right(User.Identity.Name, Len(User.Identity.Name) - 1)
            ViewData("UserName") = Username
            ViewData("Admin") = LCase(User.IsInRole("Administrator"))

            Dim homeModel As New HomeIndexModel With {
                .LaGigaLogon = New LogOnModel
            }

            Return View(homeModel)
        End Function

        <Authorize(Roles:="Player,Administrator")> _
        Function GetPlayerPicker() As ActionResult

            Dim UserTeam As UserTeam = _service.GetUserTeam(User.Identity.Name)

            Dim UserGroup As UserGroup = _service.GetUserGroupByID(UserTeam.UserGroupID)

            If Not IsNothing(UserTeam) Then

                Dim alreadyPickedMessage As String = "You have already picked your team. Please look at this weeks results."

                Dim UserPlayers As List(Of UserPlayer) = _service.GetUserPlayersOrderByPosition(UserTeam.UserTeamID)
                Dim UserPlayerIDs As List(Of Integer) = (From o In UserPlayers Select o.PlayerID).ToList

                Dim WeekUserPlayers As New List(Of WeekUserPlayer)
                Dim sDisableSubmit As String = "Enable"

                Dim draftWeekUserPlayers As List(Of WeekUserPlayer) = _service.GetDraftUserPlayersByWeek(UserTeam.UserTeamID, _service.GetCurrentWeek().WeekID).ToList()
                Dim lastWeekUserPlayers As List(Of WeekUserPlayer) = _service.GetUserPlayersByLastWeekNo(UserTeam.UserTeamID, _service.GetCurrentWeek().WeekNo - 1)
                Dim currentWeekUserPlayers As List(Of WeekUserPlayer) = _service.GetUserPlayersByWeek(UserTeam.UserTeamID, _service.GetCurrentWeek().WeekID).ToList()


                ' This is a mid season draft if there are players from last week and less than the squadsize players have been drafted. 
                ' There also have to be draft players in the squad to indicate drafting.
                If draftWeekUserPlayers.Count < CurrentCompetition.SquadSize AndAlso draftWeekUserPlayers.Count > 0 _
                    AndAlso lastWeekUserPlayers.Count > 0 AndAlso currentWeekUserPlayers.Count = 0 AndAlso UserPlayerIDs.Count = CurrentCompetition.SquadSize Then

                    Dim LastWeekPlayersExist As New List(Of WeekUserPlayer)

                    'Get a list of all players which are still present in userplayers
                    LastWeekPlayersExist = (From o In lastWeekUserPlayers Where UserPlayerIDs.Contains(o.PlayerID)).ToList

                    draftWeekUserPlayers.AddRange(LastWeekPlayersExist)
                    draftWeekUserPlayers = (From o In draftWeekUserPlayers Order By o.Player.PositionID).ToList
                End If

                '' draft takes priority
                If currentWeekUserPlayers.Count = 0 Then
                    sDisableSubmit = "Enable"
                    WeekUserPlayers = draftWeekUserPlayers
                End If

                If draftWeekUserPlayers.Count = 0 AndAlso currentWeekUserPlayers.Count > 0 Then
                    Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.Warning, .Message = alreadyPickedMessage})
                    sDisableSubmit = "Disable"
                    WeekUserPlayers = currentWeekUserPlayers
                End If

                'if there is nothing by this point.. then show last weeks players for picking this week
                If draftWeekUserPlayers.Count = 0 And currentWeekUserPlayers.Count = 0 Then
                    WeekUserPlayers = lastWeekUserPlayers
                    sDisableSubmit = "Enable"
                End If

                '' CHECK this as will change for testing
                If (Now.DayOfWeek = DayOfWeek.Saturday And Now.Hour > 13) _
                    Or Now.DayOfWeek = DayOfWeek.Sunday _
                    Or Now.DayOfWeek = DayOfWeek.Monday _
                    Or Now.DayOfWeek = DayOfWeek.Wednesday Then
                    'Or Now.DayOfWeek = DayOfWeek.Tuesday
                    sDisableSubmit = "Disable"
                End If

                '' add fixtures to top
                Dim weekNumber As Integer = _service.GetCurrentWeek().WeekNo

                Dim oFixtures As List(Of UserFixtureCalculation) = _service.getAllfixtures(Helpers.CalculateWeekNumber(UserGroup, weekNumber), UserGroup)

                '' need to populate a specific player picker form set of objects 
                Dim playerPickerForm As New List(Of PlayerPickerForm)

                '' if we have the weekuserplayers check for and add last weeks status
                If Not IsNothing(WeekUserPlayers) AndAlso WeekUserPlayers.Count > 0 Then

                    For Each WeekUserPlayer As WeekUserPlayer In WeekUserPlayers

                        Dim statusListItem As New List(Of SelectListItem)

                        If WeekUserPlayer.StatusID = PlayingType.Playing Then
                            statusListItem.Add(New SelectListItem With {.Value = PlayingType.Playing, .Text = "1st Team (Playing)", .Selected = True})
                            statusListItem.Add(New SelectListItem With {.Value = PlayingType.Squad, .Text = "Squad Member", .Selected = False})
                        Else
                            statusListItem.Add(New SelectListItem With {.Value = PlayingType.Squad, .Text = "Squad Member", .Selected = True})
                            statusListItem.Add(New SelectListItem With {.Value = PlayingType.Playing, .Text = "1st Team (Playing)", .Selected = False})
                        End If

                        playerPickerForm.Add(New PlayerPickerForm With {.status = statusListItem, .weekUserPlayer = WeekUserPlayer})
                    Next

                Else
                    Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.Warning, .Message = alreadyPickedMessage})
                End If

                Dim model As New PickPlayerIndexModel With {
                        .Week = _service.GetCurrentWeek(),
                        .UserTeam = UserTeam,
                        .FormPlayers = playerPickerForm,
                        .sDisabled = sDisableSubmit,
                        .Fixtures = oFixtures
                    }

                Return PartialView("PlayerPicker", model)

            Else
                Dim noDraftMsg As String = "There has been an error because you do not yet have a group administrator who can draft your team. Please contact an administrator to nominate one."
                Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.IsError, .Message = noDraftMsg})

            End If

        End Function

        <Authorize(Roles:="Player,Administrator")> _
        Function GetResultTable() As ActionResult

            Dim UserTeam = _service.GetUserTeam(User.Identity.Name)
            Dim stotalScore As String

            If Not IsNothing(UserTeam) Then
                Dim oResultCollection As New List(Of Result)
                oResultCollection = _service.getUserResultTable(UserTeam.UserTeamID)

                If oResultCollection Is Nothing Or oResultCollection.Count = 0 Then
                    stotalScore = "You have not picked your team or the results are not yet available"
                Else
                    stotalScore = oResultCollection.First.TotalScore().ToString
                End If

                Dim model As New PlayerHomeIndexModel With {
                    .userTeamID = UserTeam.UserTeamID,
                    .UserName = UCase(Left(User.Identity.Name, 1)) + Right(User.Identity.Name, Len(User.Identity.Name) - 1),
                    .Weekname = _service.GetCurrentWeek().WeekName,
                    .Results = oResultCollection,
                    .TotalScore = stotalScore
                    }

                Return PartialView("WeekResults", model)

            Else
                Return PartialView("UserTeamError")
            End If

        End Function

        <Authorize(Roles:="Player,Administrator")> _
        Function GetSeasonResults() As ActionResult

            Dim UserTeam As UserTeam = _service.GetUserTeam(User.Identity.Name)
            Dim userGroup As UserGroup = _service.GetUserGroupByID(UserTeam.UserGroupID)
            If Not IsNothing(UserTeam) Then
                'create own user friendly class
                Dim model As New SeasonResultIndexModel With {
                        .SeasonResult = _service.GetSeasonResult(userGroup)
                    }

                Return PartialView("SeasonResults", model)
            Else
                Return PartialView("UserTeamError")
            End If
        End Function

        <Authorize(Roles:="Player,Administrator")> _
        Function GetOpposition() As ActionResult

            Dim UserTeam As UserTeam = _service.GetUserTeam(User.Identity.Name)

            Dim UserGroup As UserGroup = _service.GetUserGroupByID(UserTeam.UserGroupID)

            If Not IsNothing(UserTeam) Then
                Dim db As New LaGigaClassesDataContext
                Dim weekNumber As Integer = _service.GetCurrentWeek().WeekNo

                Dim oppositionUserTeamId As Integer

                Dim oOppositionUserTeam As UserFixtureCalculation = (From c In db.UserFixtureCalculations Where c.UserTeamIDA = UserTeam.UserTeamID _
                                           And c.WeekNo = Helpers.CalculateWeekNumber(UserGroup, weekNumber) AndAlso c.NoPlayers.Equals(UserGroup.NoPlayers)).FirstOrDefault()

                If IsNothing(oOppositionUserTeam) Then
                    oOppositionUserTeam = (From c In db.UserFixtureCalculations Where c.UserTeamIDB = UserTeam.UserTeamID _
                                           And c.WeekNo = Helpers.CalculateWeekNumber(UserGroup, weekNumber) AndAlso c.NoPlayers.Equals(UserGroup.NoPlayers)).FirstOrDefault()

                    oppositionUserTeamId = oOppositionUserTeam.UserTeamIDA
                Else
                    oppositionUserTeamId = oOppositionUserTeam.UserTeamIDB
                End If

                Dim stotalScore As String

                Dim oResultCollection As List(Of Result)
                oResultCollection = _service.getUserResultTable(oppositionUserTeamId)

                If oResultCollection Is Nothing Or oResultCollection.Count() = 0 Then
                    stotalScore = "You have not picked your team or the results are not yet available"
                Else
                    stotalScore = oResultCollection.First.TotalScore().ToString
                End If

                Dim model As New PlayerHomeIndexModel With {
                    .userTeamID = UserTeam.UserTeamID,
                    .UserName = _service.GetUserTeamByID(oppositionUserTeamId).Name,
                    .Weekname = _service.GetCurrentWeek().WeekName,
                    .Results = oResultCollection,
                    .TotalScore = stotalScore
                    }

                Return PartialView("OppositionResults", model)
            Else
                Return PartialView("UserTeamError")
            End If
        End Function

        <Authorize(Roles:="Player,Administrator")> _
        Function CreateWeekUserPlayers(ByVal PlayerIDs As ICollection(Of String), ByVal StatusIDs As ICollection(Of String)) As ActionResult

            Dim mu As MembershipUser = Membership.GetUser

            Dim userteam As UserTeam = Helpers.GetUserTeamByUserId(mu.ProviderUserKey)

            Dim week As Week = _service.GetCurrentWeek()

            Dim WeekUserPlayers As New List(Of WeekUserPlayer)

            If (Request.IsAjaxRequest) Then

                For i As Integer = 0 To PlayerIDs.Count - 1

                    Dim playerId As Integer = CInt(PlayerIDs(i))

                    Dim oWUP As New WeekUserPlayer
                    oWUP.PlayerID = CInt(PlayerIDs(i))
                    oWUP.StatusID = CInt(StatusIDs(i))
                    oWUP.WeekID = week.WeekID
                    oWUP.UserTeamID = userteam.UserTeamID
                    oWUP.Draft = False

                    WeekUserPlayers.Add(oWUP)

                Next

                If _service.CreateUserPlayerWeek(WeekUserPlayers) Then
                    Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.Success, .Message = "Your team has been picked successfully."})
                Else
                    Dim msg As String = ModelState.Item("WeekUserPlayer").Errors(0).ErrorMessage
                    Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.IsError, .Message = msg})
                End If
            End If

                Return View()
        End Function

    End Class
End Namespace