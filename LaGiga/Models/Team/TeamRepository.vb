Imports AutoMapper
Imports LaGiga.Helpers

Public Class TeamRepository
    Inherits StandardFunctions
    Implements ITeamRepository

    Public Function ListTeams1(ByVal TeamTypeID As Integer?, ByVal searchValue As String, ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As IEnumerable(Of Team) Implements ITeamRepository.ListTeams1
        Dim query = From c In db.Teams Where c.TeamTypeID = TeamTypeID And c.TeamName.Contains(searchValue) And c.TeamName <> "None" Order By c.TeamName

        If pageSize.HasValue And pageIndex.HasValue Then
            Return query.Skip(Math.Abs((CDec(pageIndex - 1))) * pageSize).Take(pageSize).ToList()
        End If
        Return query.ToList()
    End Function

    Public Function ListTeams(ByVal pageSize As Integer?, ByVal pageIndex As Integer?, ByVal searchValue As String) As IEnumerable(Of Team) Implements ITeamRepository.ListTeams
        Dim query = From c In db.Teams Where c.TeamName.Contains(searchValue) And c.TeamName <> "None" Order By c.TeamName
        If pageSize.HasValue And pageIndex.HasValue Then
            Return query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList()
        End If
        Return query.ToList()
    End Function

    Public Function TeamsCount(ByVal TeamTypeID As Integer, ByVal searchValue As String) Implements ITeamRepository.TeamsCount
        If searchValue = "" Then
            Return (From c In db.Teams Where c.TeamTypeID = TeamTypeID).Count()
        End If
        Return (From c In db.Teams Where c.TeamTypeID = TeamTypeID And c.TeamName.Contains(searchValue)).Count()
    End Function

    Public Function TeamsCount(ByVal searchValue As String) Implements ITeamRepository.TeamsCount
        If searchValue = "" Then
            Return (From c In db.Teams).Count()
        End If
        Return (From c In db.Teams Where c.TeamName.Contains(searchValue)).Count()
    End Function

    Public Function CreateTeam(ByVal TeamToCreate As Team) As Team Implements ITeamRepository.CreateTeam
        db.Teams.InsertOnSubmit(TeamToCreate)
        db.SubmitChanges()
        Return TeamToCreate
    End Function

    Public Function EditTeam(ByVal TeamToEdit As Team) As Team Implements ITeamRepository.EditTeam
        Dim original = (From team In db.Teams Where team.TeamID = TeamToEdit.TeamID).Single()
        original.TeamName = TeamToEdit.TeamName
        original.TeamTypeID = TeamToEdit.TeamTypeID
        original.InActive = TeamToEdit.InActive
        db.SubmitChanges()
        Return TeamToEdit
    End Function

    Public Sub DeleteTeam(ByVal TeamToDelete As Team) Implements ITeamRepository.DeleteTeam
        'Dim deleteThisTeam = From c In db.Teams Where c.TeamID = TeamToDelete.TeamID
        db.Teams.DeleteOnSubmit(TeamToDelete)
        db.SubmitChanges()
    End Sub


    Public Function GetTeam(ByVal id As Integer) As Team Implements ITeamRepository.GetTeam
        Dim oTeam = (From c In db.Teams _
          Where c.TeamID = id _
          Select c).FirstOrDefault()
        Return oTeam
    End Function

    Public Function ListNational() As IEnumerable(Of Team) Implements ITeamRepository.ListNational
        Dim GroupByNat = From c In db.Teams _
                         Where c.TeamTypeID = 1 _
                         Select c
        Return GroupByNat.ToList()
    End Function

    Public Function ListClubTeams() As List(Of Team) Implements ITeamRepository.ListClubTeams
        Return Helpers.GetAllClubTeams()
    End Function

    Public Function ListTeamTypes() As IEnumerable(Of TeamType) Implements ITeamRepository.ListTeamTypes
        Return From o As Helpers.TeamType In [Enum].GetValues(GetType(Helpers.TeamType))
    End Function

    Public Function GetTeamType(ByVal TeamTypeID As Integer?) As TeamType Implements ITeamRepository.GetTeamType
        Return (From o As TeamType In [Enum].GetValues(GetType(TeamType)) Where o = TeamTypeID).FirstOrDefault()
    End Function

    Public Function getTeamTypeDDList() As IEnumerable(Of SelectListItem) Implements ITeamRepository.getTeamTypeDDList

        Return From teamType In New TeamRepository().ListTeamTypes()
                   Select New SelectListItem() With {
                       .Text = [Enum].GetName(GetType(Helpers.TeamType), teamType),
                       .Value = teamType
                   }
    End Function

    Function getTeamTypeDDList(ByVal TeamID As Integer) As IEnumerable(Of SelectListItem) Implements ITeamRepository.getTeamTypeDDList

        Dim selectedTeam = (From o In db.Teams Where o.TeamID.Equals(TeamID)).FirstOrDefault()

        Return From teamType In New TeamRepository().ListTeamTypes()
                   Select New SelectListItem() With {
                       .Text = [Enum].GetName(GetType(Helpers.TeamType), teamType),
                       .Value = teamType,
                       .Selected = teamType = selectedTeam.TeamTypeID
                   }
    End Function
End Class
