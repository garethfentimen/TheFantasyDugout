Public Class FixtureIndexModel

    Property selectedCompetition As Integer?
    Property selectedFixtures As IEnumerable(Of Fixture)
    Property CompetitionTypes As IEnumerable(Of Competition)

    Property PreviousPage As Object
    Property NextPage As Object
    Property PageCount As Object

End Class
