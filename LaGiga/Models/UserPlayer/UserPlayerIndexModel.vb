Public Class UserPlayerIndexModel

    Property selectedUserPlayers As IEnumerable(Of UserPlayer)

    Property PageCount As Integer
    Property PreviousPage As Integer
    Property NextPage As Integer

    Property ClubTeams As IEnumerable(Of SelectListItem)

    Property UserTeamID As Integer

    Property UserGroupID As Integer

    Property UserTeam As UserTeam

    Property CompetitionID As Integer

    Property CompetitionTypes As Object

    Property RecordCount As Integer


End Class