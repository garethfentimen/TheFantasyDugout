Imports LaGiga.Helpers

Public Class UserPlayerRepository
    Inherits StandardFunctions
    Implements IUserPlayerRepository

    Public Function ListUserPlayer(ByVal UserTeamID As Integer?, ByVal CompetitionID As Integer, ByVal pageSize As Integer, ByVal pageIndex As Integer?) As System.Collections.Generic.IEnumerable(Of UserPlayer) Implements IUserPlayerRepository.ListUserPlayer
        Return (From o In db.UserPlayers Where o.UserTeamID = UserTeamID And o.CompetitionID.Equals(CompetitionID) Order By o.Player.PositionID).ToList()
    End Function

    Function UserPlayerCount(ByVal UserTeamID As Integer?, ByVal CompetitionID As Integer) As Integer Implements IUserPlayerRepository.UserPlayerCount
        Return (From c In db.UserPlayers Where c.UserTeamID = UserTeamID And c.CompetitionID.Equals(CompetitionID)).Count()
    End Function

    Function GetClubTeamsDD() As System.Linq.IQueryable(Of SelectListItem) Implements IUserPlayerRepository.GetClubTeamsDD
        Return From c In db.Teams Where c.TeamTypeID = TeamType.Club And c.TeamID <> TeamType.ClubNone _
                    AndAlso Not c.InActive.HasValue OrElse c.InActive.Equals(False) Order By c.TeamName _
                        Select c = New SelectListItem With {.Value = c.TeamID, .Text = c.TeamName}
    End Function

    Function getPlayersAtClubsDD(ByVal TeamID As Integer) As IEnumerable(Of SelectListItem) Implements IUserPlayerRepository.getPlayersAtClubsDD
        Return (From c As Player In db.Players Where
                    Not c.TeamID = TeamType.ClubNone _
                    AndAlso c.TeamID = TeamID _
                Order By c.PositionID _
                    Select c = New SelectListItem With {.Value = c.PlayerID,
                                                    .Text = c.FirstName & " " & c.Surname})

        '[Enum].GetName(GetType(Position), c.PositionID) & ": " & c.FirstName & " " & c.Surname}
    End Function

    Function GetUserGroup(ByVal UserTeamID As Integer) As UserTeam Implements IUserPlayerRepository.GetUserGroup
        Return (From c In db.UserTeams Where c.UserTeamID = UserTeamID Select c).FirstOrDefault()
    End Function

    Function CreateUserPlayer(ByVal UserPlayerToCreate As UserPlayer) As UserPlayer Implements IUserPlayerRepository.CreateUserPlayer
        db.UserPlayers.InsertOnSubmit(UserPlayerToCreate)
        db.SubmitChanges()
        Return UserPlayerToCreate
    End Function

    Public Sub DeleteUserPlayer(ByVal UserPlayerToDelete As UserPlayer) Implements IUserPlayerRepository.DeleteUserPlayer
        db.UserPlayers.DeleteOnSubmit(UserPlayerToDelete)
        'also delete the UserPlayerWeek Record
        Dim oWUP As WeekUserPlayer = (From c In db.WeekUserPlayers Where c.PlayerID = UserPlayerToDelete.PlayerID And c.UserTeamID = UserPlayerToDelete.UserTeamID).FirstOrDefault()
        db.WeekUserPlayers.DeleteOnSubmit(oWUP)
        db.SubmitChanges()
    End Sub

    Function GetUserPlayer(ByVal UserPlayerID As Integer?) As UserPlayer Implements IUserPlayerRepository.getUserPlayer
        Return (From c In db.UserPlayers Where c.UserPlayerID = UserPlayerID).FirstOrDefault()
    End Function

    Function CreateWeekUserPlayer(ByVal UserPlayerToCreate As UserPlayer) As WeekUserPlayer Implements IUserPlayerRepository.CreateWeekUserPlayer
        Dim oWeekUserPlayer As New WeekUserPlayer
        oWeekUserPlayer.StatusID = Helpers.PlayingType.Squad
        oWeekUserPlayer.Draft = True
        oWeekUserPlayer.UserTeamID = UserPlayerToCreate.UserTeamID
        oWeekUserPlayer.WeekID = GetCurrentWeek().WeekID
        oWeekUserPlayer.PlayerID = UserPlayerToCreate.PlayerID

        db.WeekUserPlayers.InsertOnSubmit(oWeekUserPlayer)
        db.SubmitChanges()
        Return oWeekUserPlayer
    End Function

    Function GetCurrentWeek() As Week
        Return Helpers.CurrentWeek()
    End Function

End Class
