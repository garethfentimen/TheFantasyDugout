Namespace LaGiga
    Public Class EventController
        Inherits System.Web.Mvc.Controller

        Const pageSize As Integer = 10
        Private _service As IEventService

        Sub New()
            _service = New EventService(New ModelStateWrapper(ModelState))
        End Sub


        Sub New(ByVal service As IEventService)
            _service = service
        End Sub

        Function EventIndex(ByVal pageIndex As Integer?) As ActionResult
            If pageIndex Is Nothing Then
                pageIndex = 1
            End If
            Dim FixtureID = 7 'Request.QueryString("FixtureID")

            Dim model As New EventIndexModel With { _
                .PageCount = Math.Floor(_service.EventCount(FixtureID) / pageSize + 0.5),
                .PreviousPage = IIf(pageIndex > 1, pageIndex - 1, 1),
                .NextPage = IIf(pageIndex < .PageCount, pageIndex + 1, .PageCount)
            }

            If Request.IsAjaxRequest() Then
                Return PartialView("FixtureList", model)
            End If

            Return View("FixtureIndex", model)
            Return View()
        End Function

    End Class
End Namespace