Imports LaGiga
Imports LaGiga.Helpers

Namespace TheCompleteList

    Public Class TheListModelAdapter

        Private Property UserTeamPlayers As Dictionary(Of Integer, UserPlayer)

        Public Sub New(ByVal UserTeamPlayers As Dictionary(Of Integer, UserPlayer))
            Me.UserTeamPlayers = UserTeamPlayers
        End Sub

        Public Function AdaptTheListDataToModel(ByVal PlayerDetailList As List(Of TheList)) As IEnumerable(Of TheListPlayerDetailModel)

            Dim modelListToReturn As IEnumerable(Of TheListPlayerDetailModel)

            modelListToReturn = PlayerDetailList.Select(Of TheListPlayerDetailModel)(Function(o) Transform(o))

            Return modelListToReturn
        End Function

        Private Function Transform(ByVal pDL As TheList) As TheListPlayerDetailModel
            Dim toReturn
            Try
                Dim UserTeamName = String.Empty

                If UserTeamPlayers.ContainsKey(pDL.Player.PlayerID) Then
                    UserTeamName = UserTeamPlayers.Item(pDL.Player.PlayerID).UserTeam.Name
                End If

                toReturn = New TheListPlayerDetailModel With {
                        .PlayerID = pDL.PlayerID,
                        .FirstName = pDL.Player.FirstName,
                        .Surname = pDL.Player.Surname,
                        .Position = [Enum].GetName(GetType(Position), pDL.Player.PositionID),
                        .Team = IIf(CurrentCompetition.TeamTypeID = TeamType.Club, pDL.Player.Team.TeamName, pDL.Player.Team1.TeamName),
                        .LastWeeksPoints = pDL.WeekPoints.ToString(),
                        .TotalPoints = pDL.TotalPoints.ToString(),
                        .UserTeamName = UserTeamName
                    }

            Catch ex As Exception
                Debug.WriteLine("EEEEK")
            End Try

            Return toReturn
        End Function

    End Class

End Namespace
