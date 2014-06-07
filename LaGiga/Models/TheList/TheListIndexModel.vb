Imports System.ComponentModel.DataAnnotations
Imports LaGiga.LaGiga.TheCompleteList

Namespace TheCompleteList

    Public Class TheListIndexModel

        Property PlayerList As IEnumerable(Of TheListPlayerDetailModel)

        Property NextPage As Integer
        Property PreviousPage As Integer
        Property PageCount As Integer
        Property CurrentPage As Integer
        Property SelectedPosition As Integer
        Property TenPagesLowLimit As Integer
        Property TenPagesHighLimit As Integer

        Property TeamTypeId As Helpers.TeamType

        Property IsAuthenticatedUser As Boolean
        Property LaGigaLogon As LogOnModel

        '' dictionary of playerID, userTeamID
        Property UserTeamPlayers As Dictionary(Of Integer, UserPlayer)

        <Display(Name:="Position")>
        Property PositionId As Integer

        <Display(Name:="Team")>
        Property TeamId As Integer

        <Display(Name:="Competition")>
        Property CompetitionId As Integer

        <Display(Name:="Surname")>
        Property SearchText As String

        Property CompetitionList As IEnumerable(Of SelectListItem)
        Property ClubTeams As List(Of SelectListItem)
        Property Positions As List(Of SelectListItem)

    End Class

End Namespace