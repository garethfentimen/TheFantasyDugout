Public Class CompetitionRepository
    Inherits StandardFunctions

    Public Function GetAllCompetitions() As List(Of Competition)

        Return (From o As Competition In db.Competitions Order By o.CurrentCompetition).ToList()

    End Function

    Function GetCompetitionByID(ByVal id As Integer) As Competition
        Return (From o As Competition In db.Competitions Where o.CompetitionID.Equals(id)).FirstOrDefault()
    End Function

    Function GetCurrentCompetitionId() As Integer
        Dim currentComp As Competition = (From o In db.Competitions Where o.CurrentCompetition.HasValue AndAlso o.CurrentCompetition.Equals(True) Select o).FirstOrDefault()
        If IsNothing(currentComp) Then
            currentComp = (From c In db.Competitions Order By c.CompetitionID Descending Select c).FirstOrDefault()
        End If
        Return currentComp.CompetitionID
    End Function

    Sub CreateCompetition(ByVal competitionToCreate As Competition)
        db.Competitions.InsertOnSubmit(competitionToCreate)
        db.SubmitChanges()
    End Sub

    Sub EditCompetition(ByVal competitionToEdit As Competition)

        Dim competition As Competition = (From o As Competition In db.Competitions Where o.CompetitionID.Equals(competitionToEdit.CompetitionID)).FirstOrDefault()

        competition.CurrentCompetition = competitionToEdit.CurrentCompetition
        competition.Name = competitionToEdit.Name
        competition.FromDate = competitionToEdit.FromDate
        competition.ToDate = competitionToEdit.ToDate
        competition.SquadSize = competitionToEdit.SquadSize
        db.SubmitChanges()

    End Sub

    Sub DeleteCompetition(ByVal compToDelete As Competition)
        db.Competitions.DeleteOnSubmit(compToDelete)
        db.SubmitChanges()
    End Sub

End Class
