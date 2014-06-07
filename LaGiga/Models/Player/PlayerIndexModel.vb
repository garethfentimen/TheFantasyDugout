Public Class PlayerIndexModel

    Property SelectedPosition As Integer?
    Property selectedPlayers As IEnumerable(Of Player)
    Property positionTypes As Dictionary(Of String, Integer)

    Property PageCount As Object
    Property PreviousPage As Object
    Property NextPage As Object

    Property ClubTeams As List(Of SelectListItem)

    Property selectedTeam As Integer?

End Class
