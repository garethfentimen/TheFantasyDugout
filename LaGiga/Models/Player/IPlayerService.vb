Imports LaGiga.Helpers

Public Interface IPlayerService

    Function CreatePlayer(ByVal PlayerToCreate As Player) As Boolean
    Function DeletePlayer(ByVal PlayerToDelete As Player) As Boolean
    Function EditPlayer(ByVal PlayerToEdit As Player) As Boolean
    Function GetPlayer(ByVal id As Integer) As Player
    Function ListPlayer(ByVal PositionID As Integer?, ByVal searchValue As String, ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As IEnumerable(Of Player)
    Function ListPlayer(ByVal pageSize As Integer?, ByVal pageIndex As Integer?, ByVal searchValue As String) As IEnumerable(Of Player)

    Function ListPositions() As IEnumerable(Of Integer)
    Function PlayerCount(ByVal PositionID As Integer?, ByVal searchValue As String) As Integer
    Function PlayerCount(ByVal searchValue As String) As Integer

    Function ListPositionsDD() As IEnumerable(Of SelectListItem)

    Function GetNationalTeams() As IQueryable(Of SelectListItem)
    Function GetClubTeams() As List(Of SelectListItem)
    Function GetClubTeamsCreate() As IQueryable(Of SelectListItem)

    Function ListPlayerByTeam(ByVal TeamID As Integer?, ByVal searchValue As String, ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As IEnumerable(Of Player)
    Function CountPlayerByTeam(ByVal TeamID As Integer?, ByVal searchValue As String) As Integer

    '' Transfer methods
    Function CreateTransfer(ByVal TransferToCreate As Transfer, ByVal NewTeamID As Integer) As Boolean

End Interface
