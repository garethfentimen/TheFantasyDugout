Public Interface IEventRepository
    Function CreateEvent(ByVal EventToCreate As [Event]) As [Event]
    Function DeleteEvent(ByVal EventToDelete As [Event]) As [Event]
    Function EditEvent(ByVal EventToEdit As [Event]) As [Event]
    Function GetEvent(ByVal id As Integer) As [Event]
    Function ListEvent(ByVal EventID As Integer, ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As IEnumerable(Of [Event])

    Function EventCount(ByVal EventID As Integer) As Integer

    Function ListWeekDD() As IQueryable(Of SelectListItem)
    Function ListTeamDD() As IQueryable(Of SelectListItem)

    Function GetPlayers() As IEnumerable(Of Player)
End Interface
