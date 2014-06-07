Public Class EventService
    Implements IEventService

    Private _validationDictionary As IValidationDictionary
    Private _repository As IEventRepository

    Public Sub New(ByVal validationDictionary As IValidationDictionary)
        Me.New(validationDictionary, New EventRepository())
    End Sub

    Public Function ValidateEvent(ByVal EventToValidate As [Event]) As Boolean
        Return _validationDictionary.IsValid
    End Function

    Public Sub New(ByVal validationDictionary As IValidationDictionary, ByVal repository As IEventRepository)
        _validationDictionary = validationDictionary
        _repository = repository
    End Sub

    Public Function CreateEvent(ByVal EventToCreate As [Event]) As Boolean Implements IEventService.CreateEvent
        If (Not ValidateEvent(EventToCreate)) Then
            Return False
        End If

        Try
            _repository.CreateEvent(EventToCreate)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Function DeleteEvent(ByVal EventToDelete As [Event]) As Boolean Implements IEventService.DeleteEvent
        Try
            _repository.DeleteEvent(EventToDelete)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Function EditEvent(ByVal EventToEdit As [Event]) As Boolean Implements IEventService.EditEvent
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

    Public Function EventCount(ByVal FixtureID As Integer) As Integer Implements IEventService.EventCount
        Return _repository.EventCount(FixtureID)
    End Function

    Public Function GetEvent(ByVal id As Integer) As [Event] Implements IEventService.GetEvent
        Return _repository.GetEvent(id)
    End Function

    Public Function ListEvent(ByVal FixtureID As Integer, ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As System.Collections.Generic.IEnumerable(Of [Event]) Implements IEventService.ListEvent
        Return _repository.ListEvent(FixtureID, pageSize, pageIndex)
    End Function

    Function ListWeekDD() As IQueryable(Of SelectListItem) Implements IEventService.ListWeekDD
        Return _repository.ListWeekDD()
    End Function
    Function ListTeamDD() As IQueryable(Of SelectListItem) Implements IEventService.ListTeamDD
        Return _repository.ListTeamDD()
    End Function

End Class
