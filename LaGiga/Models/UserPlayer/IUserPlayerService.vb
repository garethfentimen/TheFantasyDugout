
Public Interface IUserPlayerService

    Function ListUserPlayer(ByVal UserTeamID As Integer?, ByVal competitionID As Integer, ByVal pageSize As Integer, ByVal pageIndex As Integer?) As IEnumerable(Of UserPlayer)

    Function UserPlayerCount(ByVal UserTeamID As Integer?, ByVal CompetitionID As Integer) As Integer

    Function GetClubTeamsDD() As IQueryable(Of SelectListItem)

    Function getPlayersAtClubsDD(ByVal TeamID As Integer) As IEnumerable(Of SelectListItem)

    Function GetUserGroup(ByVal UserTeamID As Integer) As UserTeam

    Function CreateUserPlayer(ByVal UserPlayerToCreate As UserPlayer) As Boolean

    Function DeleteUserPlayer(ByVal UserPlayerToDelete As UserPlayer) As Boolean

    Function GetUserPlayer(ByVal UserPlayerID As Integer?) As UserPlayer

    Function CreateWeekUserPlayer(ByVal UserPlayerToCreate As UserPlayer) As Boolean

End Interface
