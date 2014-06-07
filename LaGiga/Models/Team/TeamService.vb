Public Class TeamService
    Implements ITeamService

    Private _validationDictionary As IValidationDictionary
    Private _repository As ITeamRepository

    Public Sub New(ByVal validationDictionary As IValidationDictionary)
        Me.New(validationDictionary, New TeamRepository())
    End Sub


    Public Sub New(ByVal validationDictionary As IValidationDictionary, ByVal repository As ITeamRepository)
        _validationDictionary = validationDictionary
        _repository = repository
    End Sub

    ' Team methods

    Public Function ValidateTeam(ByVal TeamToValidate As Team) As Boolean
        'If TeamToValidate.EventName.Trim().Length = 0 Then
        '    _validationDictionary.AddError("EventName", "Event name is required.")
        'End If
        'If TeamToValidate.Points = 0 Then
        '    _validationDictionary.AddError("Points", "Please specify how many Points should be awarded for this event type.")
        'End If
        'If contactToValidate.Phone.Length > 0 AndAlso (Not Regex.IsMatch(contactToValidate.Phone, "((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")) Then
        '    _validationDictionary.AddError("Phone", "Invalid phone number.")
        'End If
        'If contactToValidate.Email.Length > 0 AndAlso (Not Regex.IsMatch(contactToValidate.Email, "^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")) Then
        '    _validationDictionary.AddError("Email", "Invalid email address.")
        'End If
        Return _validationDictionary.IsValid
    End Function

#Region "ITeamService Members"

    Public Function CreateTeam(ByVal TeamToCreate As Team) As Boolean Implements ITeamService.CreateTeam
        ' Validation logic
        If (Not ValidateTeam(TeamToCreate)) Then
            Return False
        End If

        ' Database logic
        Try
            _repository.CreateTeam(TeamToCreate)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Function EditTeam(ByVal TeamToEdit As Team) As Boolean Implements ITeamService.EditTeam
        ' Validation logic
        If (Not ValidateTeam(TeamToEdit)) Then
            Return False
        End If

        ' Database logic
        Try
            _repository.EditTeam(TeamToEdit)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Function DeleteTeam(ByVal TeamToDelete As Team) As Boolean Implements ITeamService.DeleteTeam
        Try
            _repository.DeleteTeam(TeamToDelete)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Function GetTeam(ByVal id As Integer) As Team Implements ITeamService.GetTeam
        Return _repository.GetTeam(id)
    End Function

    Public Function ListTeams1(ByVal TeamTypeID As Integer?, ByVal searchValue As String, ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As IEnumerable(Of Team) Implements ITeamService.ListTeams1
        Return _repository.ListTeams1(TeamTypeID, searchValue, pageSize, pageIndex)
    End Function

    Public Function ListTeams(ByVal pageSize As Integer?, ByVal pageIndex As Integer?, ByVal searchValue As String) As IEnumerable(Of Team) Implements ITeamService.ListTeams
        Return _repository.ListTeams(pageSize, pageIndex, searchValue)
    End Function

    Public Function TeamsCount(ByVal TeamTypeID As Integer, ByVal searchValue As String) Implements ITeamService.TeamsCount
        Return _repository.TeamsCount(TeamTypeID, searchValue)
    End Function

    Public Function TeamsCount(ByVal searchValue As String) Implements ITeamService.TeamsCount
        Return _repository.TeamsCount(searchValue)
    End Function

    Public Function ListNational() As IEnumerable(Of Team) Implements ITeamService.ListNational
        Return _repository.ListNational()
    End Function

    Public Function ListClub() As IEnumerable(Of Team) Implements ITeamService.ListClub
        Return _repository.ListClubTeams()
    End Function

    Public Function ListTeamType() As IEnumerable(Of Helpers.TeamType) Implements ITeamService.ListTeamTypes
        Return _repository.ListTeamTypes()
    End Function

    Function GetTeamType(ByVal TeamTypeID As Integer?) As Helpers.TeamType Implements ITeamService.GetTeamType
        Return _repository.GetTeamType(TeamTypeID)
    End Function

    Function getTeamTypeDDList() As IEnumerable(Of SelectListItem) Implements ITeamService.getTeamTypeDDList
        Return _repository.getTeamTypeDDList
    End Function

    Function getTeamTypeDDList(ByVal TeamID As Integer) As IEnumerable(Of SelectListItem) Implements ITeamService.getTeamTypeDDList
        Return _repository.getTeamTypeDDList(TeamID)
    End Function
#End Region

End Class
