Namespace LaGiga
    Public Class EventTypeController
        Inherits System.Web.Mvc.Controller

        Const PageSize As Integer = 10
        Private _service As IEventTypeService

        Sub New()
            _service = New EventTypeService(New ModelStateWrapper(ModelState))
        End Sub


        Sub New(ByVal service As IEventTypeService)
            _service = service
        End Sub

        'You might have noticed that our validation logic is still mixed up with our controller logic in the modified controller class in Listing 3. 
        'For the same reason that it is a good idea to isolate our data access logic, it is a good idea to isolate our validation logic.
        'To fix this problem, we can create a separate service layer. 
        'The service layer is a separate layer that we can insert between our controller and repository classes. 
        'The service layer contains our business logic including all of our validation logic.

        'Protected Sub ValidateEventType(ByVal EventTypeToValidate As EventType)
        '    ' Validation logic
        '    If EventTypeToValidate.EventName.Trim().Length = 0 Then
        '        ModelState.AddModelError("EventName", "Event name is required.")
        '    End If
        '    If EventTypeToValidate.Points = 0 Then
        '        ModelState.AddModelError("Points", "Points is required.")
        '    End If
        '    'If (contactToCreate.Phone.Length > 0 AndAlso Not Regex.IsMatch(contactToCreate.Phone, "((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")) Then
        '    '    ModelState.AddModelError("Phone", "Invalid phone number.")
        '    'End If
        '    'If (contactToCreate.Email.Length > 0 AndAlso Not Regex.IsMatch(contactToCreate.Email, "^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")) Then
        '    '    ModelState.AddModelError("Email", "Invalid email address.")
        '    'End If
        'End Sub

        <Authorize(Roles:="Administrator")> _
        Function EventTypeIndex(ByVal PageIndex As Integer?) As ActionResult
            Dim model As New EventTypeIndexModel With { _
                .AllEventTypes = _service.ListEventTypes(PageIndex, PageSize),
                .PageCount = Math.Floor((_service.EventTypeCount() / PageSize) + 1),
                .PreviousPage = IIf(PageIndex > 1, PageIndex - 1, 1),
                .NextPage = IIf(PageIndex < .PageCount, PageIndex + 1, .PageCount)
            }

            If Request.IsAjaxRequest() Then
                Return PartialView("EventTypeList", model)
            End If

            Return View("EventTypeIndex", model)
        End Function

        <Authorize(Roles:="Administrator")> _
        Function Create() As ActionResult
            ViewData("PositionTypes") = _service.ListPositionDD()
            Return View()
        End Function

    End Class
End Namespace