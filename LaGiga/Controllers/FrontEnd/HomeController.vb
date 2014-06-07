<HandleError()> _
Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Private _service As IPlayerHomeService

    Sub New()
        _service = New PlayerHomeService(New ModelStateWrapper(ModelState))
    End Sub

    Sub New(ByVal service As IPlayerHomeService)
        _service = service
    End Sub

    Public homeRepository As New HomeRepository

    Function Index() As ActionResult

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


        Dim model As New HomeIndexModel With {
                .RealWeekFixtures = realWeekFixtures,
                .LaGigaLogon = New LogOnModel
            }

        Return View(model)
    End Function

    <Authorize(Roles:="Administrator")> _
    Function AdminIndex()
        ViewData("Message") = "Welcome, Administrator!"

        Dim model As New HomeIndexModel With {
            .LaGigaLogon = New LogOnModel
        }

        Return View(model)
    End Function

    Function About() As ActionResult
        ViewData("About") = "<p>The Fantasy Dugout has been set up by a group of football enthusiasts who have become disatisfied with the fantasy football games available commercially.</p>" & _
                              "<p>Without giving too much away the system is based upon the idea that no two players should own the same footballer at the same time.</p>"
        Return View()
    End Function

End Class
