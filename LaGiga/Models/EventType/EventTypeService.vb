Public Class EventTypeService
    Implements IEventTypeService

    Private _validationDictionary As IValidationDictionary
    Private _repository As IEventTypeRepository

    'Constructor Dependency Injection.
    'The one and only place that the EntityContactManagerRepository class is used is in the first constructor. The remainder of the class uses the IContactManagerRepository interface instead of the concrete EntityContactManagerRepository class.
    'This makes it easy to switch implementations of the IEventTypeRepository class in the future. If you want to use the DataServicesContactRepository class instead of the EntityContactManagerRepository class, just modify the first constructor
    Public Sub New(ByVal validationDictionary As IValidationDictionary)
        Me.New(validationDictionary, New EventTypeRepository())
    End Sub


    Public Sub New(ByVal validationDictionary As IValidationDictionary, ByVal repository As IEventTypeRepository)
        _validationDictionary = validationDictionary
        _repository = repository
    End Sub

    ' EventType methods

    Public Function ValidateEventType(ByVal EventTypeToValidate As EventType) As Boolean
        If EventTypeToValidate.EventName.Trim().Length = 0 Then
            _validationDictionary.AddError("EventName", "Event name is required.")
        End If
        If EventTypeToValidate.Points = 0 Then
            _validationDictionary.AddError("Points", "Please specify how many Points should be awarded for this event type.")
        End If
        Return _validationDictionary.IsValid
    End Function

#Region "IEventTypeService Members"

    Public Function CreateEventType(ByVal EventTypeToCreate As EventType) As Boolean Implements IEventTypeService.CreateEventType
        ' Validation logic
        If (ValidateEventType(EventTypeToCreate)) Then
            Return False
        End If

        ' Database logic
        Try
            _repository.CreateEventType(EventTypeToCreate)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Function EditEventType(ByVal EventTypeToEdit As EventType) As Boolean Implements IEventTypeService.EditEventType
        ' Validation logic
        If (Not ValidateEventType(EventTypeToEdit)) Then
            Return False
        End If

        ' Database logic
        Try
            _repository.EditEventType(EventTypeToEdit)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Function DeleteEventType(ByVal EventTypeToDelete As EventType) As Boolean Implements IEventTypeService.DeleteEventType
        Try
            _repository.DeleteEventType(EventTypeToDelete)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Function GetEventType(ByVal id As Integer) As EventType Implements IEventTypeService.GetEventType
        Return _repository.GetEventType(id)
    End Function

    Function ListEventTypes(ByVal PageIndex As Integer?, ByVal PageSize As Integer?) As IEnumerable(Of EventType) Implements IEventTypeService.ListEventTypes
        Return _repository.ListEventTypes(PageIndex, PageSize)
    End Function

    Function ListPositionDD() As IQueryable(Of SelectListItem) Implements IEventTypeService.ListPositionDD
        Return _repository.ListPositionDD()
    End Function

    Function EventTypeCount() As Integer Implements IEventTypeService.EventTypeCount
        Return _repository.EventTypeCount()
    End Function

#End Region
End Class
