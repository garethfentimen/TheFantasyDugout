Namespace LaGiga
    Public Class LeagueController
        Inherits System.Web.Mvc.Controller


        Public homeRepository As New HomeRepository
        Public leagueRepository As New LeagueRepository
        '
        ' GET: /League

        <Authorize(Roles:="Player,Administrator")> _
        Function LeagueIndex() As ActionResult

            Dim currentWeek As Week = Helpers.CurrentWeek()

            Dim weekstart As String = String.Empty
            Dim weekEnd As String = String.Empty

            If currentWeek.FromDate.HasValue Then
                weekstart = currentWeek.FromDate.Value.ToString("d MMM yyyy")
            End If

            If currentWeek.ToDate.HasValue Then
                weekEnd = " - " & currentWeek.ToDate.Value.ToString("d MMM yyyy")
            End If

            'create own user friendly class
            Dim realWeekFixtures As New RealWeekFixturesIndexModel With {
                    .Fixtures = homeRepository.getRealWeekFixtures(currentWeek.WeekID),
                    .WeekNo = currentWeek.WeekNo,
                    .WeekDate = weekstart & weekEnd
                }


            Dim model As New LeagueIndexModel With {
                    .RealWeekFixtures = realWeekFixtures,
                    .LaGigaLogon = New LogOnModel
                }

            Return View(model)
        End Function

    End Class
End Namespace