Namespace LaGiga
    Public Class UserPlayerController
        Inherits System.Web.Mvc.Controller

        Const pageSize As Integer = 10
        Private _UPService As IUserPlayerService

        Sub New()
            _UPService = New UserPlayerService(New ModelStateWrapper(ModelState))
        End Sub


        Sub New(ByVal UPservice As IUserPlayerService)
            _UPservice = UPservice
        End Sub

        Private UPRepository As New UserPlayerRepository

        <Authorize(Roles:="Administrator")> _
        Function CreateUserPlayer() As ActionResult

            Dim currentComp As Competition = Helpers.CurrentCompetition

            Dim global1 As New UserPlayerStaticClass
            Dim userTeam = _UPService.GetUserGroup(global1.UserTeamID)
            Dim model As New UserPlayerIndexModel With { _
                .selectedUserPlayers = Nothing,
                .PageCount = Nothing,
                .PreviousPage = Nothing,
                .NextPage = Nothing,
                .ClubTeams = _UPService.GetClubTeamsDD(),
                .UserTeamID = userTeam.UserTeamID,
                .UserGroupID = userTeam.UserGroupID,
                .CompetitionID = currentComp.CompetitionID
            }

            Return View(model)
        End Function

        <HttpPost()> _
        Public Function CreateUserPlayer(ByVal UserPlayerToCreate As UserPlayer) As ActionResult
            If (Request.IsAjaxRequest) Then
                If _UPService.CreateUserPlayer(UserPlayerToCreate) And _UPService.CreateWeekUserPlayer(UserPlayerToCreate) Then
                    Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.Success})
                Else
                    Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.IsError})
                End If
            End If

            Return View()
        End Function

        Function GetSecondList(ByVal id As Integer) As ActionResult

            ViewData("SecondListData") = _UPService.getPlayersAtClubsDD(id)

            Return PartialView("SecondList")
        End Function
        
    End Class
End Namespace