
Namespace Formations

    Class FiveThreeTwo

        Public Shared Function Validate(ByVal playersOnPitch As IEnumerable(Of WeekUserPlayer)) As Boolean

            If playersOnPitch.Where(Function(o) Helpers.GetPlayerById(o.PlayerID).PositionID = Helpers.Position.DF).Count = 5 AndAlso
                playersOnPitch.Where(Function(o) Helpers.GetPlayerById(o.PlayerID).PositionID = Helpers.Position.MF).Count = 3 AndAlso
                playersOnPitch.Where(Function(o) Helpers.GetPlayerById(o.PlayerID).PositionID = Helpers.Position.FW).Count = 2 Then
                Return True
            End If

            Return False

        End Function
    End Class

End Namespace