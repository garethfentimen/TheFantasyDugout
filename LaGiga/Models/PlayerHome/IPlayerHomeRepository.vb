Public Interface IPlayerHomeRepository

    Function getUserResultTable(ByVal UserTeamID As Integer) As List(Of Result)

    Function GetUserTeam(ByVal Username As String) As UserTeam
    Function GetCurrentWeek() As Week

    Function GetUserPlayersOrderByPosition(ByVal UserTeamID As Integer) As List(Of UserPlayer)

    Function CreateWeekUserPlayer(ByVal WeekUserPlayers As List(Of WeekUserPlayer)) As IEnumerable(Of WeekUserPlayer)

    Function GetUserPlayerWeek(ByVal WeekID As Integer) As IEnumerable(Of WeekUserPlayer)

    Function GetMostRecentWeek() As Week
    Function GetUserPlayersByWeek(ByVal UserTeamID As Integer, ByVal WeekID As Integer) As IEnumerable(Of WeekUserPlayer)

    Function GetUserTeamByID(ByVal UserTeamID As Integer) As UserTeam
    Function GetOppositionUserTeam(ByVal UserTeamID As Integer, ByVal WeekNo As Integer) As UserTeam

    Function GetDraftUserPlayersByWeek(ByVal UserTeamID As Integer, ByVal WeekID As Integer) As IEnumerable(Of WeekUserPlayer)
    Function GetUserPlayersByLastWeekNo(ByVal UserTeamID As Integer, ByVal WeekNo As Integer) As List(Of WeekUserPlayer)

    Function GetAllfixtures(ByVal WeekNo As Integer, ByVal UserGroup As UserGroup) As List(Of UserFixtureCalculation)

    Function GetSeasonResult(ByVal UserGroup As UserGroup) As List(Of SeasonResult)

    Function GetPlayerEventModel(ByVal iPageIndex As Integer?, ByVal iTeamID As Integer?, ByVal PositionID As Integer?, ByVal sSearchName As String) As List(Of PlayerEventModel)

    Function GetPlayerEventModelCount() As Integer

    Function GetPositions() As List(Of SelectListItem)

    Function GetPlayer(ByVal PlayerID As Integer) As Player

    Function GetUserGroupByID(ByVal UserGroupID As Integer) As UserGroup

End Interface
