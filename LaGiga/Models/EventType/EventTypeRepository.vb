Imports LaGiga.Helpers

Public Class EventTypeRepository
    Implements IEventTypeRepository

    Private db As New LaGigaClassesDataContext

    Function ListEventTypes(ByVal PageIndex As Integer?, ByVal PageSize As Integer?) As IEnumerable(Of EventType) Implements IEventTypeRepository.ListEventTypes
        Dim oEventTypeList = From c In db.EventTypes
        If PageSize.HasValue And PageIndex.HasValue Then
            Return oEventTypeList.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList()
        End If
        Return oEventTypeList.ToList()
    End Function

    Public Function CreateEventType(ByVal EventTypeToCreate As EventType) As EventType Implements IEventTypeRepository.CreateEventType
        db.EventTypes.InsertOnSubmit(EventTypeToCreate)
        db.SubmitChanges()
        Return EventTypeToCreate
    End Function

    Public Function EditEventType(ByVal EventTypeToEdit As EventType) As EventType Implements IEventTypeRepository.EditEventType
        Dim originalEventType = (From c In db.EventTypes _
                                 Where c.EventTypeID = EventTypeToEdit.EventTypeID _
                                 Select c).FirstOrDefault()
        originalEventType.EventName = EventTypeToEdit.EventName
        originalEventType.Points = EventTypeToEdit.Points
        originalEventType.PositionID = EventTypeToEdit.PositionID
        originalEventType.Master = EventTypeToEdit.Master
        db.SubmitChanges()
        Return EventTypeToEdit
    End Function

    Public Sub DeleteEventType(ByVal EventTypeToDelete As EventType) Implements IEventTypeRepository.DeleteEventType
        Dim originalEventType = GetEventType(EventTypeToDelete.EventTypeID)
        db.EventTypes.DeleteOnSubmit(originalEventType)
        db.SubmitChanges()
    End Sub

    Public Function GetEventType(ByVal id As Integer) As EventType Implements IEventTypeRepository.GetEventType
        Dim oEventType = (From c In db.EventTypes _
          Where c.EventTypeID = id _
          Select c).FirstOrDefault()
        Return oEventType
    End Function

    Public Function ListPositionDD() As IQueryable(Of SelectListItem) Implements IEventTypeRepository.ListPositionDD
        Return (From o As Integer In [Enum].GetValues(GetType(Position)).Cast(Of Integer)()
                        Select New SelectListItem With {.Value = o, .Text = [Enum].GetName(GetType(Position), o)}
                        )
    End Function

    Function EventTypeCount() As Integer Implements IEventTypeRepository.EventTypeCount
        Return (From c In db.EventTypes).Count()
    End Function

End Class
