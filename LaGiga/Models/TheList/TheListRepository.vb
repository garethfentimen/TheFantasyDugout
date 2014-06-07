Imports LaGiga.Helpers
Imports System.Net
Imports System.IO
Imports System.Threading.Tasks

Public Class TheListRepository
    Inherits StandardFunctions

    Function GetUserTeamPlayer(ByVal UserGroupID As Integer) As Dictionary(Of Integer, UserPlayer)
        Return (From o As UserPlayer In db.UserPlayers Where o.CompetitionID.Equals(CurrentWeek.CompetitionID) _
                AndAlso o.UserGroupID.Equals(UserGroupID)).ToDictionary(Function(c) c.PlayerID)
    End Function

    Function GetEventInfo(ByVal playerId As Integer, ByVal competitionId As Integer) As List(Of ResultEventModel)

        Dim EventModelList As New List(Of ResultEventModel)

        Dim competitionWeekIds As List(Of Integer) = Helpers.GetCompetitionWeekIdsForCompetitionID(competitionId)

        '' Check here for transfers for this player
        Dim playerTransfers = db.Transfers.Where(Function(o) o.PlayerID.Equals(playerId))

        For Each activeWeek As TheList In db.TheLists.Where(Function(o) o.PlayerID.Equals(playerId) _
                                            AndAlso competitionWeekIds.Contains(o.WeekID)).OrderByDescending(Function(o) o.WeekID).ToList

            If activeWeek.MinutesPlayed > 0 Then

                Dim EventModel As New ResultEventModel

                Dim fixtureSb As New StringBuilder

                Dim playerTeamId = activeWeek.Player.TeamID

                Dim transferFee As Decimal = 0.0

                If Not playerTransfers Is Nothing Then

                    Dim transferEventModel = New ResultEventModel

                    For Each transfer In playerTransfers
                        If transfer.ToDate.HasValue AndAlso _
                            activeWeek.Week.FromDate < transfer.ToDate Then
                            playerTeamId = transfer.TeamID
                        End If

                        '' Check wether we need to add a transfer REM
                        '' when the week is the last one it can be before the transfer

                        If transfer.ToDate.HasValue AndAlso _
                            transfer.ToDate >= activeWeek.Week.FromDate AndAlso _
                            activeWeek.Week.ToDate.HasValue AndAlso _
                            transfer.ToDate < activeWeek.Week.ToDate.Value Then

                            transferEventModel.IsTransferREM = True
                            transferEventModel.FromTeam = transfer.Team.TeamName

                            EventModelList.Add(transferEventModel)
                        End If

                        If Not transfer.ToDate.HasValue Then
                            '' current club
                            transferEventModel.TransferFee = transfer.TransferFee
                            transferEventModel.ToTeam = transfer.Team.TeamName
                        End If
                    Next

                    If transferEventModel.IsTransferREM Then
                        EventModelList.Add(transferEventModel)
                    End If

                End If



                Dim fixtures As List(Of Fixture) = (From o As Fixture In db.Fixtures Where o.WeekID.Equals(activeWeek.WeekID) _
                                                    AndAlso (o.AwayTeamID.Equals(playerTeamId) OrElse o.HomeTeamID.Equals(playerTeamId))).ToList

                For Each Fix As Fixture In fixtures
                    fixtureSb.AppendFormat("{0} {1} - {2} {3}", Fix.Team1.TeamName, Fix.HomeScore, Fix.AwayScore, Fix.Team.TeamName)
                Next

                EventModel.Fixtures = fixtureSb.ToString()
                EventModel.WeekName = activeWeek.Week.WeekName
                EventModel.MinutesOnPitch = activeWeek.MinutesPlayed
                EventModel.Goals = activeWeek.GoalsScored
                EventModel.GoalsConceded = activeWeek.GoalsConceeded
                EventModel.Assists = activeWeek.Assists
                EventModel.CleanSheets = activeWeek.CleanSheets
                EventModel.YellowCards = activeWeek.YellowCards
                EventModel.RedCards = activeWeek.RedCards
                EventModel.WeekPoints = activeWeek.WeekPoints
                EventModel.TotalPoints = activeWeek.TotalPoints

                EventModelList.Add(EventModel)

            End If
        Next

        Return EventModelList

    End Function

    Function GetCurrentListDataForPlayer(ByVal PlayerID As Integer) As TheList

        Dim LastWeekID As Integer = (From o As Week In db.Weeks Where o.CompetitionID.Equals(CurrentWeek.CompetitionID) _
                                AndAlso o.WeekNo.Equals(CurrentWeek.WeekNo - 1) Select o.WeekID).FirstOrDefault
        If Not IsNothing(LastWeekID) Then
            Return (From o As TheList In db.TheLists Where o.PlayerID.Equals(PlayerID) _
                                      AndAlso o.WeekID.Equals(LastWeekID)).FirstOrDefault
        End If

    End Function

    Function GetCurrentFixtureForPlayer(ByVal PlayerID As Integer) As String

        Dim player As Player = (From o As Player In db.Players Where o.PlayerID.Equals(PlayerID)).FirstOrDefault

        Dim fixtures As List(Of Fixture) = (From o As Fixture In db.Fixtures Where o.WeekID.Equals(CurrentWeek.WeekID) _
                                                AndAlso (o.AwayTeamID.Equals(player.TeamID) OrElse o.HomeTeamID.Equals(player.TeamID))).ToList

        Dim fixtureSb As New StringBuilder

        For Each Fix As Fixture In fixtures
            fixtureSb.AppendFormat("{0} {1} - {2} {3}", Fix.Team1.TeamName, Fix.HomeScore, Fix.AwayScore, Fix.Team.TeamName)
        Next

        Return fixtureSb.ToString
    End Function

    Function FindPlayerImage(ByVal playerName As String) As String

        Dim strResult As String = ""

        Try

            Dim UrlToQuery As String = "http://en.wikipedia.org/wiki/" & playerName.Replace(" ", "_")

            Dim objRequest As WebRequest = HttpWebRequest.Create(UrlToQuery)

            Dim ScrapeWebTask = Task.Factory.StartNew(Function() objRequest.GetResponse())

            ScrapeWebTask.Wait()

            Using sr As New StreamReader(ScrapeWebTask.Result.GetResponseStream())
                strResult = sr.ReadToEnd()
                '' Close and clean up the StreamReader
                sr.Close()
            End Using
            Dim searchString = "<a href=""/wiki/File:"
            Dim imageLocationStart As Integer = strResult.IndexOf(searchString)

            If imageLocationStart > 0 Then
                Dim imageHtml = Mid(strResult, imageLocationStart, 400)

                searchString = "src="""
                imageLocationStart = imageHtml.IndexOf(searchString)
                searchString = """ width="""
                Dim imageLocationEnd As Integer = imageHtml.IndexOf(searchString)

                strResult = imageHtml.Substring(imageLocationStart, imageLocationEnd - imageLocationStart).Replace("src=""", "")
            End If

        Catch ex As Exception
            strResult = "NoImage"
        End Try

        If strResult.IndexOf("DOCTYPE") > 0 Then
            strResult = "NoImage"
        End If

        Return strResult

    End Function

End Class