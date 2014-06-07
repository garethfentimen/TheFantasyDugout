Public Interface IUserTeamRepository

    Function CreateTeam(ByVal TeamToCreate As UserTeam) As UserTeam
    Sub DeleteUserTeam(ByVal TeamToDelete As UserTeam)
    Function EditUserTeam(ByVal TeamToEdit As UserTeam) As UserTeam
    Function GetUserTeam(ByVal id As Integer) As UserTeam
    Function ListUserTeam(ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As IEnumerable(Of UserTeam)

    Function UserGroupDDList() As IQueryable(Of SelectListItem)
    Function UserTeamCount() As Integer

    Function GetAllCompetitions() As List(Of Competition)

End Interface
