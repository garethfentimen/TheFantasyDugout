using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaGiga.Service.TheList
{
    public class TheListService
    {

        private int competitionId;
        private int pageIndex;
        private int positionID;
        private int teamID;
        private string searchName;
    
        public TheListService(int competitionId, int pageIndex, int teamID, int positionID, string LoweredSearchName)
        {
            this.competitionId = competitionId;
            this.pageIndex = pageIndex;
            this.positionID = positionID;
            this.teamID = teamID;
            this.searchName = LoweredSearchName;
        }            

      public IEnumerable<TheList> GetPlayersForTheList()
      {

          'Dim t2 As New Stopwatch
            't2.Start()

            var players = new List<Player>();

            If TeamID <> 0 Or LoweredSearchName <> "" Or PositionID <> 0 Then

                players = Helpers.GetAllActivePlayers()

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

            'Dim competitionWeeks As List(Of Integer) = Helpers.GetCompetitionWeekIdsForCompetitionID(competitionId)

            Dim LastWeek As Week = (From o As Week In db.Weeks Where o.CompetitionID.Equals(CompetitionId) AndAlso o.WeekNo.Equals(CurrentWeek.WeekNo - 1)).FirstOrDefault

            Dim thisWeeksListData As New List(Of TheList)

            If LastWeek IsNot Nothing Then

                Dim playerIDs As List(Of Integer) = (From o In players Select o.PlayerID).ToList

                If playerIDs.Count > 0 Then
                    thisWeeksListData = (From o As TheList In db.TheLists Where o.WeekID.Equals(LastWeek.WeekID) AndAlso playerIDs.Contains(o.PlayerID)).ToList
                Else
                    thisWeeksListData = (From o As TheList In db.TheLists Where o.WeekID.Equals(LastWeek.WeekID)).ToList
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
      }


    }
}
