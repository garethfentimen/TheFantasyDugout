Public Class PickPlayerIndexModel

    Property UserTeam As UserTeam

    Property FormPlayers As List(Of PlayerPickerForm)

    Property Week As Week

    Property sDisabled As String

    Property Fixtures As IEnumerable(Of UserFixtureCalculation)

End Class
