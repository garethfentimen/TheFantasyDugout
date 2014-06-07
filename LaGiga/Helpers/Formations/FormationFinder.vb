Namespace Formations

    Public Enum FormationType

        None = 0
        FourFourTwo = 1
        FiveThreeTwo = 2
        FourThreeThree = 2

    End Enum

    Class FormationFinder

        Private Property _playersInFootballTeam As Integer = 11

        Public Property ValidationDictionary As IValidationDictionary
        Protected Property _players As List(Of WeekUserPlayer)
        Protected Property _playersOnPitch As IEnumerable(Of WeekUserPlayer)

        Public Property FormationType As FormationType

        Public Sub New(ByVal incomingValidationDictionary As IValidationDictionary,
                       ByVal players As List(Of WeekUserPlayer))

            ValidationDictionary = incomingValidationDictionary
            _players = players
            CheckSquadSize()

            _playersOnPitch = _players.Where(Function(o) o.StatusID = Helpers.PlayingType.Playing)
            CorrectNumberOfPlayersPlaying()
            OneGKPlaying()

            FormationType = FormationType.None

            If FiveThreeTwo.Validate(_playersOnPitch) Then
                FormationType = Formations.FormationType.FiveThreeTwo
            End If

            If FourFourTwo.Validate(_playersOnPitch) Then
                FormationType = Formations.FormationType.FourFourTwo
            End If

            If FourThreeThree.Validate(_playersOnPitch) Then
                FormationType = Formations.FormationType.FiveThreeTwo
            End If

            If FormationType = Formations.FormationType.None Then
                incomingValidationDictionary.AddError("WeekUserPlayer", "Please choose players in either a 4-4-2, 4-3-3 or 5-3-2 formation.")
            End If

        End Sub

        ''' <summary>[Common Rule] Must be correct squad size</summary>
        Private Sub CheckSquadSize()
            If _players.Count <> Helpers.CurrentCompetition.SquadSize Then
                ValidationDictionary.AddError("WeekUserPlayer", "The squad size is incorrect it should be " & Helpers.CurrentCompetition.SquadSize & ".")
            End If
        End Sub

        ''' <summary>[Common Rule] Must be 11 players</summary>
        Private Sub CorrectNumberOfPlayersPlaying()
            If _playersOnPitch.Count <> _playersInFootballTeam Then
                ValidationDictionary.AddError("WeekUserPlayer", "You must select a team with 11 players in it. Come on, you should have at least worked this one out!")
            End If
        End Sub

        ''' <summary>[Common Rule] Can only be 1 GK</summary>
        Private Sub OneGKPlaying()
            If _playersOnPitch.Where(Function(o) Helpers.GetPlayerById(o.PlayerID).PositionID = Helpers.Position.GK).Count <> 1 Then
                ValidationDictionary.AddError("WeekUserPlayer", "You do not have 1 GoalKeeper(GK). Please pick 11 players to play in a 4-4-2 formation: 1 GK, 4 DF, 4 MF, 2 FW.")
            End If
        End Sub


    End Class

End Namespace
