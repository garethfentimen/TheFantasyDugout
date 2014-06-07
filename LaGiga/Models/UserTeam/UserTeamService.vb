Public Class UserTeamService
    Implements IUserTeamService

    Private _validationDictionary As IValidationDictionary
    Private _repository As IUserTeamRepository

    Public Sub New(ByVal validationDictionary As IValidationDictionary)
        Me.New(validationDictionary, New UserTeamRepository())
    End Sub

    Public Sub New(ByVal validationDictionary As IValidationDictionary, ByVal repository As IUserTeamRepository)
        _validationDictionary = validationDictionary
        _repository = repository
    End Sub

    Public Function ValidateUserTeam(ByVal UserTeamToValidate As UserTeam) As Boolean
        Return _validationDictionary.IsValid
    End Function

#Region "IUserTeamService Members"

    Public Function CreateUserTeam(ByVal UserTeamToCreate As UserTeam) As Boolean Implements IUserTeamService.CreateUserTeam
        If (ValidateUserTeam(UserTeamToCreate)) Then
            Return False
        End If

        Try
            _repository.CreateTeam(UserTeamToCreate)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Function DeleteUserTeam(ByVal UserTeamToDelete As UserTeam) As Boolean Implements IUserTeamService.DeleteUserTeam
        Try
            _repository.DeleteUserTeam(UserTeamToDelete)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Function EditUserTeam(ByVal UserTeamToEdit As UserTeam) As Boolean Implements IUserTeamService.EditUserTeam
        If (Not ValidateUserTeam(UserTeamToEdit)) Then
            Return False
        End If

        Try
            _repository.EditUserTeam(UserTeamToEdit)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Function GetUserTeam(ByVal id As Integer) As UserTeam Implements IUserTeamService.GetUserTeam
        Return _repository.GetUserTeam(id)
    End Function

    Public Function ListUserTeam(ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As System.Collections.Generic.IEnumerable(Of UserTeam) Implements IUserTeamService.ListUserTeam
        Return _repository.ListUserTeam(pageSize, pageIndex)
    End Function

    Public Function UserTeamCount() As Integer Implements IUserTeamService.UserTeamCount
        Return _repository.UserTeamCount()
    End Function

    Function UserGroupDDList() As IQueryable(Of SelectListItem) Implements IUserTeamService.UserGroupDDList
        Return _repository.UserGroupDDList
    End Function

    Function GetAllCompetitions() As List(Of Competition) Implements IUserTeamService.GetAllCompetitions
        Return _repository.GetAllCompetitions()
    End Function

#End Region

End Class
