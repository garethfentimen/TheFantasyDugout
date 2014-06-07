Public Interface ITeamService

    ' EventType methods
    Function CreateTeam(ByVal TeamToCreate As Team) As Boolean
    Function DeleteTeam(ByVal TeamToDelete As Team) As Boolean
    Function EditTeam(ByVal TeamToEdit As Team) As Boolean
    Function GetTeam(ByVal id As Integer) As Team
    Function ListTeams1(ByVal TeamTypeID As Integer?, ByVal searchValue As String, ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As IEnumerable(Of Team)
    Function ListTeams(ByVal pageSize As Integer?, ByVal pageIndex As Integer?, ByVal searchValue As String) As IEnumerable(Of Team)

    Function TeamsCount(ByVal TeamTypeID As Integer, ByVal searchValue As String)
    Function TeamsCount(ByVal searchValue As String)

    ' Group methods
    Function ListTeamTypes() As IEnumerable(Of Helpers.TeamType)
    Function ListNational() As IEnumerable(Of Team)
    Function ListClub() As IEnumerable(Of Team)
    Function GetTeamType(ByVal TeamTypeID As Integer?) As Helpers.TeamType

    'Viewdata Methods
    Function getTeamTypeDDList() As IEnumerable(Of SelectListItem)
    Function getTeamTypeDDList(ByVal TeamID As Integer) As IEnumerable(Of SelectListItem)

End Interface
