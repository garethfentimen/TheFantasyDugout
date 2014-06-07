Imports LaGiga

Public Class Helpers

    Protected Shared db As New LaGigaClassesDataContext

    Public Enum TeamType
        National = 1
        Club = 2
        NationalNone = 36
        ClubNone = 37
    End Enum

    Public Enum PlayingType
        Playing = 1
        Squad = 2
    End Enum

    Public Enum Position
        GK = 1
        DF = 2
        MF = 3
        FW = 4
    End Enum

#Region "Lists For Forms"

    ''' <summary>Get a Select Item list of all current club teams in the premier league + all teams option</summary>
    ''' <returns>list of selectlistitem</returns>
    ''' <remarks>No relegated teams and with "all teams" value of -1</remarks>
    Public Shared Function GetClubTeamsForOptionList() As List(Of SelectListItem)
        Dim ClubTeams As New List(Of SelectListItem)
        ClubTeams.Add(New SelectListItem With {.Text = "All Teams", .Value = "-1"})
        ClubTeams.AddRange(GetSLIClubTeams())
        Return ClubTeams
    End Function

    ''' <summary>Get a Select Item list of all current club teams in the premier league</summary>
    ''' <returns>list of selectlistitem</returns>
    ''' <remarks>No relegated teams</remarks>
    Public Shared Function GetSLIClubTeams() As List(Of SelectListItem)
        Return (From o As Team In db.Teams Where o.TeamTypeID = TeamType.Club _
                            AndAlso Not o.InActive.HasValue OrElse o.InActive.Equals(False)
                            Select o = New SelectListItem _
                            With {.Value = o.TeamID, .Text = o.TeamName}).ToList()
    End Function

    ''' <summary>Get a list of all current club teams in the premier league</summary>
    ''' <returns>list of team</returns>
    ''' <remarks>No relegated teams</remarks>
    Public Shared Function GetClubTeams() As List(Of Team)
        Return (From o As Team In db.Teams Where o.TeamTypeID = TeamType.Club _
                            AndAlso Not o.InActive.HasValue OrElse o.InActive.Equals(False)).ToList()
    End Function

    ''' <summary>Get a list of all club teams</summary>
    ''' <returns>list of team</returns>
    ''' <remarks>include relegated teams</remarks>
    Public Shared Function GetAllClubTeams() As List(Of Team)
        Return (From o As Team In db.Teams Where o.TeamTypeID = TeamType.Club).ToList()
    End Function

    ''' <summary>Get a list of all club teams in the premier league including relegated teams</summary>
    ''' <returns>list of team</returns>
    ''' <remarks>Include relegated teams</remarks>
    Public Shared Function GetSLIAllClubTeams() As List(Of SelectListItem)
        Return (From o As Team In db.Teams Where o.TeamTypeID = TeamType.Club
                Select o = New SelectListItem _
                            With {.Value = o.TeamID, .Text = o.TeamName}).ToList()
    End Function

    ''' <summary>Get a list of all club teams in the premier league including relegated teams</summary>
    ''' <returns>list of selectlistitem</returns>
    ''' <remarks>Include relegated teams</remarks>
    Public Shared Function GetUserTeamByUserId(ByVal UserId As Guid) As UserTeam
        Return (From o As UserTeam In db.UserTeams Where o.UserId.Equals(UserId)).FirstOrDefault
    End Function

    ''' <summary>Get a list of playerIDs owned by userTeam</summary>
    ''' <param name="userTeam">userteam object</param>
    Shared Function GetUserPlayersByUserTeam(ByVal userTeam As UserTeam) As List(Of Integer)
        Return (From o As UserPlayer In db.UserPlayers Where o.UserTeamID.Equals(userTeam.UserTeamID) _
                AndAlso o.CompetitionID.Equals(CurrentCompetition.CompetitionID) Select o.PlayerID).ToList
    End Function

    ''' <summary>All positions as list of selectlistitem</summary>
    ''' <returns>list of selectlistitem</returns>
    Public Shared Function GetPositions() As List(Of SelectListItem)
        Return (From o As Integer In [Enum].GetValues(GetType(Position)).Cast(Of Integer)()
                        Select New SelectListItem With {.Value = o, .Text = [Enum].GetName(GetType(Position), o)}
                        ).ToList
    End Function

    Public Shared Function GetDefaultTeamTypes() As List(Of SelectListItem)
        Return (From o As Integer In [Enum].GetValues(GetType(TeamType)).Cast(Of Integer)() Where o < 3
                        Select New SelectListItem With {.Value = o, .Text = [Enum].GetName(GetType(TeamType), o)}
                        ).ToList
    End Function

    Public Shared Function EventTypeList() As List(Of SelectListItem)

        Dim eventTypesToRtn As New List(Of SelectListItem)

        eventTypesToRtn.AddRange(From c In db.EventTypes Where c.Master = True Select New SelectListItem _
                       With {.Value = c.EventTypeID, _
                         .Text = c.EventName})

        Return eventTypesToRtn
    End Function

    ''' <summary>List of active players as list</summary>
    ''' <returns>list of player</returns>
    Shared Function GetAllActivePlayers() As List(Of Player)

        Dim teamTypeID As TeamType = CurrentCompetition.TeamTypeID

        Dim rtnPlayer As New List(Of Player)

        '' Have included for national as well because we may want to change nat teams per world cup/ Euro competitions
        '' relegated = inactive
        Dim relegatedTeams As List(Of Integer) = (From o As Team In db.Teams Where o.InActive.HasValue AndAlso o.InActive.Equals(True) Select o.TeamID).ToList

        Select Case teamTypeID
            Case TeamType.Club
                rtnPlayer = (From o As Player In db.Players Where o.Team.TeamTypeID.Equals(teamTypeID) _
                        AndAlso o.Team.TeamTypeID <> TeamType.ClubNone AndAlso Not relegatedTeams.Contains(o.TeamID)).ToList
            Case TeamType.National
                rtnPlayer = (From o As Player In db.Players Where o.Team.TeamTypeID.Equals(teamTypeID) _
                        AndAlso o.Team.TeamTypeID <> TeamType.NationalNone AndAlso Not relegatedTeams.Contains(o.TeamID)).ToList
        End Select

        Return rtnPlayer

    End Function

    ''' <summary>Get a List of all active playerIDs</summary>
    ''' <returns>list of selectlistitem</returns>
    Shared Function GetAllActivePlayerIDs() As List(Of Integer)

        Dim teamTypeID As TeamType = CurrentCompetition.TeamTypeID

        Dim rtnPlayer As New List(Of Integer)

        '' Have included for national as well because we may want to change nat teams per world cup/ Euro competitions
        '' relegated = inactive
        Dim relegatedTeams As List(Of Integer) = (From o As Team In db.Teams Where o.InActive.HasValue AndAlso o.InActive.Equals(True) Select o.TeamID).ToList

        Select Case teamTypeID
            Case TeamType.Club
                rtnPlayer = (From o As Player In db.Players Where o.Team.TeamTypeID.Equals(teamTypeID) _
                        AndAlso o.Team.TeamTypeID <> TeamType.ClubNone AndAlso Not relegatedTeams.Contains(o.TeamID) Select o.PlayerID).ToList
            Case TeamType.National
                rtnPlayer = (From o As Player In db.Players Where o.Team.TeamTypeID.Equals(teamTypeID) _
                        AndAlso o.Team.TeamTypeID <> TeamType.NationalNone AndAlso Not relegatedTeams.Contains(o.TeamID) Select o.PlayerID).ToList
        End Select

        Return rtnPlayer

    End Function

#End Region

#Region "Calculations"

    ''' <summary>Works out the week number - as it has to be between 1 - 6 to alternate fixtures between players </summary>
    ''' <returns>The calculated week No between 1- (No of players - 1) e.g. 1 - 7 for eight because you cannot play yourself.</returns>
    Public Shared Function CalculateWeekNumber(ByVal UserGroup As UserGroup, ByVal weekNumber As Integer) As Integer

        Dim iWeekNo As Integer = 0

        Dim noOfPlayers As Integer = UserGroup.NoPlayers - 1

        '' if weeknumber <= no of players - so in a group of 8 this would be 7
        '' then we need to cycle the week number back around again so that it works with the fixture calculator table

        '' loop 10 times - this should cater for nearly all scenarios - unless its a really long season and its a tiny goup - like 4

        If weekNumber <= noOfPlayers Then
            Return weekNumber
        Else

            ' '' run loop to find iweekNo
            For i As Integer = 2 To 15
                If weekNumber > (noOfPlayers * (i - 1)) And weekNumber <= (noOfPlayers * i) Then
                    iWeekNo = weekNumber - (noOfPlayers * (i - 1))
                    Exit For
                End If
            Next

            Return iWeekNo
        End If

    End Function
#End Region

#Region "Current Game Time"
    Shared Function CurrentWeek() As Week
        Dim oCurrentWeek = (From c In db.Weeks Where c.CurrentWeek = True Select c).FirstOrDefault()
        Dim oNoCurrentWeek = (From c In db.Weeks Order By c.WeekID Descending Select c).FirstOrDefault()

        Return IIf(IsNothing(oCurrentWeek), oNoCurrentWeek, oCurrentWeek)
    End Function

    Shared Function LastWeekOfCompetition(competitionId As Integer) As Week
        Return db.Weeks.Where(Function(o) o.CompetitionID = competitionId).OrderByDescending(Function(o) o.WeekID).FirstOrDefault
    End Function

    Shared Function CurrentCompetition() As Competition
        Dim currentComp As Competition = (From o In db.Competitions Where o.CurrentCompetition.HasValue AndAlso o.CurrentCompetition.Equals(True) Select o).FirstOrDefault()
        If IsNothing(currentComp) Then
            currentComp = (From c In db.Competitions Order By c.CompetitionID Descending Select c).FirstOrDefault()
        End If
        Return currentComp
    End Function

    Shared Function GetCompetionList() As List(Of SelectListItem)
        Dim competitions As List(Of SelectListItem) = (From o In db.Competitions Where Not o.CurrentCompetition.HasValue OrElse o.CurrentCompetition.Equals(False)).
                                        Select(Function(o) New SelectListItem With
                                            {
                                             .Text = o.Name,
                                             .Value = o.CompetitionID
                                            }).ToList()

        competitions.Add(New SelectListItem With {.Selected = True, .Text = "Current", .Value = CurrentCompetition.CompetitionID})
        Return competitions
    End Function

    Shared Function GetCompetitionWeekIdsForCompetitionID(ByVal CompetitionID As Integer) As List(Of Integer)
        Return (From o As Week In db.Weeks Where o.CompetitionID.Equals(CompetitionID) Select o.WeekID).ToList
    End Function

#End Region

    Shared Function GetPlayerById(ByVal playerId As Integer) As Player
        Return db.Players.Where(Function(o) o.PlayerID = playerId).FirstOrDefault
    End Function

End Class
