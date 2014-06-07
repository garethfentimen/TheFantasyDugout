Imports LaGiga.Helpers

Public Class PlayerHomeRepository
    Inherits StandardFunctions
    Implements IPlayerHomeRepository

    Private oPEM As New PlayerEventModel

    Function GetUserTeam(ByVal Username As String) As UserTeam Implements IPlayerHomeRepository.GetUserTeam
        Return GetUserTeamByUserName(Username)
    End Function

    Function GetCurrentWeek() As Week Implements IPlayerHomeRepository.GetCurrentWeek
        Return Helpers.CurrentWeek()
    End Function

    Function GetUserPlayersOrderByPosition(ByVal UserTeamID As Integer) As List(Of UserPlayer) Implements IPlayerHomeRepository.GetUserPlayersOrderByPosition
        '' need to check competition
        Return (From c In db.UserPlayers Where c.UserTeamID = UserTeamID _
                    AndAlso c.CompetitionID.Equals(Helpers.CurrentCompetition().CompetitionID) _
                Order By c.Player.PositionID).ToList
    End Function

    Function CreateWeekUserPlayer(ByVal WeekUserPlayers As List(Of WeekUserPlayer)) As IEnumerable(Of WeekUserPlayer) Implements IPlayerHomeRepository.CreateWeekUserPlayer

        db.WeekUserPlayers.InsertAllOnSubmit(WeekUserPlayers)
        db.SubmitChanges()
        Return WeekUserPlayers
    End Function

    Function GetUserPlayerWeek(ByVal WeekID As Integer) As IEnumerable(Of WeekUserPlayer) Implements IPlayerHomeRepository.GetUserPlayerWeek
        Dim oquery = From c In db.WeekUserPlayers Where c.WeekID = WeekID
        If oquery.Count = 0 Then
            oquery = Nothing
        End If
        Return oquery
    End Function

    Function GetMostRecentWeek() As Week Implements IPlayerHomeRepository.GetMostRecentWeek
        Dim Recentweek = (From c In db.Weeks Where c.CurrentWeek = False And c.WeekID = Helpers.CurrentWeek.WeekID - 1 Select c).FirstOrDefault()
        Dim SecondRecentweek = (From c In db.Weeks Where c.CurrentWeek = False And c.WeekID = GetCurrentWeek.WeekID - 2 Order By c.WeekID Descending Select c).FirstOrDefault()

        Return IIf(IsNothing(Recentweek), SecondRecentweek, Recentweek)
    End Function

    Function GetUserPlayersByWeek(ByVal UserTeamID As Integer, ByVal WeekID As Integer) As IEnumerable(Of WeekUserPlayer) Implements IPlayerHomeRepository.GetUserPlayersByWeek
        Return (From c In db.WeekUserPlayers
                Where c.UserTeamID = UserTeamID _
                    AndAlso c.WeekID = WeekID _
                    AndAlso c.Draft = False Order By c.Player.PositionID)
    End Function

    Function GetDraftUserPlayersByWeek(ByVal UserTeamID As Integer, ByVal WeekID As Integer) As IEnumerable(Of WeekUserPlayer) Implements IPlayerHomeRepository.GetDraftUserPlayersByWeek
        Return (From c In db.WeekUserPlayers Where c.UserTeamID = UserTeamID And c.WeekID = WeekID And c.Draft = True Order By c.Player.PositionID)
    End Function

    Function GetUserResultTable(ByVal UserTeamID As Integer) As List(Of Result) Implements IPlayerHomeRepository.getUserResultTable

        Dim oResultCollection As New List(Of Result)
        Dim oCurrentWeek = Helpers.CurrentWeek()

        Dim firstTeamPlayers = (From o In db.WeekUserPlayers Where o.UserTeamID = UserTeamID And o.StatusID = Helpers.PlayingType.Playing _
                                 And o.Draft = 0 And o.WeekID = oCurrentWeek.WeekID Order By o.Player.PositionID).ToList()

        Dim oCheckFixturesPresent = (From c In db.Fixtures Where c.WeekID = oCurrentWeek.WeekID)

        ' if this is after the picking time then populate weekuserplayer with last weeks players
        If firstTeamPlayers.Count = 0 And oCheckFixturesPresent.Count > 0 And ((Now.DayOfWeek = DayOfWeek.Saturday And Now.Hour > 12) _
                                                                                Or Now.DayOfWeek = DayOfWeek.Sunday Or Now.DayOfWeek = DayOfWeek.Monday) Then
            'team has not been picked so pick last weeks team.
            Dim oLastWeek = (From c In db.Weeks Where c.WeekNo = oCurrentWeek.WeekNo - 1 AndAlso c.CompetitionID.Equals(Helpers.CurrentCompetition.CompetitionID)).FirstOrDefault
            If Not IsNothing(oLastWeek) Then
                Dim oLastWeeksSquad = (From c In db.WeekUserPlayers Where c.UserTeamID = UserTeamID And c.Draft = 0 And c.WeekID = oLastWeek.WeekID Order By c.Player.PositionID).ToList()
                'change the weekID
                Dim oSquadList As New List(Of WeekUserPlayer)

                For Each oLastSquad In oLastWeeksSquad
                    Dim oSquad As New WeekUserPlayer
                    oSquad.WeekID = oCurrentWeek.WeekID
                    oSquad.Draft = oLastSquad.Draft
                    oSquad.PlayerID = oLastSquad.PlayerID
                    oSquad.StatusID = oLastSquad.StatusID
                    oSquad.UserTeamID = oLastSquad.UserTeamID
                    oSquadList.Add(oSquad)
                Next oLastSquad
                CreateWeekUserPlayer(oSquadList)
                firstTeamPlayers = (From o In db.WeekUserPlayers Where o.UserTeamID = UserTeamID And o.StatusID = Helpers.PlayingType.Playing And o.Draft = 0 And o.WeekID = oCurrentWeek.WeekID Order By o.Player.PositionID).ToList()
            End If
        End If

        Return GetUserResult(UserTeamID, firstTeamPlayers)

    End Function

    Function GetUserTeamByID(ByVal UserTeamID As Integer) As UserTeam Implements IPlayerHomeRepository.GetUserTeamByID
        Return (From c In db.UserTeams Where c.UserTeamID = UserTeamID Select c).FirstOrDefault()
    End Function

    Function GetUserPlayersByLastWeekNo(ByVal UserTeamID As Integer, ByVal WeekNo As Integer) As List(Of WeekUserPlayer) Implements IPlayerHomeRepository.GetUserPlayersByLastWeekNo
        Dim oWeek = (From c In db.Weeks Where c.WeekNo = WeekNo AndAlso c.CompetitionID.Equals(Helpers.CurrentCompetition.CompetitionID)).FirstOrDefault()

        Dim rtnLastWeekUserPlayers As New List(Of WeekUserPlayer)
        If Not IsNothing(oWeek) Then
            rtnLastWeekUserPlayers = (From c In db.WeekUserPlayers Where c.WeekID = oWeek.WeekID _
                            And c.UserTeamID = UserTeamID And c.Draft = 0 _
                            Order By c.Player.PositionID).ToList
        End If
        Return rtnLastWeekUserPlayers
    End Function

    Function GetOppositionUserTeam(ByVal UserTeamID As Integer, ByVal WeekNo As Integer) As UserTeam Implements IPlayerHomeRepository.GetOppositionUserTeam
        Dim oFixtureCalc = (From c In db.UserFixtureCalculations Where c.UserTeamIDA = UserTeamID And c.WeekNo = WeekNo).FirstOrDefault()
        Return GetUserTeamByID(oFixtureCalc.UserTeamIDB)
    End Function

    Function getAllfixtures(ByVal WeekNo As Integer, ByVal UserGroup As UserGroup) As List(Of UserFixtureCalculation) Implements IPlayerHomeRepository.GetAllfixtures
        Dim ofixtures As New List(Of UserFixtureCalculation)
        Dim oUserTeams As List(Of UserTeam) = (From c In db.UserTeams Where c.UserGroupID.Equals(UserGroup.UserGroupID)).ToList
        For Each oUserTeam As UserTeam In oUserTeams
            Dim o_lUserTeam = oUserTeam
            Dim ofixture = (From c In db.UserFixtureCalculations Where c.WeekNo = WeekNo And c.UserTeamIDA = o_lUserTeam.UserTeamID AndAlso c.NoPlayers.Equals(UserGroup.NoPlayers)).FirstOrDefault()
            If Not IsNothing(ofixture) Then
                ofixtures.Add(ofixture)
            End If
        Next oUserTeam
        Return ofixtures
    End Function

    Function GetSeasonResult(ByVal UserGroup As UserGroup) As List(Of SeasonResult) Implements IPlayerHomeRepository.GetSeasonResult

        Dim oUserTeams As List(Of UserTeam) = (From c In db.UserTeams Where c.UserGroupID.Equals(UserGroup.UserGroupID)).ToList
        Dim oPrevWeek As Week = (From c In db.Weeks Where c.WeekNo = Helpers.CurrentWeek().WeekNo - 1 AndAlso c.CompetitionID.Equals(Helpers.CurrentCompetition.CompetitionID)).FirstOrDefault()
        Dim oSeasonResults As New List(Of SeasonResult)

        Dim weekIDs As List(Of Integer) = (From o As Week In db.Weeks Where o.CompetitionID.Equals(Helpers.CurrentCompetition.CompetitionID) Select o.WeekID).ToList

        If Not IsNothing(oPrevWeek) Then
            oSeasonResults = (From c As SeasonResult In db.SeasonResults Where c.WeekID = oPrevWeek.WeekID AndAlso c.GoalsScored > 0 _
                              AndAlso weekIDs.Contains(c.WeekID)).ToList()


            If oSeasonResults.Count = 0 Then
                For Each userTeam In oUserTeams
                    Dim oSeasonResult As New SeasonResult
                    Dim Points As Double = 0.0
                    Dim PointsScored As Double = 0.0
                    Dim oAllSeasonResults As List(Of SeasonResult) = (From c In db.SeasonResults Where c.UserTeamID = userTeam.UserTeamID AndAlso weekIDs.Contains(c.WeekID) And c.GoalsScored = 0).ToList

                    If oAllSeasonResults.Count > 0 Then
                        For Each oSR As SeasonResult In oAllSeasonResults
                            If oSR.Won = 1 Then
                                oSeasonResult.Points += 2
                            End If
                            If oSR.Drawn = 1 Then
                                oSeasonResult.Points += 1
                            End If
                            oSeasonResult.Won += oSR.Won
                            oSeasonResult.Drawn += oSR.Drawn
                            oSeasonResult.Lost += oSR.Lost
                            oSeasonResult.GoalsScored += oSR.Points
                        Next oSR
                        oSeasonResult.UserTeamID = userTeam.UserTeamID
                        oSeasonResult.WeekID = oPrevWeek.WeekID
                        db.SeasonResults.InsertOnSubmit(oSeasonResult)
                        db.SubmitChanges()
                    End If
                Next
            End If
            oSeasonResults = (From c In db.SeasonResults Where c.WeekID = oPrevWeek.WeekID And c.GoalsScored > 0 AndAlso weekIDs.Contains(c.WeekID) Order By c.Points Descending, c.GoalsScored Descending).ToList()
        End If

        Return oSeasonResults
    End Function

    Function GetPlayerEventModel(ByVal iPageIndex As Integer?, ByVal iTeamID As Integer?, ByVal PositionID As Integer?, ByVal sSearchName As String) As List(Of PlayerEventModel) Implements IPlayerHomeRepository.GetPlayerEventModel
        Return oPEM.PopulateClassProperties(iPageIndex, iTeamID, PositionID, sSearchName)
    End Function

    Function GetPlayerEventModelCount() As Integer Implements IPlayerHomeRepository.GetPlayerEventModelCount
        Return Math.Floor((oPEM.c_iPlayerCount / oPEM.c_iPageSize) + 0.9)
    End Function

    Function GetPositions() As List(Of SelectListItem) Implements IPlayerHomeRepository.GetPositions
        Return (From o As Integer In [Enum].GetValues(GetType(Position)).Cast(Of Integer)()
                        Select New SelectListItem With {.Value = o, .Text = [Enum].GetName(GetType(Position), o)}
                        )
    End Function

    Function GetPlayer(ByVal PlayerID As Integer) As Player Implements IPlayerHomeRepository.GetPlayer
        Return (From c In db.Players Where c.PlayerID = PlayerID).FirstOrDefault
    End Function

    Function GetUserGroupByID(ByVal UserGroupID As Integer) As UserGroup Implements IPlayerHomeRepository.GetUserGroupByID
        Return (From o As UserGroup In db.UserGroups Where o.UserGroupID.Equals(UserGroupID)).FirstOrDefault
    End Function
End Class
