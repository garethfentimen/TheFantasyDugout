Imports LaGiga.Helpers

Public Class StandardFunctions
    Protected db As New LaGigaClassesDataContext
    Protected Property bRunResultCalcs As Boolean = True
    Protected iPageSize As Integer = 10

    Function FindEventType(ByVal result As Result, ByVal EventName As String) As Result

        Select Case EventName
            Case "Appearance (start or sub)"
                result.Appearances += 1
            Case "Goal"
                result.Goals += 1
            Case "Assist"
                result.Assists += 1
            Case "Yellow Card"
                result.Yellowcards += 1
            Case "Red Card"
                result.RedCards += 1
            Case "Clean Sheet"
                result.CleanSheets += 1
            Case "Concede Goal"
                result.GoalsConceded += 1
        End Select

        Return result

    End Function

    ''' <summary>Calculates the result for a users team </summary>
    ''' <param name="UserTeamID">The users Team Ident</param>
    ''' <param name="oFirstTeamPlayers">Collection of the 11 players who were playing during the week</param>
    ''' <returns>List of result</returns>
    Function GetUserResult(ByVal UserTeamID As Integer, ByVal oFirstTeamPlayers As List(Of WeekUserPlayer)) As List(Of Result)

        Dim oResultCollection As New List(Of Result)

        Dim PlayerPoints As Double
        Dim TotPoints As Double

        If oFirstTeamPlayers Is Nothing Then 'still no team data so must not have picked yet
            Return Nothing
        Else

            For Each oPlayer As WeekUserPlayer In oFirstTeamPlayers
                Dim o_lPlayer = oPlayer
                Dim oResult As New Result
                Dim oFirstTeamEvents = From c In db.Events Where c.PlayerID = o_lPlayer.PlayerID And c.WeekID = CurrentWeek.WeekID

                PlayerPoints = 0 'set points back to zero for iteration

                For Each oEvent In oFirstTeamEvents
                    Dim o_lEvent = oEvent
                    PlayerPoints += oEvent.Points
                    Dim oEventType = (From c In db.EventTypes Where c.EventTypeID = o_lEvent.EventTypeID).FirstOrDefault

                    If bRunResultCalcs Then
                        oResult = FindEventType(oResult, oEventType.EventName)
                    End If

                Next oEvent

                oResult.PlayerName = oPlayer.Player.FirstName + " " + oPlayer.Player.Surname
                oResult.TeamName = oPlayer.Player.Team.TeamName
                oResult.Score = PlayerPoints

                oResultCollection.Add(oResult)
                TotPoints += PlayerPoints
            Next oPlayer

            For Each Result In oResultCollection
                Result.TotalScore = TotPoints
            Next Result

            Return oResultCollection

        End If

    End Function

    Public Function GetUserTeamByUserName(ByVal username As String) As UserTeam
        Return (From c In db.UserTeams Where c.Name Is (UCase(Left(username, 1)) + Right(username, Len(username) - 1))).FirstOrDefault()
    End Function
End Class
