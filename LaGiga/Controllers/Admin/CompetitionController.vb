Namespace LaGiga
    Public Class CompetitionController
        Inherits System.Web.Mvc.Controller

        Public _repository As New CompetitionRepository

        '
        ' GET: /Competition

        Function Index() As ActionResult
            Return View(_repository.GetAllCompetitions)
        End Function

        '
        ' GET: /Competition/Create

        Function Create() As ActionResult

            Dim CreateCompetitionViewModel = New CreateCompetitionViewModel With {
                    .TeamTypeID = Helpers.TeamType.Club,
                    .TeamTypes = Helpers.GetDefaultTeamTypes()
                }

            Return View(CreateCompetitionViewModel)
        End Function

        '
        ' POST: /Competition/Create

        <HttpPost()> _
        Function Create(ByVal comp As Competition) As ActionResult
            Try

                _repository.CreateCompetition(comp)

                Return RedirectToAction("Index")
            Catch
                Return View(_repository.GetAllCompetitions)
            End Try
        End Function
        
        '
        ' GET: /Competition/Edit/5

        Function Edit(ByVal id As Integer) As ActionResult
            Return View(_repository.GetCompetitionByID(id))
        End Function

        '
        ' POST: /Competition/Edit/5

        <HttpPost()> _
        Function Edit(ByVal competitionToEdit As Competition) As ActionResult
            Try
                _repository.EditCompetition(competitionToEdit)

                Return RedirectToAction("Index")
            Catch
                Return View(_repository.GetAllCompetitions)
            End Try
        End Function


        '
        ' POST: /Competition/Delete/5

        <AcceptVerbs(HttpVerbs.Delete), ActionName("Delete")> _
        Function Delete(ByVal id As Integer) As ActionResult
            Try
                ' TODO: Add delete logic here

                Dim comp As Competition = _repository.GetCompetitionByID(id)

                _repository.DeleteCompetition(comp)


                If Request.IsAjaxRequest() Then
                    Return PartialView("competitionList", _repository.GetAllCompetitions)
                End If
            Catch
                Return View(_repository.GetAllCompetitions)
            End Try
            Return View(_repository.GetAllCompetitions)
        End Function
    End Class
End Namespace