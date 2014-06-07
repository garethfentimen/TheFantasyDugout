Public Interface IEventTypeRepository

    Function CreateEventType(ByVal EventTypeToCreate As EventType) As EventType
    Sub DeleteEventType(ByVal EventTypeToDelete As EventType)
    Function EditEventType(ByVal EventTypeToEdit As EventType) As EventType
    Function GetEventType(ByVal id As Integer) As EventType
    Function ListEventTypes(ByVal PageIndex As Integer?, ByVal PageSize As Integer?) As IEnumerable(Of EventType)

    Function ListPositionDD() As IQueryable(Of SelectListItem)
    Function EventTypeCount() As Integer

End Interface
