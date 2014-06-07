Public Interface IUserPlayerRepository

    Function getUserPlayer(ByVal UserPlayerID As Integer?) As UserPlayer

    Function ListUserPlayer(ByVal UserTeamID As Integer?, ByVal CompetitionID As Integer, ByVal pageSize As Integer, ByVal pageIndex As Integer?) As IEnumerable(Of UserPlayer)
    Function UserPlayerCount(ByVal UserTeamID As Integer?, ByVal CompetitionID As Integer) As Integer

    Function GetUserGroup(ByVal UserTeamID As Integer) As UserTeam

    Function GetClubTeamsDD() As System.Linq.IQueryable(Of SelectListItem)
    Function getPlayersAtClubsDD(ByVal TeamID As Integer) As IEnumerable(Of SelectListItem)

    Function CreateUserPlayer(ByVal UserPlayerToCreate As UserPlayer) As UserPlayer

    Sub DeleteUserPlayer(ByVal UserPlayerToDelete As UserPlayer)

    Function CreateWeekUserPlayer(ByVal UserPlayerToCreate As UserPlayer) As WeekUserPlayer

End Interface
