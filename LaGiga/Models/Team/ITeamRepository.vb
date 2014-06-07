Public Interface ITeamRepository

    Function CreateTeam(ByVal TeamToCreate As Team) As Team
    Sub DeleteTeam(ByVal TeamToDelete As Team)
    Function EditTeam(ByVal TeamToEdit As Team) As Team
    Function GetTeam(ByVal id As Integer) As Team
    Function ListTeams1(ByVal TeamTypeID As Integer?, ByVal searchValue As String, ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As IEnumerable(Of Team)
    Function ListTeams(ByVal pageSize As Integer?, ByVal pageIndex As Integer?, ByVal searchValue As String) As IEnumerable(Of Team)
    Function TeamsCount(ByVal TeamTypeID As Integer, ByVal searchValue As String)
    Function TeamsCount(ByVal searchValue As String)

    ' Group methods
    Function ListTeamTypes() As IEnumerable(Of Helpers.TeamType)
    Function ListNational() As IEnumerable(Of Team)
    Function ListClubTeams() As List(Of Team)
    Function GetTeamType(ByVal TeamTypeID As Integer?) As Helpers.TeamType

    'Viewdata Methods
    Function getTeamTypeDDList() As IEnumerable(Of SelectListItem)
    Function getTeamTypeDDList(ByVal TeamID As Integer) As IEnumerable(Of SelectListItem)

End Interface
