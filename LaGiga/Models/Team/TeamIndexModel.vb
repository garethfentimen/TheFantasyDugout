Public Class TeamIndexModel

    Public Property SelectedTeamType() As Integer?
    Public Property selectedTeams() As IEnumerable(Of Team)
    Public Property TeamTypes() As IEnumerable(Of Helpers.TeamType)

    Property PageCount As Object
    Property PreviousPage As Object
    Property NextPage As Object

End Class
