Public Interface IEventService

    Function CreateEvent(ByVal EventToCreate As [Event]) As Boolean
    Function DeleteEvent(ByVal EventToDelete As [Event]) As Boolean
    Function EditEvent(ByVal EventToEdit As [Event]) As Boolean
    Function GetEvent(ByVal id As Integer) As [Event]
    Function ListEvent(ByVal FixtureID As Integer, ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As IEnumerable(Of [Event])

    Function EventCount(ByVal FixtureID As Integer) As Integer

    Function ListWeekDD() As IQueryable(Of SelectListItem)
    Function ListTeamDD() As IQueryable(Of SelectListItem)

End Interface
