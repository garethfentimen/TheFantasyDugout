Public Class HomeRepository
    Inherits StandardFunctions

    Function getRealWeekFixtures(ByVal weekID As Integer) As List(Of Fixture)

        Return (From o As Fixture In db.Fixtures Where o.WeekID.Equals(weekID)).ToList()

    End Function

End Class
