Imports LaGiga.Helpers

Public Class PlayerService
    Implements IPlayerService

    Private _validationDictionary As IValidationDictionary
    Private _repository As IPlayerRepository

    Public Sub New(ByVal validationDictionary As IValidationDictionary)
        Me.New(validationDictionary, New PlayerRepository())
    End Sub

    Public Function ValidateTeam(ByVal PlayerToValidate As Player) As Boolean
        Return _validationDictionary.IsValid
    End Function

    Public Sub New(ByVal validationDictionary As IValidationDictionary, ByVal repository As IPlayerRepository)
        _validationDictionary = validationDictionary
        _repository = repository
    End Sub

    Function CreatePlayer(ByVal PlayerToCreate As Player) As Boolean Implements IPlayerService.CreatePlayer
        If (Not ValidateTeam(PlayerToCreate)) Then
            Return False
        End If

        Try
            _repository.CreatePlayer(PlayerToCreate)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Function DeletePlayer(ByVal PlayerToDelete As Player) As Boolean Implements IPlayerService.DeletePlayer
        Try
            _repository.DeletePlayer(PlayerToDelete)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Function EditPlayer(ByVal PlayerToEdit As Player) As Boolean Implements IPlayerService.EditPlayer
        If (Not ValidateTeam(PlayerToEdit)) Then
            Return False
        End If

        Try
            _repository.EditPlayer(PlayerToEdit)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Function GetPlayer(ByVal id As Integer) As Player Implements IPlayerService.GetPlayer
        Return _repository.GetPlayer(id)
    End Function

    Public Function ListPlayer(ByVal pageSize As Integer?, ByVal pageIndex As Integer?, ByVal searchValue As String) As System.Collections.Generic.IEnumerable(Of Player) Implements IPlayerService.ListPlayer
        Return _repository.ListPlayer(pageSize, pageIndex, searchValue)
    End Function

    Public Function ListPlayer(ByVal PositionID As Integer?, ByVal searchValue As String, ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As System.Collections.Generic.IEnumerable(Of Player) Implements IPlayerService.ListPlayer
        Return _repository.ListPlayer(PositionID, searchValue, pageSize, pageIndex)
    End Function

    Function ListPlayerByTeam(ByVal TeamID As Integer?, ByVal searchValue As String, ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As IEnumerable(Of Player) Implements IPlayerService.ListPlayerByTeam
        Return _repository.ListPlayerByTeam(TeamID, searchValue, pageSize, pageIndex)
    End Function

    Function ListPositions() As IEnumerable(Of Integer) Implements IPlayerService.ListPositions
        Return _repository.ListPositions()
    End Function
    Function PlayerCount(ByVal PositionID As Integer?, ByVal searchValue As String) As Integer Implements IPlayerService.PlayerCount
        Return _repository.PlayerCount(PositionID, searchValue)
    End Function
    Function PlayerCount(ByVal searchValue As String) As Integer Implements IPlayerService.PlayerCount
        Return _repository.PlayerCount(searchValue)
    End Function

    Function GetNationalTeams() As IQueryable(Of SelectListItem) Implements IPlayerService.GetNationalTeams
        Return _repository.GetNationalTeams()
    End Function

    Function GetClubTeams() As List(Of SelectListItem) Implements IPlayerService.GetClubTeams
        Return _repository.GetClubTeams()
    End Function

    Function GetClubTeamsCreate() As IQueryable(Of SelectListItem) Implements IPlayerService.GetClubTeamsCreate
        Return _repository.GetClubTeamsCreate()
    End Function

    Function ListPositionsDD() As IEnumerable(Of SelectListItem) Implements IPlayerService.ListPositionsDD
        Return _repository.ListPositionsDD()
    End Function

    Function CountPlayerByTeam(ByVal TeamID As Integer?, ByVal searchValue As String) As Integer Implements IPlayerService.CountPlayerByTeam
        Return _repository.CountPlayerByTeam(TeamID, searchValue)
    End Function

    Function CreateTransfer(ByVal TransferToCreate As Transfer, ByVal NewTeamID As Integer) As Boolean Implements IPlayerService.CreateTransfer
        Return _repository.CreateTransfer(TransferToCreate, NewTeamID)
    End Function
End Class
