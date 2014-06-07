Imports LaGiga.Formations

Public Class PlayerHomeService
    Implements IPlayerHomeService

    Private _validationDictionary As IValidationDictionary
    Private _repository As IPlayerHomeRepository

    Public Sub New(ByVal validationDictionary As IValidationDictionary)
        Me.New(validationDictionary, New PlayerHomeRepository())
    End Sub

    Public Sub New(ByVal validationDictionary As IValidationDictionary, ByVal repository As IPlayerHomeRepository)
        _validationDictionary = validationDictionary
        _repository = repository
    End Sub

    Function GetUserTeam(ByVal Username As String) As UserTeam Implements IPlayerHomeService.GetUserTeam
        Return _repository.GetUserTeam(Username)
    End Function

    Function GetCurrentWeek() As Week Implements IPlayerHomeService.GetCurrentWeek
        Return _repository.GetCurrentWeek()
    End Function

    Function GetUserPlayersOrderByPosition(ByVal UserTeamID As Integer) As List(Of UserPlayer) Implements IPlayerHomeService.GetUserPlayersOrderByPosition
        Return _repository.GetUserPlayersOrderByPosition(UserTeamID)
    End Function

    Function CreateUserPlayerWeek(ByVal WeekUserPlayers As List(Of WeekUserPlayer)) As Boolean Implements IPlayerHomeService.CreateUserPlayerWeek

        Dim FormtationTest As New FormationFinder(_validationDictionary, WeekUserPlayers)

        If FormtationTest.FormationType <> 0 AndAlso FormtationTest.ValidationDictionary.IsValid Then
            Try
                _repository.CreateWeekUserPlayer(WeekUserPlayers)
            Catch
                Return False
            End Try
            Return True
        Else
            Return False
        End If
    End Function

    Function GetMostRecentWeek() As Week Implements IPlayerHomeService.GetMostRecentWeek
        Return _repository.GetMostRecentWeek()
    End Function

    Function GetUserPlayersByWeek(ByVal UserTeamID As Integer, ByVal WeekID As Integer) As IEnumerable(Of WeekUserPlayer) Implements IPlayerHomeService.GetUserPlayersByWeek
        Return _repository.GetUserPlayersByWeek(UserTeamID, WeekID)
    End Function

    Function getUserResultTable(ByVal UserTeamID As Integer) As List(Of Result) Implements IPlayerHomeService.getUserResultTable
        Return _repository.getUserResultTable(UserTeamID)
    End Function

    Function GetUserTeamByID(ByVal UserTeamID As Integer) As UserTeam Implements IPlayerHomeService.GetUserTeamByID
        Return _repository.GetUserTeamByID(UserTeamID)
    End Function

    Function GetDraftUserPlayersByWeek(ByVal UserTeamID As Integer, ByVal WeekID As Integer) As IEnumerable(Of WeekUserPlayer) Implements IPlayerHomeService.GetDraftUserPlayersByWeek
        Return _repository.GetDraftUserPlayersByWeek(UserTeamID, WeekID)
    End Function

    Function GetUserPlayersByLastWeekNo(ByVal UserTeamID As Integer, ByVal WeekNo As Integer) As List(Of WeekUserPlayer) Implements IPlayerHomeService.GetUserPlayersByLastWeekNo
        Return _repository.GetUserPlayersByLastWeekNo(UserTeamID, WeekNo)
    End Function

    Function GetOppositionUserTeam(ByVal UserTeamID As Integer, ByVal WeekNo As Integer) As UserTeam Implements IPlayerHomeService.GetOppositionUserTeam
        Return _repository.GetOppositionUserTeam(UserTeamID, WeekNo)
    End Function

    Function getAllfixtures(ByVal WeekNo As Integer, ByVal UserGroup As UserGroup) As List(Of UserFixtureCalculation) Implements IPlayerHomeService.getAllfixtures
        Return _repository.GetAllfixtures(WeekNo, UserGroup)
    End Function

    Function GetSeasonResult(ByVal UserGroup As UserGroup) As List(Of SeasonResult) Implements IPlayerHomeService.GetSeasonResult
        Return _repository.GetSeasonResult(UserGroup)
    End Function

    Function GetPlayerEventModel(ByVal iPageIndex As Integer?, ByVal iTeamID As Integer?, ByVal PositionID As Integer?, ByVal sSearchName As String) As List(Of PlayerEventModel) Implements IPlayerHomeService.GetPlayerEventModel
        Return _repository.GetPlayerEventModel(iPageIndex, iTeamID, PositionID, sSearchName)
    End Function

    Function GetPlayerEventModelCount() As Integer Implements IPlayerHomeService.GetPlayerEventModelCount
        Return _repository.GetPlayerEventModelCount()
    End Function

    Function GetPositions() As List(Of SelectListItem) Implements IPlayerHomeService.GetPositions
        Return _repository.GetPositions()
    End Function

    Function GetUserGroupByID(ByVal UserGroupID As Integer) As UserGroup Implements IPlayerHomeService.GetUserGroupByID
        Return _repository.GetUserGroupByID(UserGroupID)
    End Function

End Class
