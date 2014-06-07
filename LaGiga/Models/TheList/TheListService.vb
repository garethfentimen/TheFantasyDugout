Imports LaGiga.Helpers
Namespace TheListServices

    Public Interface IGetPlayersForTheList
        Function GetPlayersForTheList() As List(Of TheList)
    End Interface

    Public Class GetPlayersForTheList
        Inherits StandardFunctions
        'Implements IGetPlayersForTheList

        Public Property TotalActivePlayers As Integer
        Private CompetitionId As Integer
        Private PageIndex As Integer
        Private PositionID As Integer
        Private TeamID As Integer
        Private _searchName As String

        Private ReadOnly Property LoweredSearchName As String
            Get
                Return _searchName.ToLower()
            End Get
        End Property

        Public Sub New(ByVal competitionId As Integer, ByVal pageIndex As Integer, ByVal teamID As Integer, ByVal positionID As Integer, ByVal LoweredSearchName As String)

            Me.CompetitionId = competitionId
            Me.PageIndex = pageIndex
            Me.PositionID = positionID
            Me.TeamID = teamID
            Me._searchName = LoweredSearchName

        End Sub

        Function GetPlayersForTheList() As List(Of TheList)

            'Dim t2 As New Stopwatch
            't2.Start()

            Dim players As List(Of Player) = Helpers.GetAllActivePlayers()

            If TeamID <> 0 Or LoweredSearchName <> "" Or PositionID <> 0 Then

                If TeamID = -2 Then
                    Dim mu As MembershipUser = Membership.GetUser
                    Dim userPlayerIDs As List(Of Integer) = Helpers.GetUserPlayersByUserTeam(Helpers.GetUserTeamByUserId(mu.ProviderUserKey))
                    players = players.Where(Function(o) userPlayerIDs.Contains(o.PlayerID)).ToList
                End If

                If TeamID > 0 And LoweredSearchName = "" And PositionID = 0 Then
                    players = players.Where(Function(o) o.TeamID.Equals(TeamID)).ToList
                End If
                If TeamID < 1 Then
                    players = players.Where(Function(o) o.Surname.ToLower.Contains(LoweredSearchName)).ToList
                End If
                If TeamID > 0 And LoweredSearchName <> "" And PositionID = 0 Then
                    players = players.Where(Function(o) o.Surname.ToLower.Contains(LoweredSearchName) And o.TeamID.Equals(TeamID)).ToList
                End If
                'position
                If PositionID > 0 Then
                    If TeamID <= 0 And LoweredSearchName = "" Then
                        players = players.Where(Function(o) o.PositionID.Equals(PositionID)).ToList
                    End If
                    If TeamID > 0 Then
                        players = players.Where(Function(o) o.PositionID.Equals(PositionID) AndAlso o.TeamID.Equals(TeamID)).ToList
                    End If
                    If LoweredSearchName <> "" Then
                        players = players.Where(Function(o) o.PositionID.Equals(PositionID) AndAlso o.Surname.ToLower.Contains(LoweredSearchName)).ToList
                    End If
                    If TeamID > 0 And LoweredSearchName <> "" Then
                        players = players.Where(Function(o) o.PositionID.Equals(PositionID) AndAlso o.TeamID.Equals(TeamID) _
                                                            AndAlso o.Surname.ToLower.Contains(LoweredSearchName)).ToList
                    End If
                End If
            End If

            't2.Stop()

            'Debug.WriteLine("** Filtering took " & t2.ElapsedMilliseconds & "ms")

            'Dim t1 As New Stopwatch
            't1.Start()

            Dim theListData As New List(Of TheList)

            Dim weekToShow As Integer = CurrentWeek.WeekNo - 1
            If CompetitionId <> Helpers.CurrentCompetition.CompetitionID Then
                weekToShow = Helpers.LastWeekOfCompetition(CompetitionId).WeekNo - 1
            End If

            Dim LastWeek As Week = (From o As Week In db.Weeks Where o.CompetitionID.Equals(CompetitionId) AndAlso o.WeekNo.Equals(weekToShow)).FirstOrDefault

            Dim thisWeeksListData As New List(Of TheList)

            If LastWeek IsNot Nothing Then

                Dim playerIDs As List(Of Integer) = (From o In players Select o.PlayerID).ToList

                If playerIDs.Count > 0 Then
                    thisWeeksListData = (From o As TheList In db.TheLists Where o.WeekID.Equals(LastWeek.WeekID) AndAlso playerIDs.Contains(o.PlayerID)).ToList
                Else
                    'could not find any players
                    'thisWeeksListData = (From o As TheList In db.TheLists Where o.WeekID.Equals(LastWeek.WeekID)).ToList
                End If

            End If

            theListData.AddRange(thisWeeksListData)

            theListData = (From o As TheList In theListData Order By o.TotalPoints Descending).ToList

            TotalActivePlayers = theListData.Count()

            theListData = theListData.Skip((PageIndex - 1) * iPageSize).Take(iPageSize).ToList

            't1.Stop()

            'Debug.WriteLine("** Get data took " & t1.ElapsedMilliseconds & "ms")

            Return theListData

        End Function

    End Class

End Namespace
