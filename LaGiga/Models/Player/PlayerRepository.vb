Imports LaGiga.Helpers

Public Class PlayerRepository
    Implements IPlayerRepository

    Private db As New LaGigaClassesDataContext

    Public Function CreatePlayer(ByVal PlayerToCreate As Player) As Player Implements IPlayerRepository.CreatePlayer
        db.Players.InsertOnSubmit(PlayerToCreate)
        db.SubmitChanges()
        Return PlayerToCreate
    End Function

    Public Function DeletePlayer(ByVal PlayerToDelete As Player) As Player Implements IPlayerRepository.DeletePlayer
        db.Players.DeleteOnSubmit(PlayerToDelete)
        db.SubmitChanges()
        Return PlayerToDelete
    End Function

    Public Function CreateTransfer(ByVal TransferToCreate As Transfer, ByVal NewTeamID As Integer) As Boolean Implements IPlayerRepository.CreateTransfer

        '' Transfer Functionality

        Dim result As New BaseResult
        Try

            '' first check whether there is a current transfer record for this player
            Dim playerTransfer As Transfer = (From o As Transfer In db.Transfers Where o.PlayerID.Equals(TransferToCreate.PlayerID) AndAlso o.TeamID.Equals(TransferToCreate.TeamID) _
                                              AndAlso Not o.ToDate.HasValue).FirstOrDefault

            If IsNothing(playerTransfer) Then
                '' create historical Transfer record for current team
                Dim transferHistorical As New Transfer
                transferHistorical.TransferId = Guid.NewGuid()
                transferHistorical.PlayerID = TransferToCreate.PlayerID
                transferHistorical.TeamID = TransferToCreate.TeamID
                transferHistorical.FromDate = New Date(2010, 8, 1)

                db.Transfers.InsertOnSubmit(transferHistorical)
                db.SubmitChanges()
                CreateTransfer(TransferToCreate, NewTeamID)
            Else
                '' end old record
                playerTransfer.ToDate = TransferToCreate.FromDate
                '' create current transfer record
                TransferToCreate.TransferId = Guid.NewGuid()
                TransferToCreate.TeamID = NewTeamID
                '' update player with new team
                Dim originalPlayer = (From c In db.Players Where c.PlayerID = TransferToCreate.PlayerID).FirstOrDefault()
                originalPlayer.TeamID = NewTeamID
                db.Transfers.InsertOnSubmit(TransferToCreate)
                db.SubmitChanges()
                Return True
            End If
        Catch ex As Exception
            'EventLog.WriteEntry("TheFantasyDugout/PlayerRepository/CreateTransfer(" & TransferToCreate.PlayerID.ToString, "There was an error : " & ex.Message.ToString)
            Return False
        End Try

        Return True
    End Function

    Public Function EditPlayer(ByVal PlayerToEdit As Player) As Player Implements IPlayerRepository.EditPlayer

        Dim originalPlayer = (From c In db.Players Where c.PlayerID = PlayerToEdit.PlayerID).FirstOrDefault()
        originalPlayer.FirstName = PlayerToEdit.FirstName
        originalPlayer.Surname = PlayerToEdit.Surname
        originalPlayer.PositionID = PlayerToEdit.PositionID
        originalPlayer.TeamID = PlayerToEdit.TeamID
        originalPlayer.NationalTeamID = PlayerToEdit.NationalTeamID
        db.SubmitChanges()
        Return PlayerToEdit
    End Function

    Public Function GetPlayer(ByVal id As Integer) As Player Implements IPlayerRepository.GetPlayer
        Return (From c In db.Players Where c.PlayerID = id _
          Select c).FirstOrDefault()
    End Function

    Public Function ListPlayer(ByVal pageSize As Integer?, ByVal pageIndex As Integer?, ByVal searchValue As String) As System.Collections.Generic.IEnumerable(Of Player) Implements IPlayerRepository.ListPlayer

        Dim query = From c In db.Players Where c.Surname.Contains(searchValue) Order By c.Surname
        If pageSize.HasValue And pageIndex.HasValue Then
            Return query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList()
        End If
        Return query.ToList()

    End Function

    Public Function ListPlayer(ByVal PositionID As Integer?, ByVal searchValue As String, ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As System.Collections.Generic.IEnumerable(Of Player) Implements IPlayerRepository.ListPlayer
        Dim query = From c In db.Players Where c.PositionID = PositionID And c.Surname.Contains(searchValue) Order By c.Surname

        If pageSize.HasValue And pageIndex.HasValue Then
            Return query.Skip(Math.Abs((CDec(pageIndex - 1))) * pageSize).Take(pageSize).ToList()
        End If
        Return query.ToList()
    End Function

    Function ListPlayerByTeam(ByVal TeamID As Integer?, ByVal searchValue As String, ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As IEnumerable(Of Player) Implements IPlayerRepository.ListPlayerByTeam
        Dim Query = From c In db.Players Where c.TeamID = TeamID And c.Surname.Contains(searchValue) Order By c.PositionID

        If pageSize.HasValue And pageIndex.HasValue Then
            Return Query.Skip(Math.Abs((CDec(pageIndex - 1))) * pageSize).Take(pageSize).ToList()
        End If

        Return Query.ToList()
    End Function

    Function ListPositions() As IEnumerable(Of Integer) Implements IPlayerRepository.ListPositions
        Return (From o As Integer In [Enum].GetValues(GetType(Position)).Cast(Of Integer)())
    End Function

    Function PlayerCount(ByVal PositionID As Integer?, ByVal searchValue As String) As Integer Implements IPlayerRepository.PlayerCount
        If searchValue = "" Then
            Return (From c In db.Players Where c.PositionID = PositionID).Count()
        End If
        Return (From c In db.Players Where c.PositionID = PositionID And c.Surname.Contains(searchValue)).Count()
    End Function

    Function PlayerCount(ByVal searchValue As String) As Integer Implements IPlayerRepository.PlayerCount
        If searchValue = "" Then
            Return (From c In db.Players).Count()
        End If
        Return (From c In db.Players Where c.Surname.Contains(searchValue)).Count()
    End Function

    Function GetNationalTeams() As IQueryable(Of SelectListItem) Implements IPlayerRepository.GetNationalTeams
        Dim oNatTeams = From c In db.Teams Where c.TeamTypeID = 1 Order By c.TeamName Select c = New SelectListItem _
                    With {.Value = c.TeamID, _
                          .Text = c.TeamName}
        Return oNatTeams
    End Function
    Function GetClubTeams() As List(Of SelectListItem) Implements IPlayerRepository.GetClubTeams
        Dim oclubTeams As IQueryable(Of SelectListItem) = From c In db.Teams Where c.TeamTypeID = 2 Order By c.TeamName Select c = New SelectListItem _
                    With {.Value = c.TeamID, _
                          .Text = c.TeamName}
        oclubTeams.First()
        Dim oclubTeams2 As New List(Of SelectListItem)
        oclubTeams2.Add(New SelectListItem With {.Value = 0, .Text = "Any"})
        For Each item As SelectListItem In oclubTeams.ToList()
            oclubTeams2.Add(item)
        Next item
        Return oclubTeams2
    End Function

    Function GetClubTeamsCreate() As IQueryable(Of SelectListItem) Implements IPlayerRepository.GetClubTeamsCreate
        Return From c In db.Teams Where c.TeamTypeID = 2 Order By c.TeamName Select c = New SelectListItem _
                    With {.Value = c.TeamID, .Text = c.TeamName}
    End Function

    Function ListPositionsDD() As IEnumerable(Of SelectListItem) Implements IPlayerRepository.ListPositionsDD
        Dim opositions = (From o As Integer In [Enum].GetValues(GetType(Position)).Cast(Of Integer)()
                            Select New SelectListItem With {.Value = o, .Text = [Enum].GetName(GetType(Position), o)}
                         )
        Return opositions
    End Function

    Function listNationalTeams() As IEnumerable(Of Team)
        Return From c In db.Teams Where c.TeamTypeID = 1
    End Function

    Function CountPlayerByTeam(ByVal TeamID As Integer?, ByVal searchValue As String) As Integer Implements IPlayerRepository.CountPlayerByTeam
        Return (From c In db.Players Where c.TeamID = TeamID And c.Surname.Contains(searchValue)).Count()
    End Function
End Class
