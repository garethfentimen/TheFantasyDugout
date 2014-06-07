<HandleError()> _
Public Class UserTeamController
    Inherits System.Web.Mvc.Controller

    Const pageSize As Integer = 10
    Private _service As IUserTeamService
    Private _UPService As IUserPlayerService
    'set userteam
    Private Global1 As New UserPlayerStaticClass

    Sub New()
        _service = New UserTeamService(New ModelStateWrapper(ModelState))
        _UPService = New UserPlayerService(New ModelStateWrapper(ModelState))
    End Sub


    Sub New(ByVal service As IUserTeamService, ByVal UPservice As IUserPlayerService)
        _service = service
        _UPService = UPservice
    End Sub

    Private UtRepository As New UserTeamRepository

    <Authorize(Roles:="Administrator")> _
    Function UserTeamIndex(ByVal pageIndex As Integer?, Optional ByVal searchValue As String = "") As ActionResult
        Dim model As New UserTeamIndexModel With { _
            .selectedUserTeams = _service.ListUserTeam(pageSize, pageIndex),
            .PageCount = Math.Floor(_service.UserTeamCount() / pageSize) + 0.5,
            .PreviousPage = IIf(pageIndex > 1, pageIndex - 1, 1),
            .NextPage = IIf(pageIndex < .PageCount, pageIndex + 1, .PageCount),
            .RecordCount = _service.UserTeamCount()
        }

        If Request.IsAjaxRequest() Then
            Return PartialView("UserTeamList", model)
        End If

        Return View("UserTeamIndex", model)
    End Function

    '
    ' GET: /UserTeam/Details/5

    <Authorize(Roles:="Administrator")> _
    Function UserPlayerIndex(ByVal UserTeamID As Integer?, ByVal competitionID As Integer?, Optional ByVal pageIndex As Integer = 1, Optional ByVal searchValue As String = "") As ActionResult

        Dim selectedCompetitionID As Integer = 0

        If competitionID.HasValue Then
            selectedCompetitionID = competitionID
        Else
            selectedCompetitionID = Helpers.CurrentCompetition.CompetitionID
        End If

        Dim model As New UserPlayerIndexModel With { _
            .selectedUserPlayers = _UPService.ListUserPlayer(UserTeamID, selectedCompetitionID, pageSize, pageIndex),
            .PageCount = Math.Floor(_UPService.UserPlayerCount(UserTeamID, selectedCompetitionID) / pageSize) + 0.5,
            .PreviousPage = IIf(pageIndex > 1, pageIndex - 1, 1),
            .NextPage = IIf(pageIndex < .PageCount, pageIndex + 1, .PageCount),
            .ClubTeams = _UPService.GetClubTeamsDD(),
            .UserTeam = _service.GetUserTeam(UserTeamID),
            .CompetitionID = Helpers.CurrentCompetition.CompetitionID,
            .CompetitionTypes = _service.GetAllCompetitions(),
            .RecordCount = _UPService.UserPlayerCount(UserTeamID, selectedCompetitionID)
        }

        Global1.UserTeamID = IIf(IsNothing(UserTeamID), 0, UserTeamID)

        If Request.IsAjaxRequest() Then
            Return PartialView("UserPlayerList", model)
        End If

        Return View("UserPlayerIndex", model)
    End Function

    '
    ' GET: /UserTeam/Create
    <Authorize(Roles:="Administrator")> _
    Function Create() As ActionResult
        ViewData("UserGroupDD") = _service.UserGroupDDList()
        Return View()
    End Function

    '
    ' POST: /UserTeam/Create

    <HttpPost()> _
    Function Create(ByVal UserGroupDD As Integer, ByVal UserTeamToCreate As UserTeam) As ActionResult
        UserTeamToCreate.UserGroupID = UserGroupDD
        If (Request.IsAjaxRequest) Then
            If _service.CreateUserTeam(UserTeamToCreate) Then
                Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.Success})
            Else
                Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.IsError})
            End If
        End If

        Return View()
    End Function

    '
    ' GET: /UserTeam/Edit/5

    Function Edit(ByVal id As Integer) As ActionResult
        Return View(New UserTeamEditIndexModel With {.UserTeam = _service.GetUserTeam(id), .Password = False})
    End Function

    '
    ' POST: /UserTeam/Edit/5

    <HttpPost()> _
    Function Save(ByVal rtnUserTeamEdit As UserTeamEditIndexModel) As ActionResult

        Dim msg As String = String.Empty
        If rtnUserTeamEdit.Password Then

            Dim currentUser As MembershipUser = Membership.GetUser(rtnUserTeamEdit.UserTeam.UserId)
            Dim newPassword As String = String.Empty

            Try
                newPassword = currentUser.ResetPassword()
            Catch ex As Exception
                msg = ex.ToString
            End Try

            If String.IsNullOrEmpty(newPassword) Then
                msg += "but password could not be reset"
            Else
                msg = String.Format("and password changed to {0}", newPassword)
            End If

        End If

        If _service.EditUserTeam(rtnUserTeamEdit.UserTeam) Then
            Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.Success, .Message = String.Format("UserTeam Saved successfully {0}", msg)})
        Else
            Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.IsError, .Message = String.Format("There was a problem saving the user team {0}", msg)})
        End If
    End Function

    '
    ' GET: /UserTeam/Delete/5

    Function Delete(ByVal id As Integer) As ActionResult
        Return View(_service.GetUserTeam(id))
    End Function

    '
    ' POST: /UserTeam/Delete/5

    <Authorize(Roles:="Administrator")> _
    <AcceptVerbs(HttpVerbs.Delete), ActionName("Delete")> _
    Public Function AjaxDelete(ByVal id As Integer?, ByVal competitionID As Integer?, ByVal typeID As Integer?, ByVal pageIndex As Integer?) As ActionResult

        If typeID = 2 Then
            Dim UserPlayerToDelete = _UPService.GetUserPlayer(id)

            ' Delete from database
            _UPService.DeleteUserPlayer(UserPlayerToDelete)

            Dim model As New UserPlayerIndexModel With { _
                .selectedUserPlayers = _UPService.ListUserPlayer(Global1.UserTeamID, Helpers.CurrentCompetition.CompetitionID, pageSize, pageIndex),
                .PageCount = Math.Floor(_UPService.UserPlayerCount(Global1.UserTeamID, competitionID) / pageSize) + 0.5,
                .PreviousPage = IIf(pageIndex > 1, pageIndex - 1, 1),
                .NextPage = IIf(pageIndex < .PageCount, pageIndex + 1, .PageCount),
                .ClubTeams = _UPService.GetClubTeamsDD(),
                .UserTeam = _service.GetUserTeam(Global1.UserTeamID),
                .CompetitionID = competitionID,
                .CompetitionTypes = _service.GetAllCompetitions,
                .RecordCount = _UPService.UserPlayerCount(Global1.UserTeamID, competitionID)
            }

            If Request.IsAjaxRequest() Then
                Return PartialView("UserPlayerList", model)
            End If

            Return View("UserPlayerIndex", model)
        Else


            Dim UserTeamToDelete = _service.GetUserTeam(id)

            ' Delete from database
            _service.DeleteUserTeam(UserTeamToDelete)

            Dim model As New UserTeamIndexModel With { _
                .selectedUserTeams = _service.ListUserTeam(pageSize, pageIndex),
                .PageCount = Math.Floor(_service.UserTeamCount() / pageSize) + 0.5,
                .PreviousPage = IIf(pageIndex > 1, pageIndex - 1, 1),
                .NextPage = IIf(pageIndex < .PageCount, pageIndex + 1, .PageCount)
            }

            If Request.IsAjaxRequest() Then
                Return PartialView("UserTeamList", model)
            End If

            Return View("UserTeamIndex", model)
        End If
    End Function

    Public Function GetSecondList(ByVal id As Integer) As ActionResult

        ViewData("SecondListData") = _UPService.getPlayersAtClubsDD(id)

        Return PartialView("SecondList")
    End Function

End Class