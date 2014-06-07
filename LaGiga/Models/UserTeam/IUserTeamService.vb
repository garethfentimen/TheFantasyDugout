Public Interface IUserTeamService

    Function CreateUserTeam(ByVal UserTeamToCreate As UserTeam) As Boolean
    Function DeleteUserTeam(ByVal UserTeamToDelete As UserTeam) As Boolean
    Function EditUserTeam(ByVal UserTeamToEdit As UserTeam) As Boolean
    Function GetUserTeam(ByVal id As Integer) As UserTeam
    Function ListUserTeam(ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As IEnumerable(Of UserTeam)

    Function UserGroupDDList() As IQueryable(Of SelectListItem)
    Function UserTeamCount() As Integer

    Function GetAllCompetitions() As List(Of Competition)

End Interface
