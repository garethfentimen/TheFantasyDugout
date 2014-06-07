Public Interface IEventTypeService

    ' EventType methods
    Function CreateEventType(ByVal EventTypeToCreate As EventType) As Boolean
    Function DeleteEventType(ByVal EventTypeToDelete As EventType) As Boolean
    Function EditEventType(ByVal EventTypeToEdit As EventType) As Boolean
    Function GetEventType(ByVal id As Integer) As EventType
    Function ListEventTypes(ByVal PageIndex As Integer?, ByVal PageSize As Integer?) As IEnumerable(Of EventType)

    Function ListPositionDD() As IQueryable(Of SelectListItem)
    Function EventTypeCount() As Integer

End Interface

