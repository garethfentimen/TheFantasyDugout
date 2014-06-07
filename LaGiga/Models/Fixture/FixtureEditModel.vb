Public Class FixtureEditModel

    Property ThisEvent As [Event]
    Property Fixture As Fixture

    Property Events As IEnumerable(Of [Event])

    Property EventTypeValues As List(Of SelectListItem)

    Property HomePlayerTypeValues As List(Of SelectListItem)
    Property AwayPlayerTypeValues As List(Of SelectListItem)

    Property EventsToCreate As List(Of [Event])

    Property PlayerTypeValues As List(Of SelectListItem)

End Class
