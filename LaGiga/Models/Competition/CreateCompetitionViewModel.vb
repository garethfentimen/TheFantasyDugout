Imports System.ComponentModel

Public Class CreateCompetitionViewModel

    <DisplayName("Competition Name")> _
    Public Property Name As String

    <DisplayName("Current Competition?")> _
    Public Property CurrentCompetition As Boolean

    <DisplayName("Competition Start Date")> _
    Public Property FromDate As Date

    <DisplayName("Competition End Date")> _
    Public Property ToDate As Date

    <DisplayName("Squad Size")> _
    Public Property SquadSize As Integer

    <DisplayName("Team Type")> _
    Public Property TeamTypeID As Integer

    Public Property TeamTypes As IEnumerable(Of SelectListItem)

End Class
