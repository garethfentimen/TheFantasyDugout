Imports LaGiga.Helpers

Public Class PlayerEventModel
    Inherits StandardFunctions
    Implements IComparable

    Sub New()
        'Empty constructor
    End Sub

    Property PlayerID As Integer
    Property PlayerFirstName As String
    Property PlayerSurname As String
    Property Position As String
    Property ClubTeam As String
    Property NationalTeam As String

    'Event Properties
    Property TotalPoints As Double
    Property AveragePoints As Double

    Property c_iPlayerCount As Integer
    Property c_iPageSize As Integer

    Function compareto(ByVal obj As Object) As Integer Implements IComparable.CompareTo
        Dim oPlayerEventCompare As PlayerEventModel = CType(obj, PlayerEventModel)
        If Me.TotalPoints > oPlayerEventCompare.TotalPoints Then
            Return -1
        End If
        If Me.TotalPoints < oPlayerEventCompare.TotalPoints Then
            Return 1
        End If
        Return 0
    End Function

    Public Function PopulateClassProperties(ByVal iPageIndex As Integer?, ByVal iTeamID As Integer?, ByVal iPositionID As Integer?, ByVal sSearchName As String) As List(Of PlayerEventModel)
        Dim oPlayerEventModelList As New List(Of PlayerEventModel)
        iPageSize = 6
        c_iPageSize = iPageSize

        Dim oAllPlayers As New List(Of Player)
        If iTeamID <> 0 Or sSearchName <> "" Or iPositionID <> 0 Then
            If iTeamID > 0 And sSearchName = "" And iPositionID = 0 Then
                oAllPlayers = (From c In db.Players Where c.TeamID = iTeamID).ToList()
            End If
            If iTeamID < 1 Then
                oAllPlayers = (From c In db.Players Where c.Surname.Contains(sSearchName)).ToList()
            End If
            If iTeamID > 0 And sSearchName <> "" And iPositionID = 0 Then
                oAllPlayers = (From c In db.Players Where c.TeamID = iTeamID And c.Surname.Contains(sSearchName)).ToList()
            End If
            'position
            If iPositionID > 0 Then
                If iTeamID <= 0 And sSearchName = "" Then
                    oAllPlayers = (From c In db.Players Where c.PositionID = iPositionID).ToList()
                End If
                If iTeamID > 0 Then
                    oAllPlayers = (From c In db.Players Where c.PositionID = iPositionID And c.TeamID = iTeamID).ToList()
                End If
                If sSearchName <> "" Then
                    oAllPlayers = (From c In db.Players Where c.PositionID = iPositionID And c.Surname.Contains(sSearchName)).ToList()
                End If
                If iTeamID > 0 And sSearchName <> "" Then
                    oAllPlayers = (From c In db.Players Where c.PositionID = iPositionID And c.TeamID = iTeamID And c.Surname.Contains(sSearchName)).ToList()
                End If
            End If
        Else
            oAllPlayers = (From c In db.Players).ToList
        End If

        oAllPlayers.Skip((iPageIndex - 1) * iPageSize).Take(iPageSize).ToList()

        c_iPlayerCount = oAllPlayers.Count()

        For Each player As Player In oAllPlayers
            Dim oPlayerEventModel As New PlayerEventModel
            Dim l_oPlayer = player
            oPlayerEventModel.PlayerID = player.PlayerID
            oPlayerEventModel.PlayerFirstName = player.FirstName
            oPlayerEventModel.PlayerSurname = player.Surname
            oPlayerEventModel.Position = [Enum].GetName(GetType(Position), player.PositionID)
            oPlayerEventModel.ClubTeam = player.Team.TeamName
            oPlayerEventModel.NationalTeam = player.Team1.TeamName

            Dim oEvents = (From c In db.Events Where c.PlayerID = l_oPlayer.PlayerID)
            Dim iWeekCount = (From c In db.Weeks Where c.WeekNo < Helpers.CurrentWeek.WeekNo).Count()
            For Each oEvent As [Event] In oEvents
                oPlayerEventModel.TotalPoints += oEvent.Points
            Next
            'oPlayerEventModel.AveragePoints = Math.Round(oPlayerEventModel.TotalPoints / iWeekCount, 2)
            oPlayerEventModelList.Add(oPlayerEventModel)
        Next

        oPlayerEventModelList.Sort()

        Return oPlayerEventModelList.Skip((iPageIndex - 1) * iPageSize).Take(iPageSize).ToList()
    End Function

End Class
