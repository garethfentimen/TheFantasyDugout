Namespace LaGiga
    Public Class TeamController
        Inherits System.Web.Mvc.Controller

        Const pageSize As Integer = 10
        Private _service As ITeamService

        Sub New()
            _service = New TeamService(New ModelStateWrapper(ModelState))
        End Sub


        Sub New(ByVal service As ITeamService)
            _service = service
        End Sub

        <Authorize(Roles:="Administrator")> _
        Function TeamIndex(ByVal TeamTypeID As Integer?, ByVal pageIndex As Integer?, Optional ByVal searchValue As String = "") As ActionResult

            Dim model As New TeamIndexModel With { _
                .TeamTypes = _service.ListTeamTypes().ToList(),
                .selectedTeams = IIf((TeamTypeID.HasValue And TeamTypeID > 0),
                                    _service.ListTeams1(TeamTypeID, searchValue, pageSize, pageIndex),
                                    _service.ListTeams(pageSize, pageIndex, searchValue)),
                .SelectedTeamType = TeamTypeID,
                .PageCount = IIf(TeamTypeID.HasValue And TeamTypeID > 0,
                                    Math.Floor((_service.TeamsCount(TeamTypeID, searchValue) / pageSize) + 0.5),
                                    Math.Floor((_service.TeamsCount(searchValue) / pageSize) + 0.5)
                                  ),
                .PreviousPage = IIf(pageIndex > 1, pageIndex - 1, 1),
                .NextPage = IIf(pageIndex < .PageCount, pageIndex + 1, .PageCount)
            }

            If Request.IsAjaxRequest() Then
                Return PartialView("TeamList", model)
            End If

            Return View("TeamIndex", model)

        End Function

        <Authorize(Roles:="Administrator")> _
        Function Create() As ActionResult
            ViewData("TeamTypes") = _service.getTeamTypeDDList()
            Return View()
        End Function

        <HttpPost()> _
        Function Create(ByVal TeamTypes As Integer, ByVal TeamToCreate As Team) As ActionResult
            TeamToCreate.TeamTypeID = TeamTypes
            If (Request.IsAjaxRequest) Then
                If _service.CreateTeam(TeamToCreate) Then
                    Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.Success})
                Else
                    Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.IsError})
                End If
            End If

            Return View()
        End Function

        Function Edit(ByVal id As Integer) As ActionResult
            Return View(_service.GetTeam(id))
        End Function

        <HttpPost()> _
        Function Save(ByVal TeamToEdit As Team) As ActionResult
            If _service.EditTeam(TeamToEdit) Then
                Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.Success})
            Else
                Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.IsError})
            End If
            Return View()
        End Function

        <AcceptVerbs(HttpVerbs.Delete), ActionName("Delete")> _
        Public Function AjaxDelete(ByVal id As Integer, ByVal TeamTypeID As Integer?, ByVal pageIndex As Integer?, Optional ByVal searchValue As String = "") As ActionResult
            ' Get contact and group
            Dim TeamToDelete = _service.GetTeam(id)

            ' Delete from database
            _service.DeleteTeam(TeamToDelete)

            Dim model As New TeamIndexModel With { _
                .TeamTypes = _service.ListTeamTypes().ToList(),
                .selectedTeams = IIf((TeamTypeID.HasValue And TeamTypeID > 0),
                                    _service.ListTeams1(TeamTypeID, searchValue, pageSize, pageIndex),
                                    _service.ListTeams(pageSize, pageIndex, searchValue)),
                .SelectedTeamType = TeamTypeID,
                .PageCount = IIf(TeamTypeID.HasValue And TeamTypeID > 0,
                                    Math.Floor((_service.TeamsCount(TeamTypeID, searchValue) / pageSize) + 1),
                                    Math.Floor((_service.TeamsCount(searchValue) / pageSize) + 1)
                                  ),
                .PreviousPage = IIf(pageIndex > 1, pageIndex - 1, 1),
            .NextPage = IIf(pageIndex < .PageCount, pageIndex + 1, .PageCount)
            }

            ' Return Contact List
            If Request.IsAjaxRequest() Then
                Return PartialView("TeamList", model)
            End If

            Return View("TeamIndex", model)
        End Function

    End Class
End Namespace