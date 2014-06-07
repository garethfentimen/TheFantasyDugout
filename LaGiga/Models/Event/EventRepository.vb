Public Class EventRepository
    Inherits StandardFunctions
    Implements IEventRepository

    Public Function CreateEvent(ByVal EventToCreate As [Event]) As [Event] Implements IEventRepository.CreateEvent
        db.Events.InsertOnSubmit(EventToCreate)
        db.SubmitChanges()
        Return EventToCreate
    End Function

    Public Function DeleteEvent(ByVal EventToDelete As [Event]) As [Event] Implements IEventRepository.DeleteEvent
        db.Events.DeleteOnSubmit(EventToDelete)
        db.SubmitChanges()
        Return EventToDelete
    End Function

    Public Function EditEvent(ByVal EventToEdit As [Event]) As [Event] Implements IEventRepository.EditEvent
        Dim originalEvent = (From c In db.Events Where c.EventID = EventToEdit.EventID).Single()
        originalEvent.EventTypeID = EventToEdit.EventTypeID
        originalEvent.FixtureID = EventToEdit.FixtureID
        originalEvent.PlayerID = EventToEdit.PlayerID
        originalEvent.FromMinute = EventToEdit.FromMinute
        originalEvent.ToMinute = EventToEdit.ToMinute
        originalEvent.WeekID = EventToEdit.WeekID
        originalEvent.Points = EventToEdit.Points
        db.SubmitChanges()
        Return EventToEdit
    End Function

    Public Function EventCount(ByVal FixtureID As Integer) As Integer Implements IEventRepository.EventCount
        Return (From c In db.Events Join d In db.Fixtures On d.FixtureID Equals c.FixtureID Where d.FixtureID = FixtureID).Count()
    End Function

    Public Function GetEvent(ByVal id As Integer) As [Event] Implements IEventRepository.GetEvent
        Dim oEvent = (From c In db.Events _
          Where c.EventID = id _
          Select c).FirstOrDefault()
        Return oEvent
    End Function

    Public Function ListEvent(ByVal FixtureID As Integer, ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As System.Collections.Generic.IEnumerable(Of [Event]) Implements IEventRepository.ListEvent
        Dim oEvents = From c In db.Events Where c.FixtureID = FixtureID Order By c.FromMinute
        If pageSize.HasValue And pageIndex.HasValue Then
            Return oEvents.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList()
        End If
        Return oEvents.ToList()
    End Function


    Function ListWeekDD() As IQueryable(Of SelectListItem) Implements IEventRepository.ListWeekDD
        Return From c In db.Weeks Select New SelectListItem _
               With {.Value = c.WeekID, _
                          .Text = c.WeekName}
    End Function

    Function ListTeamDD() As IQueryable(Of SelectListItem) Implements IEventRepository.ListTeamDD
        Dim oWeek = (From c In db.Weeks Order By c.WeekID Select c).Single

        Dim TeamTypeID As Helpers.TeamType = Helpers.TeamType.National
        If oWeek.Competition.Name = "Premier League" Then
            TeamTypeID = Helpers.TeamType.Club
        End If

        Return From c In db.Teams Where c.TeamTypeID = TeamTypeID And c.TeamName <> "None" Select New SelectListItem _
               With {.Value = c.TeamID, _
                          .Text = c.TeamName}
    End Function

    Function GetPlayers() As IEnumerable(Of Player) Implements IEventRepository.GetPlayers
        Return From c In db.Events Join d In db.Players On c.PlayerID Equals d.PlayerID Select d
    End Function
End Class
