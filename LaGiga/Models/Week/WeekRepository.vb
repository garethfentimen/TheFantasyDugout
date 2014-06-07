Public Class WeekRepository
    Inherits StandardFunctions

    Function ListWeeks(ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As Object
        Dim oweeks = From c In db.Weeks Order By c.CompetitionID Descending, c.WeekNo Descending
        If pageSize.HasValue And pageIndex.HasValue And pageIndex > 0 Then
            Return oweeks.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList()
        End If
        Return oweeks.ToList()
    End Function
    Function WeeksCount() As Integer
        Return (From c In db.Weeks).Count()
    End Function
    Function GetCompetitionDD() As IQueryable(Of SelectListItem)
        Return From c In db.Competitions Where c.CurrentCompetition.HasValue AndAlso c.CurrentCompetition.Value.Equals(True) _
               Select c = New SelectListItem With {.Value = c.CompetitionID, .Text = c.Name, .Selected = True}
    End Function
    Function CreateWeek(ByVal Competition As Integer, ByVal WeekToCreate As Week) As Boolean
        WeekToCreate.CompetitionID = Competition
        db.Weeks.InsertOnSubmit(WeekToCreate)
        db.SubmitChanges()
        Return True
    End Function
    Function EditWeek(ByVal WeekToEdit As Week) As Boolean
        Try
            Dim oOriginalWeek = (From c In db.Weeks Where c.WeekID = WeekToEdit.WeekID Select c).FirstOrDefault()
            oOriginalWeek.WeekName = WeekToEdit.WeekName
            oOriginalWeek.CompetitionID = WeekToEdit.CompetitionID
            oOriginalWeek.CurrentWeek = WeekToEdit.CurrentWeek
            oOriginalWeek.WeekNo = WeekToEdit.WeekNo
            oOriginalWeek.FromDate = WeekToEdit.FromDate
            oOriginalWeek.ToDate = WeekToEdit.ToDate
            db.SubmitChanges()
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Function GetWeek(ByVal id As Integer) As Week
        Return (From c In db.Weeks Where c.WeekID = id Select c).FirstOrDefault
    End Function

    Function DeleteWeek(ByVal WeekToDelete As Week) As Boolean
        Try
            db.Weeks.DeleteOnSubmit(WeekToDelete)
            db.SubmitChanges()
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Function GetCurrentWeek() As Week
        Return Helpers.CurrentWeek()
    End Function

    Function CreateWeekUserPlayer(ByVal WeekUserPlayers As List(Of WeekUserPlayer)) As IEnumerable(Of WeekUserPlayer)
        db.WeekUserPlayers.InsertAllOnSubmit(WeekUserPlayers)
        db.SubmitChanges()
        Return WeekUserPlayers
    End Function

    Function getUserResults(ByVal UserTeamID As Integer, _
                            ByVal CurrentCompetitionID As Integer) As List(Of Result)

        Dim firstTeamPlayers As List(Of WeekUserPlayer)
        firstTeamPlayers = (From o In db.WeekUserPlayers Where o.UserTeamID = UserTeamID And o.StatusID = Helpers.PlayingType.Playing And o.Draft = 0 And o.WeekID = Helpers.CurrentWeek.WeekID).ToList()

        If firstTeamPlayers.Count = 0 Then
            'team has not been picked so pick last weeks team.
            Dim oLastWeek = (From c In db.Weeks Where c.WeekNo = Helpers.CurrentWeek.WeekNo - 1 AndAlso c.CompetitionID.Equals(CurrentCompetitionID)).FirstOrDefault
            '' Cannot get last weeks squad if there was no last week
            If Not IsNothing(oLastWeek) Then
                Dim oLastWeeksSquad As List(Of WeekUserPlayer) = (From c In db.WeekUserPlayers Where c.UserTeamID = UserTeamID And c.Draft = 0 And c.WeekID = oLastWeek.WeekID).ToList()
                'change the weekID
                Dim oSquadList As New List(Of WeekUserPlayer)

                For Each oLastSquad In oLastWeeksSquad
                    Dim oSquad As New WeekUserPlayer
                    oSquad.WeekID = Helpers.CurrentWeek.WeekID
                    oSquad.Draft = oLastSquad.Draft
                    oSquad.PlayerID = oLastSquad.PlayerID
                    oSquad.StatusID = oLastSquad.StatusID
                    oSquad.UserTeamID = oLastSquad.UserTeamID
                    oSquadList.Add(oSquad)
                Next oLastSquad
                CreateWeekUserPlayer(oSquadList)
            End If
            firstTeamPlayers = (From o In db.WeekUserPlayers Where o.UserTeamID = UserTeamID And o.StatusID = Helpers.PlayingType.Playing And o.Draft = 0 And o.WeekID = Helpers.CurrentWeek.WeekID).ToList()
        End If

        bRunResultCalcs = False
        Return GetUserResult(UserTeamID, firstTeamPlayers)

    End Function

    Function GetUserTeamsInUserGroup(ByVal UserGroupID As Integer) As List(Of UserTeam)
        Return (From c In db.UserTeams Where c.UserGroupID = UserGroupID).ToList
    End Function

    Function AddSeasonResult(ByVal oSeasonResultCollection As List(Of SeasonResult)) As Boolean
        Try
            db.SeasonResults.InsertAllOnSubmit(oSeasonResultCollection)
            db.SubmitChanges()
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Function GetWeekByCompetitionAndNo(ByVal WeekNo As Integer, ByVal competitionID As Integer) As Week
        Return (From c In db.Weeks Where c.WeekNo = WeekNo AndAlso c.CompetitionID.Equals(competitionID)).FirstOrDefault()
    End Function

    Function GetAllUserGroups() As List(Of UserGroup)
        Return (From o As UserGroup In db.UserGroups).ToList
    End Function

    Function GetAllCompetitionsDD() As IQueryable(Of SelectListItem)
        Return From c In db.Competitions
               Select c = New SelectListItem With {.Value = c.CompetitionID, .Text = c.Name}
    End Function

    Function PopulateTheList(ByVal week As Week) As ErrorIndexModel

        Dim res As New ErrorIndexModel

        Try

            Dim theListCollection As New List(Of TheList)

            Dim thisWeeksEvents As List(Of [Event]) = (From o As [Event] In db.Events Where o.WeekID.Equals(week.WeekID)).ToList

            Dim thisCompetitionWeekIDs As List(Of Integer) = Helpers.GetCompetitionWeekIdsForCompetitionID(week.CompetitionID)

            For Each player As Player In Helpers.GetAllActivePlayers()

                Try

                Dim eventsForPlayer As List(Of [Event]) = (From o As [Event] In thisWeeksEvents Where o.PlayerID.Equals(player.PlayerID)).ToList

                    
                    If Not IsNothing(eventsForPlayer) AndAlso eventsForPlayer.Count > 0 Then

                        '' add records for a player with events for this week events
                        ''
                        Dim thisWeekPoints As Double = 0.0

                        Dim resultForPlayer As New Result

                        Dim theListItem As New TheList

                        theListItem.PlayerID = player.PlayerID
                        theListItem.WeekID = week.WeekID

                        For Each e As [Event] In eventsForPlayer

                            Dim eventType = (From c In db.EventTypes Where c.EventTypeID = e.EventTypeID).FirstOrDefault

                            resultForPlayer = FindEventType(resultForPlayer, eventType.EventName)

                            If eventType.EventName = "Appearance (start or sub)" Then
                                theListItem.MinutesPlayed = e.ToMinute - e.FromMinute
                            End If

                            thisWeekPoints += e.Points

                        Next

                        theListItem.WeekPoints = thisWeekPoints
                        theListItem.GoalsScored = resultForPlayer.Goals
                        theListItem.Assists = resultForPlayer.Assists
                        theListItem.GoalsConceeded = resultForPlayer.GoalsConceded
                        theListItem.CleanSheets = resultForPlayer.CleanSheets
                        theListItem.YellowCards = resultForPlayer.Yellowcards
                        theListItem.RedCards = resultForPlayer.RedCards

                        Dim previousListData As TheList = (From o As TheList In db.TheLists Where o.PlayerID.Equals(player.PlayerID) _
                                                           AndAlso thisCompetitionWeekIDs.Contains(o.WeekID) _
                                                           Order By o.WeekID Descending).FirstOrDefault

                        If IsNothing(previousListData) Then
                            theListItem.TotalPoints = thisWeekPoints
                        Else
                            theListItem.TotalPoints = previousListData.TotalPoints + thisWeekPoints
                        End If

                        theListCollection.Add(theListItem)

                    Else
                        '' if no event info then add empty record taking the total from the most recent previous week
                        ''
     
                        Dim previousListData As TheList = (From o As TheList In db.TheLists Where o.PlayerID.Equals(player.PlayerID) _
                                                           AndAlso thisCompetitionWeekIDs.Contains(o.WeekID) _
                                                           Order By o.WeekID Descending).FirstOrDefault
                        

                        If Not IsNothing(previousListData) Then
                            '' only add if there is previous data for player
                            ''

                            Dim theListItem As New TheList

                            theListItem.PlayerID = player.PlayerID
                            theListItem.WeekID = week.WeekID
                            theListItem.MinutesPlayed = 0
                            theListItem.WeekPoints = 0
                            theListItem.GoalsScored = 0
                            theListItem.Assists = 0
                            theListItem.GoalsConceeded = 0
                            theListItem.CleanSheets = 0
                            theListItem.YellowCards = 0
                            theListItem.RedCards = 0
                            theListItem.TotalPoints = previousListData.TotalPoints

                            theListCollection.Add(theListItem)
                        End If

                    End If

                Catch ex As Exception
                    res.ErrorType = ErrorType.IsError
                    res.Message = "Something bad went wrong looping around player " & player.PlayerID.ToString & ex.ToString
                End Try
            Next

            '' got all data now commit to db
            db.TheLists.InsertAllOnSubmit(theListCollection)
            db.SubmitChanges()

            res.ErrorType = ErrorType.Success
            res.Message = "Populated the list data successfully"

        Catch ex As Exception
            res.ErrorType = ErrorType.IsError
            res.Message = "Something bad went wrong.. Tell Gareth he is a fool - " & ex.ToString
        End Try

        Return res

    End Function

    Function populateListBackData() As ErrorIndexModel

        Dim res As ErrorIndexModel

        Dim weeksNoListData As List(Of Week) = (From o As Week In db.Weeks Where Not o.WeekID.Equals(Helpers.CurrentWeek.WeekID) _
                                                AndAlso o.CompetitionID.Equals(Helpers.CurrentCompetition.CompetitionID)).ToList

        For Each Week As Week In weeksNoListData

            Try

                res = PopulateTheList(Week)

            Catch ex As Exception
                res.ErrorType = ErrorType.IsError
                res.Message = "Something bad in the loop around old weeks.. Tell Gareth he is a fool - " & ex.ToString
            End Try
        Next

        Return res
    End Function




End Class
