Public Class UserTeamRepository
    Inherits StandardFunctions
    Implements IUserTeamRepository

    Public Function CreateTeam(ByVal UserTeamToCreate As UserTeam) As UserTeam Implements IUserTeamRepository.CreateTeam
        db.UserTeams.InsertOnSubmit(UserTeamToCreate)
        db.SubmitChanges()
        Return UserTeamToCreate
    End Function

    Public Sub DeleteUserTeam(ByVal UserTeamToDelete As UserTeam) Implements IUserTeamRepository.DeleteUserTeam
        db.UserTeams.DeleteOnSubmit(UserTeamToDelete)
        db.SubmitChanges()
    End Sub

    Public Function EditUserTeam(ByVal UserTeamToEdit As UserTeam) As UserTeam Implements IUserTeamRepository.EditUserTeam
        Dim original = (From c In db.UserTeams Where c.UserTeamID = UserTeamToEdit.UserTeamID).Single()
        original.Name = UserTeamToEdit.Name
        original.UserId = UserTeamToEdit.UserId
        original.UserGroupID = UserTeamToEdit.UserGroupID
        db.SubmitChanges()
        Return UserTeamToEdit
    End Function

    Public Function GetUserTeam(ByVal id As Integer) As UserTeam Implements IUserTeamRepository.GetUserTeam
        Return (From c In db.UserTeams Where c.UserTeamID = id
          Select c).FirstOrDefault()
    End Function

    Public Function ListUserTeam(ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As IEnumerable(Of UserTeam) Implements IUserTeamRepository.ListUserTeam
        Dim query = From c In db.UserTeams Order By c.Name

        If pageSize.HasValue And pageIndex.HasValue Then
            Return query.Skip(Math.Abs((CDec(pageIndex - 1))) * pageSize).Take(pageSize).ToList()
        End If
        Return query.ToList()
    End Function

    Public Function UserTeamCount() As Integer Implements IUserTeamRepository.UserTeamCount
        Return (From c In db.UserTeams).Count()
    End Function

    Function UserGroupDDList() As IQueryable(Of SelectListItem) Implements IUserTeamRepository.UserGroupDDList
        Return (From c In db.UserGroups Select c = New SelectListItem With
                                                  {.Value = c.UserGroupID, .Text = c.Name})
    End Function

    Function GetAllCompetitions() As List(Of Competition) Implements IUserTeamRepository.GetAllCompetitions
        Return (From o As Competition In db.Competitions Order By o.CompetitionID Descending).ToList
    End Function
End Class
