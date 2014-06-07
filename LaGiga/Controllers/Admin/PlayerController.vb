Namespace LaGiga



    Public Class PlayerController
        Inherits System.Web.Mvc.Controller

        Const pageSize As Integer = 11
        Private _service As IPlayerService

        Sub New()
            _service = New PlayerService(New ModelStateWrapper(ModelState))
        End Sub


        Sub New(ByVal service As IPlayerService)
            _service = service
        End Sub

        <Authorize(Roles:="Administrator")> _
        Function PlayerIndex(ByVal TeamID As Integer?, ByVal PositionID As Integer?, ByVal pageIndex As Integer?, Optional ByVal searchValue As String = "") As ActionResult

            Dim lselectedPlayers As IEnumerable(Of Player) = Nothing
            Dim iPagecount As Integer = 0
            If TeamID.HasValue And TeamID > 0 Then
                lselectedPlayers = _service.ListPlayerByTeam(TeamID, searchValue, pageSize, pageIndex)
                iPagecount = (_service.CountPlayerByTeam(TeamID, searchValue) / pageSize) + 0.5
                iPagecount = Math.Floor(iPagecount)
            End If

            If PositionID.HasValue And PositionID > 0 Then
                lselectedPlayers = _service.ListPlayer(PositionID, searchValue, pageSize, pageIndex)
                iPagecount = _service.PlayerCount(PositionID, searchValue) / pageSize + 0.5
                iPagecount = Math.Floor(iPagecount)
            End If

            If lselectedPlayers Is Nothing And iPagecount = 0 Then
                lselectedPlayers = _service.ListPlayer(pageSize, pageIndex, searchValue)
                iPagecount = _service.PlayerCount(searchValue) / pageSize + 0.5
                iPagecount = Math.Floor(iPagecount)
            End If


            Dim model As New PlayerIndexModel With { _
                .positionTypes = _service.ListPositions().ToDictionary(Function(o) [Enum].GetName(GetType(Helpers.Position), o)),
                .ClubTeams = _service.GetClubTeams(),
                .selectedPlayers = lselectedPlayers,
                .SelectedPosition = PositionID,
                .selectedTeam = TeamID,
                .PageCount = iPagecount,
                .PreviousPage = IIf(pageIndex > 1, pageIndex - 1, 1),
                .NextPage = IIf(pageIndex < .PageCount, pageIndex + 1, .PageCount)
            }

            If Request.IsAjaxRequest() Then
                Return PartialView("PlayerList", model)
            End If

            Return View("PlayerIndex", model)
        End Function

        <Authorize(Roles:="Administrator")> _
        Function Details(ByVal PlayerID As Integer) As ActionResult
            Return View()
        End Function

        '
        ' GET: /Player/Create
        <Authorize(Roles:="Administrator")> _
        Function Create() As ActionResult
            ViewData("Positions") = _service.ListPositionsDD()
            ViewData("NationalTeams") = _service.GetNationalTeams()
            ViewData("ClubTeams") = _service.GetClubTeamsCreate()
            Return View()
        End Function

        '
        ' POST: /Player/Create

        <HttpPost()> _
        Function Create(ByVal PlayerToCreate As Player, ByVal Positions As Integer, Optional ByVal ClubTeams As Integer = 0, Optional ByVal NationalTeams As Integer = 0) As ActionResult
            PlayerToCreate.PositionID = Positions
            PlayerToCreate.NationalTeamID = NationalTeams
            PlayerToCreate.TeamID = ClubTeams
            If (Request.IsAjaxRequest) Then
                If _service.CreatePlayer(PlayerToCreate) Then
                    Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.Success})
                Else
                    Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.IsError})
                End If
            End If

            Return RedirectToAction("TeamIndex")
        End Function

        <Authorize(Roles:="Administrator")> _
        Function Edit(ByVal id As Integer) As ActionResult
            Dim model As New PlayerTransferIndexModel With {
                .Player = _service.GetPlayer(id)
            }

            Return View(model)
        End Function

        <HttpPost()> _
        Function Save(ByVal PlayerToEdit As PlayerTransferIndexModel) As ActionResult
            If _service.EditPlayer(PlayerToEdit.Player) Then
                Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.Success})
            Else
                Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.IsError})
            End If
            Return View()
        End Function

        <HttpPost()> _
        Function CreateTransfer(ByVal TransferToCreate As Transfer, ByVal NewTeamID As Integer) As ActionResult
            If TransferToCreate.FromDate.Equals(Date.MinValue) Then
                TransferToCreate.FromDate = Now.Date
            End If

            Dim player As Player = _service.GetPlayer(TransferToCreate.PlayerID)
            player.TeamID = NewTeamID

            Dim model As New PlayerTransferIndexModel With {
                .Player = player
            }

            If _service.CreateTransfer(TransferToCreate, NewTeamID) Then
                model.Succeeded = True
                model.SuccessMessage = "Transfer made successfully"
                Return View("Edit", model)
            End If
            Return View("Edit", model)
        End Function

        '
        ' GET: /Player/Delete/5
        <Authorize(Roles:="Administrator")> _
        Function Delete(ByVal id As Integer) As ActionResult
            Return View(_service.GetPlayer(id))
        End Function

        <AcceptVerbs(HttpVerbs.Delete), ActionName("Delete")> _
        Public Function AjaxDelete(ByVal PlayerID As Integer, ByVal PositionID As Integer?, ByVal pageIndex As Integer?, Optional ByVal searchValue As String = "") As ActionResult
            Dim PlayerToDelete = _service.GetPlayer(PlayerID)

            ' Delete from database
            _service.DeletePlayer(PlayerToDelete)

            Dim model As New PlayerIndexModel With { _
                .positionTypes = _service.ListPositions().ToDictionary(Function(o) [Enum].GetName(GetType(Helpers.Position), o)),
                .selectedPlayers = IIf((PositionID.HasValue And PositionID > 0),
                                    _service.ListPlayer(PositionID, searchValue, pageSize, pageIndex),
                                    _service.ListPlayer(pageSize, pageIndex, searchValue)),
                .SelectedPosition = PositionID,
                .PageCount = IIf(PositionID.HasValue And PositionID > 0,
                                    Math.Floor((_service.PlayerCount(PositionID, searchValue) / pageSize) + 1),
                                    Math.Floor((_service.PlayerCount(searchValue) / pageSize) + 1)
                                  ),
                .PreviousPage = IIf(pageIndex > 1, pageIndex - 1, 1),
                .NextPage = IIf(pageIndex < .PageCount, pageIndex + 1, .PageCount)
            }


            If Request.IsAjaxRequest() Then
                Return PartialView("PlayerList", model)
            End If

            Return View("PlayerIndex", model)
        End Function

    End Class
End Namespace