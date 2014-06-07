Public Interface IPlayerHomeService

    Function getUserResultTable(ByVal UserTeamID As Integer) As List(Of Result)

    Function GetCurrentWeek() As Week
    Function GetMostRecentWeek() As Week

    Function GetUserTeam(ByVal Username As String) As UserTeam
    Function GetUserTeamByID(ByVal UserTeamID As Integer) As UserTeam
    Function GetUserPlayersByWeek(ByVal UserTeamID As Integer, ByVal WeekID As Integer) As IEnumerable(Of WeekUserPlayer)

    Function GetUserPlayersOrderByPosition(ByVal UserTeamID As Integer) As List(Of UserPlayer)

    Function CreateUserPlayerWeek(ByVal WeekUserPlayers As List(Of WeekUserPlayer)) As Boolean

    Function GetDraftUserPlayersByWeek(ByVal UserTeamID As Integer, ByVal WeekID As Integer) As IEnumerable(Of WeekUserPlayer)

    Function GetUserPlayersByLastWeekNo(ByVal UserTeamID As Integer, ByVal WeekNo As Integer) As List(Of WeekUserPlayer)

    Function GetOppositionUserTeam(ByVal UserTeamID As Integer, ByVal WeekNo As Integer) As UserTeam

    Function getAllfixtures(ByVal WeekNo As Integer, ByVal UserGroup As UserGroup) As List(Of UserFixtureCalculation)
    Function GetSeasonResult(ByVal UserGroup As UserGroup) As List(Of SeasonResult)

    Function GetPlayerEventModel(ByVal iPageIndex As Integer?, ByVal iTeamID As Integer?, ByVal PositionID As Integer?, ByVal sSearchName As String) As List(Of PlayerEventModel)
    Function GetPositions() As List(Of SelectListItem)

    Function GetPlayerEventModelCount() As Integer

    Function GetUserGroupByID(ByVal UserGroupID As Integer) As UserGroup

End Interface
