Public Class UserPlayerService
    Implements IUserPlayerService

    Private _validationDictionary As IValidationDictionary
    Private _repository As IUserPlayerRepository

    Public Sub New(ByVal validationDictionary As IValidationDictionary)
        Me.New(validationDictionary, New UserPlayerRepository())
    End Sub

    Public Sub New(ByVal validationDictionary As IValidationDictionary, ByVal repository As IUserPlayerRepository)
        _validationDictionary = validationDictionary
        _repository = repository
    End Sub

    Public Function ValidateUserTeam(ByVal UserTeamToValidate As UserPlayer) As Boolean
        Return _validationDictionary.IsValid
    End Function

    Function ListUserPlayer(ByVal UserTeamID As Integer?, ByVal CompetitionID As Integer, ByVal pageSize As Integer, ByVal pageIndex As Integer?) As IEnumerable(Of UserPlayer) Implements IUserPlayerService.ListUserPlayer
        Return _repository.ListUserPlayer(UserTeamID, CompetitionID, pageSize, pageIndex)
    End Function

    Function UserPlayerCount(ByVal UserTeamID As Integer?, ByVal CompetitionID As Integer) As Integer Implements IUserPlayerService.UserPlayerCount
        Return _repository.UserPlayerCount(UserTeamID, CompetitionID)
    End Function

    Function GetClubTeamsDD() As IQueryable(Of SelectListItem) Implements IUserPlayerService.GetClubTeamsDD
        Return _repository.GetClubTeamsDD()
    End Function

    Function getPlayersAtClubsDD(ByVal TeamID As Integer) As IEnumerable(Of SelectListItem) Implements IUserPlayerService.getPlayersAtClubsDD
        Return _repository.getPlayersAtClubsDD(TeamID)
    End Function

    Function GetUserGroup(ByVal UserTeamID As Integer) As UserTeam Implements IUserPlayerService.GetUserGroup
        Return _repository.GetUserGroup(UserTeamID)
    End Function

    Function CreateUserPlayer(ByVal UserPlayerToCreate As UserPlayer) As Boolean Implements IUserPlayerService.CreateUserPlayer
        If Not (ValidateUserTeam(UserPlayerToCreate)) Then
            Return False
        End If

        Try
            _repository.CreateUserPlayer(UserPlayerToCreate)
        Catch
            Return False
        End Try
        Return True
    End Function

    Function CreateWeekUserPlayer(ByVal UserPlayerToCreate As UserPlayer) As Boolean Implements IUserPlayerService.CreateWeekUserPlayer
        Try
            _repository.CreateWeekUserPlayer(UserPlayerToCreate)
        Catch
            Return False
        End Try
        Return True
    End Function

    Function DeleteUserPlayer(ByVal UserPlayerToDelete As UserPlayer) As Boolean Implements IUserPlayerService.DeleteUserPlayer
        Try
            _repository.DeleteUserPlayer(UserPlayerToDelete)
        Catch
            Return False
        End Try
        Return True
    End Function

    Function GetUserPlayer(ByVal UserPlayerID As Integer?) As UserPlayer Implements IUserPlayerService.GetUserPlayer
        Return _repository.GetUserPlayer(UserPlayerID)
    End Function

End Class
