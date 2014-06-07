Imports LaGiga.Helpers

Public Class AddEventCalculator
    Inherits StandardFunctions

    Sub New()

    End Sub

    Property oFixtureDetail As Fixture
    Property o_lCleanSheetEvent As New List(Of [Event])
    Property o_lConcedeGoalEvent As New List(Of [Event])

    Public Function Calculate(ByVal EventToCreate As [Event], ByVal FixtureEditDetail As FixtureEditModel) As [Event]

        EventToCreate.FixtureID = oFixtureDetail.FixtureID

        Dim player = (From c In db.Players Where c.PlayerID = EventToCreate.PlayerID).FirstOrDefault()

        Dim bCleanSheet As Boolean = False
        If oFixtureDetail.Week.Competition.Name.Contains("Premier League") Then
            If oFixtureDetail.HomeTeamID = player.Team.TeamID And oFixtureDetail.AwayScore = 0 Then
                bCleanSheet = True
            End If

            If oFixtureDetail.AwayTeamID = player.Team.TeamID And oFixtureDetail.HomeScore = 0 Then
                bCleanSheet = True
            End If
        End If

        If oFixtureDetail.Week.Competition.Name.Contains("World Cup") Then
            If oFixtureDetail.HomeTeamID = player.Team1.TeamID And oFixtureDetail.AwayScore = 0 Then
                bCleanSheet = True
            End If
            If oFixtureDetail.AwayTeamID = player.Team1.TeamID And oFixtureDetail.HomeScore = 0 Then
                bCleanSheet = True
            End If
        End If

        Dim sEventName As String = (From c In db.EventTypes Where c.EventTypeID = EventToCreate.EventTypeID Select c.EventName).FirstOrDefault()
        Dim oEventType As EventType = (From c In db.EventTypes Where c.EventName.Contains(sEventName) And c.PositionID = player.PositionID).FirstOrDefault()

        '' Clean Sheet Events if they are a defender or goalkeeper. 
        '' If EventTypeID = "Appearance (start or sub)" master (EventTypeID:12) and if they have played from 0 to 90 or above

        If player.PositionID = Position.GK And EventToCreate.ToMinute >= 90 And EventToCreate.FromMinute = 0 And EventToCreate.EventTypeID = 12 And bCleanSheet = True Then
            Dim oGKCleanSheet = (From c In db.EventTypes Where c.EventName.Contains("Clean Sheet") And c.PositionID = Position.GK).FirstOrDefault()

            Dim oCleanSheetEvent As New [Event]
            oCleanSheetEvent.FromMinute = 200
            oCleanSheetEvent.EventTypeID = oGKCleanSheet.EventTypeID
            oCleanSheetEvent.FixtureID = oFixtureDetail.FixtureID
            oCleanSheetEvent.WeekID = oFixtureDetail.WeekID
            oCleanSheetEvent.PlayerID = EventToCreate.PlayerID
            oCleanSheetEvent.Points = oGKCleanSheet.Points
            o_lCleanSheetEvent.Add(oCleanSheetEvent)
        End If

        If player.PositionID = Position.DF And EventToCreate.ToMinute >= 90 And EventToCreate.FromMinute = 0 And EventToCreate.EventTypeID = 12 And bCleanSheet = True Then
            Dim oDFCleanSheet = (From c In db.EventTypes Where c.EventName.Contains("Clean Sheet") And c.PositionID = Position.DF).FirstOrDefault()

            Dim oCleanSheetEvent As New [Event]
            oCleanSheetEvent.FromMinute = 200
            oCleanSheetEvent.EventTypeID = oDFCleanSheet.EventTypeID
            oCleanSheetEvent.FixtureID = oFixtureDetail.FixtureID
            oCleanSheetEvent.WeekID = oFixtureDetail.WeekID
            oCleanSheetEvent.PlayerID = EventToCreate.PlayerID
            oCleanSheetEvent.Points = oDFCleanSheet.Points
            o_lCleanSheetEvent.Add(oCleanSheetEvent)
        End If

        '' Concede goal events

        If player.PositionID = Position.GK OrElse player.PositionID = Position.DF AndAlso EventToCreate.EventTypeID = 12 Then
            'get the minute(s) in which the goals were conceded
            Dim oGoalsConceded As List(Of [Event]) = (From c In db.Events Where c.FixtureID = oFixtureDetail.FixtureID _
                                                            And c.EventType.EventName.Equals("Goal") And c.Player.Team.TeamID <> player.TeamID).ToList

            '' check for own goals on own side
            Dim ownGoals As List(Of [Event]) = (From c In db.Events Where c.FixtureID = oFixtureDetail.FixtureID _
                                                            And c.EventType.EventName.Equals("Own Goal") And c.Player.Team.TeamID.Equals(player.TeamID)).ToList

            If Not IsNothing(ownGoals) AndAlso ownGoals.Count > 0 Then
                oGoalsConceded.AddRange(ownGoals)
            End If

            'If Defender and on Home team and the awayscore is > 0
            If player.PositionID = Position.DF AndAlso oFixtureDetail.HomeTeamID = player.TeamID AndAlso oFixtureDetail.AwayScore > 0 Then
                For Each oEvent As [Event] In oGoalsConceded
                    If EventToCreate.FromMinute <= oEvent.FromMinute And EventToCreate.ToMinute >= oEvent.FromMinute Then
                        Dim oDFConcedeGoal = (From c In db.EventTypes Where c.EventName.Contains("Concede Goal") And c.PositionID = Position.DF).FirstOrDefault()
                        Dim oConcedeGoalEvent As New [Event]
                        oConcedeGoalEvent.FromMinute = 201
                        oConcedeGoalEvent.EventTypeID = oDFConcedeGoal.EventTypeID
                        oConcedeGoalEvent.FixtureID = oFixtureDetail.FixtureID
                        oConcedeGoalEvent.WeekID = oFixtureDetail.WeekID
                        oConcedeGoalEvent.PlayerID = EventToCreate.PlayerID
                        oConcedeGoalEvent.Points = oDFConcedeGoal.Points
                        o_lConcedeGoalEvent.Add(oConcedeGoalEvent)
                    End If
                Next oEvent
            End If

            'If Defender and on Away team and the home score is 0
            If player.PositionID = Position.DF And (oFixtureDetail.AwayTeamID = player.Team.TeamID And oFixtureDetail.HomeScore > 0) Then
                For Each oEvent As [Event] In oGoalsConceded
                    If EventToCreate.FromMinute <= oEvent.FromMinute And EventToCreate.ToMinute >= oEvent.FromMinute Then
                        Dim oDFConcedeGoal = (From c In db.EventTypes Where c.EventName.Contains("Concede Goal") And c.PositionID = Position.DF).FirstOrDefault()
                        Dim oConcedeGoalEvent As New [Event]
                        oConcedeGoalEvent.FromMinute = 201
                        oConcedeGoalEvent.EventTypeID = oDFConcedeGoal.EventTypeID
                        oConcedeGoalEvent.FixtureID = oFixtureDetail.FixtureID
                        oConcedeGoalEvent.WeekID = oFixtureDetail.WeekID
                        oConcedeGoalEvent.PlayerID = EventToCreate.PlayerID
                        oConcedeGoalEvent.Points = oDFConcedeGoal.Points
                        o_lConcedeGoalEvent.Add(oConcedeGoalEvent)
                    End If
                Next oEvent
            End If

            'If Goalkeeper and on Home team and the awayscore is 0
            If player.PositionID = Position.GK And (oFixtureDetail.HomeTeamID = player.Team.TeamID And oFixtureDetail.AwayScore > 0) Then
                oGoalsConceded.First()
                For Each oEvent As [Event] In oGoalsConceded
                    If EventToCreate.FromMinute <= oEvent.FromMinute And EventToCreate.ToMinute >= oEvent.FromMinute Then
                        Dim oGKConcedeGoal = (From c In db.EventTypes Where c.EventName.Contains("Concede Goal") And c.PositionID = Position.GK).FirstOrDefault()

                        Dim oConcedeGoalEvent As New [Event]
                        oConcedeGoalEvent.FromMinute = 202
                        oConcedeGoalEvent.EventTypeID = oGKConcedeGoal.EventTypeID
                        oConcedeGoalEvent.FixtureID = oFixtureDetail.FixtureID
                        oConcedeGoalEvent.WeekID = oFixtureDetail.WeekID
                        oConcedeGoalEvent.PlayerID = EventToCreate.PlayerID
                        oConcedeGoalEvent.Points = oGKConcedeGoal.Points
                        o_lConcedeGoalEvent.Add(oConcedeGoalEvent)
                    End If
                Next oEvent
            End If

            'If Goalkeeper and on Away team and the home score is 0
            If player.PositionID = Position.GK And (oFixtureDetail.AwayTeamID = player.Team.TeamID And oFixtureDetail.HomeScore > 0) Then
                oGoalsConceded.First()
                For Each oEvent As [Event] In oGoalsConceded
                    If EventToCreate.FromMinute <= oEvent.FromMinute And EventToCreate.ToMinute >= oEvent.FromMinute Then
                        Dim oGKConcedeGoal = (From c In db.EventTypes Where c.EventName.Contains("Concede Goal") And c.PositionID = Position.GK).FirstOrDefault()

                        Dim oConcedeGoalEvent As New [Event]
                        oConcedeGoalEvent.FromMinute = 202
                        oConcedeGoalEvent.EventTypeID = oGKConcedeGoal.EventTypeID
                        oConcedeGoalEvent.FixtureID = oFixtureDetail.FixtureID
                        oConcedeGoalEvent.WeekID = oFixtureDetail.WeekID
                        oConcedeGoalEvent.PlayerID = EventToCreate.PlayerID
                        oConcedeGoalEvent.Points = oGKConcedeGoal.Points
                        o_lConcedeGoalEvent.Add(oConcedeGoalEvent)
                    End If
                Next oEvent
            End If

        End If

        EventToCreate.Points = oEventType.Points
        EventToCreate.WeekID = oFixtureDetail.WeekID

        Return EventToCreate

    End Function

End Class
