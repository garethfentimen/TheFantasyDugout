Public Class FixtureService
    Implements IFixtureService

    Private _validationDictionary As IValidationDictionary
    Private _repository As IFixtureRepository

    Public Sub New(ByVal validationDictionary As IValidationDictionary)
        Me.New(validationDictionary, New FixtureRepository())
    End Sub

    Public Function ValidateFixture(ByVal FixtureToValidate As Fixture) As Boolean
        Return _validationDictionary.IsValid
    End Function

    Public Function ValidateEvent(ByVal EventToValidate As [Event]) As Boolean
        Return _validationDictionary.IsValid
    End Function

    Public Sub New(ByVal validationDictionary As IValidationDictionary, ByVal repository As IFixtureRepository)
        _validationDictionary = validationDictionary
        _repository = repository
    End Sub

    Public Function CreateFixture(ByVal FixtureToCreate As Fixture) As Boolean Implements IFixtureService.CreateFixture
        'If (Not ValidateFixture(FixtureToCreate)) Then
        'Return False
        'End If

        Try
            _repository.CreateFixture(FixtureToCreate)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Function DeleteFixture(ByVal FixtureToDelete As Fixture) As Boolean Implements IFixtureService.DeleteFixture
        Try
            _repository.DeleteFixture(FixtureToDelete)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Function EditFixture(ByVal FixtureToEdit As Fixture) As Boolean Implements IFixtureService.EditFixture
        If (Not ValidateFixture(FixtureToEdit)) Then
            Return False
        End If

        Try
            _repository.EditFixture(FixtureToEdit)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Function FixtureCount(ByVal CompetitionID As Integer?, ByVal searchValue As String) As Integer Implements IFixtureService.FixtureCount
        Return _repository.FixtureCount(CompetitionID, searchValue)
    End Function

    Public Function FixtureCount(ByVal searchValue As String) As Integer Implements IFixtureService.FixtureCount
        Return _repository.FixtureCount(searchValue)
    End Function

    Public Function GetFixture(ByVal id As Integer) As Fixture Implements IFixtureService.GetFixture
        Return _repository.GetFixture(id)
    End Function

    Public Function ListCompetitions() As System.Collections.Generic.IEnumerable(Of Competition) Implements IFixtureService.ListCompetitions
        Return _repository.ListCompetitions()
    End Function

    Public Function ListFixture(ByVal CompetitionID As Integer?, ByVal searchValue As String, ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As System.Collections.Generic.IEnumerable(Of Fixture) Implements IFixtureService.ListFixture
        Return _repository.ListFixture(CompetitionID, searchValue, pageSize, pageIndex)
    End Function

    Public Function ListFixture(ByVal pageSize As Integer?, ByVal pageIndex As Integer?, ByVal searchValue As String) As System.Collections.Generic.IEnumerable(Of Fixture) Implements IFixtureService.ListFixture
        Return _repository.ListFixture(pageSize, pageIndex, searchValue)
    End Function

    Function ListWeekDD() As IQueryable(Of SelectListItem) Implements IFixtureService.ListWeekDD
        Return _repository.ListWeekDD()
    End Function
    Function ListTeamDD() As IQueryable(Of SelectListItem) Implements IFixtureService.ListTeamDD
        Return _repository.ListTeamDD()
    End Function

    '----------Event Methods

    Public Function CreateEvent(ByVal EventToCreate As [Event]) As Boolean Implements IFixtureService.CreateEvent
        Try
            _repository.CreateEvent(EventToCreate)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Function DeleteEvent(ByVal EventToDelete As [Event]) As Boolean Implements IFixtureService.DeleteEvent
        Try
            _repository.DeleteEvent(EventToDelete)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Function EditEvent(ByVal EventToEdit As [Event]) As Boolean Implements IFixtureService.EditEvent
        If (Not ValidateEvent(EventToEdit)) Then
            Return False
        End If

        Try
            _repository.EditEvent(EventToEdit)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Function EventCount(ByVal EventID As Integer) As Integer Implements IFixtureService.EventCount
        Return _repository.EventCount(EventID)
    End Function

    Public Function GetEvent(ByVal id As Integer) As [Event] Implements IFixtureService.GetEvent
        Return _repository.GetEvent(id)
    End Function

    Public Function ListEvent(ByVal EventID As Integer, ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As System.Collections.Generic.IEnumerable(Of [Event]) Implements IFixtureService.ListEvent
        Return _repository.ListEvent(EventID, pageSize, pageIndex)
    End Function

    Function ListPlayerDD() As List(Of SelectListItem) Implements IFixtureService.ListPlayerDD
        Return _repository.ListPlayerDD()
    End Function

    Function GetEvents(ByVal FixtureID As Integer) As IEnumerable(Of [Event]) Implements IFixtureService.GetEvents
        Return _repository.GetEvents(FixtureID)
    End Function

    Function ListHomePlayerDDForFixture(ByVal id As Integer) As List(Of SelectListItem) Implements IFixtureService.ListHomePlayerDDForFixture
        Return _repository.ListHomePlayerDDForFixture(id)
    End Function

    Function ListAwayPlayerDDForFixture(ByVal id As Integer) As List(Of SelectListItem) Implements IFixtureService.ListAwayPlayerDDForFixture
        Return _repository.ListAwayPlayerDDForFixture(id)
    End Function

    Function AddEventCalculator(ByVal EventToCreate As [Event], ByVal FixtureEditDetail As FixtureEditModel) As [Event] Implements IFixtureService.AddEventCalculator
        Return _repository.AddEventCalculator(EventToCreate, FixtureEditDetail)
    End Function
End Class
