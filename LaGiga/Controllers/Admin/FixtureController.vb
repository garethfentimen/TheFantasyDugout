Namespace LaGiga
    Public Class FixtureController
        Inherits System.Web.Mvc.Controller

        Public iFixtureID As Integer
        Const pageSize As Integer = 10
        Private _service As IFixtureService

        Sub New()
            _service = New FixtureService(New ModelStateWrapper(ModelState))
        End Sub


        Sub New(ByVal service As IFixtureService)
            _service = service
        End Sub

        <Authorize(Roles:="Administrator")> _
        Function FixtureIndex(ByVal CompetitionID As Integer?, ByVal pageIndex As Integer?, Optional ByVal searchValue As String = "") As ActionResult

            Dim oFixtures As IEnumerable(Of Fixture)
            If (CompetitionID.HasValue And CompetitionID > 0) Then
                oFixtures = _service.ListFixture(CompetitionID, searchValue, pageSize, pageIndex)
            Else
                oFixtures = _service.ListFixture(pageSize, pageIndex, searchValue)
            End If

            Dim iFixturePageCount As Integer
            If (CompetitionID.HasValue And CompetitionID > 0) Then
                iFixturePageCount = _service.FixtureCount(CompetitionID, searchValue) / pageSize + 0.5
                iFixturePageCount = Math.Floor(iFixturePageCount)
            Else
                iFixturePageCount = Math.Floor(_service.FixtureCount(searchValue) / pageSize + 0.5)
                iFixturePageCount = Math.Floor(iFixturePageCount)
            End If

            Dim model As New FixtureIndexModel With { _
                .CompetitionTypes = _service.ListCompetitions(),
                .selectedCompetition = CompetitionID,
                .selectedFixtures = oFixtures,
                .PageCount = iFixturePageCount,
                .PreviousPage = IIf(pageIndex > 1, pageIndex - 1, 1),
                .NextPage = IIf(pageIndex < .PageCount, pageIndex + 1, .PageCount)
            }

            If Request.IsAjaxRequest() Then
                Return PartialView("FixtureList", model)
            End If

            Return View("FixtureIndex", model)
        End Function

        <Authorize(Roles:="Administrator")> _
        Function ViewFixture(ByVal id As Integer) As ActionResult
            Dim model As New FixtureEditModel With { _
                .Fixture = _service.GetFixture(id),
                .Events = _service.GetEvents(id),
                .EventTypeValues = Helpers.EventTypeList,
                .PlayerTypeValues = _service.ListPlayerDD()
            }
            Return View("ViewFixture", model)
        End Function

        <Authorize(Roles:="Administrator")> _
        Function Create() As ActionResult
            ViewData("WeekTypesDD") = _service.ListWeekDD()
            ViewData("HomeTeamTypesDD") = _service.ListTeamDD()
            ViewData("AwayTeamTypesDD") = _service.ListTeamDD()
            Return View()
        End Function

        <HttpPost()> _
        Function CreateFixture(ByVal FixtureToCreate As Fixture, ByVal WeekTypesDD As Integer, ByVal HomeTeamTypesDD As Integer, ByVal AwayTeamTypesDD As Integer) As ActionResult

            FixtureToCreate.HomeTeamID = HomeTeamTypesDD
            FixtureToCreate.AwayTeamID = AwayTeamTypesDD
            FixtureToCreate.WeekID = WeekTypesDD
            If Request.IsAjaxRequest Then

                If _service.CreateFixture(FixtureToCreate) Then
                    Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.Success})
                Else
                    Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.IsError})
                End If
            End If
            Return RedirectToAction("FixtureIndex")
        End Function

        <HttpPost()> _
        Function CreateEvent(ByVal EventToCreate As [Event], ByVal FixtureEditDetail As FixtureEditModel) As ActionResult

            EventToCreate = _service.AddEventCalculator(EventToCreate, FixtureEditDetail)

            If Request.IsAjaxRequest Then
                If _service.CreateEvent(EventToCreate) Then
                    Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.Success})
                Else
                    Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.IsError})
                End If
            End If
            Return RedirectToAction("FixtureIndex")
        End Function

        
        <Authorize(Roles:="Administrator")> _
        Function Edit(ByVal id As Integer) As ActionResult

            Dim model As New FixtureEditModel With { _
                .Fixture = _service.GetFixture(id),
                .EventTypeValues = Helpers.EventTypeList,
                .HomePlayerTypeValues = _service.ListHomePlayerDDForFixture(id),
                .AwayPlayerTypeValues = _service.ListAwayPlayerDDForFixture(id)
            }
            Return View(model)
        End Function

        '
        ' POST: /Fixture/Edit

        <HttpPost()> _
        Function Save(ByVal FixtureToEdit As FixtureEditModel) As ActionResult
            If _service.EditFixture(FixtureToEdit.Fixture) Then
                Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.Success})
            Else
                Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.IsError})
            End If
            Return View()
        End Function

        'PRE: Fixture/Eventedit 

        <Authorize(Roles:="Administrator")> _
        Function EventEdit(ByVal id As Integer) As ActionResult
            Return View(_service.GetEvent(id))
        End Function

        '
        ' POST: /Fixture/EventEdit
        <HttpPost()> _
        Function SaveEvent(ByVal EventToEdit As [Event]) As ActionResult
            If _service.EditEvent(EventToEdit) Then
                Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.Success})
            Else
                Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.IsError})
            End If
            Return View()
        End Function

        '
        ' GET: /Fixture/Delete/5
        <Authorize(Roles:="Administrator")> _
        Function Delete(ByVal id As Integer) As ActionResult
            Return View(_service.GetFixture(id))
        End Function

        '
        ' POST: /Fixture/Delete/5
        <Authorize(Roles:="Administrator")> _
        <AcceptVerbs(HttpVerbs.Delete), ActionName("Delete")> _
        Public Function AjaxDelete(ByVal FixtureID As Integer?, ByVal CompetitionID As Integer?, ByVal pageIndex As Integer?, Optional ByVal searchValue As String = "") As ActionResult
            Dim FixtureToDelete = _service.GetFixture(FixtureID)

            ' Delete from database
            _service.DeleteFixture(FixtureToDelete)

            Dim model As New FixtureIndexModel With { _
                .CompetitionTypes = _service.ListCompetitions(),
                .selectedCompetition = CompetitionID,
                .selectedFixtures = IIf((FixtureID.HasValue And FixtureID > 0),
                                    _service.ListFixture(CompetitionID, searchValue, pageSize, pageIndex),
                                    _service.ListFixture(pageSize, pageIndex, searchValue)),
                .PageCount = IIf(FixtureID.HasValue And FixtureID > 0,
                                    Math.Floor((_service.FixtureCount(CompetitionID, searchValue) / pageSize) + 1),
                                    Math.Floor((_service.FixtureCount(searchValue) / pageSize) + 1)
                                  ),
                .PreviousPage = IIf(pageIndex > 1, pageIndex - 1, 1),
                .NextPage = IIf(pageIndex < .PageCount, pageIndex + 1, .PageCount)
            }


            If Request.IsAjaxRequest() Then
                Return PartialView("FixtureList", model)
            End If

            Return View("FixtureIndex", model)
        End Function

        '
        ' GET: /Fixture/Delete/5
        <Authorize(Roles:="Administrator")> _
        Function DeleteEvent(ByVal id As Integer) As ActionResult
            Return View(_service.GetEvent(id))
        End Function

        '
        ' POST: /Fixture/EventDelete/5
        <Authorize(Roles:="Administrator")> _
        <AcceptVerbs(HttpVerbs.Post)> _
        Function DeleteEvent(ByVal EventTodelete As [Event]) As ActionResult

            ' Delete from database
            _service.DeleteEvent(_service.GetEvent(EventTodelete.EventID))


            Return RedirectToAction("FixtureIndex")
        End Function

    End Class
End Namespace