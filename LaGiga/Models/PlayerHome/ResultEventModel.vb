Public Class ResultEventModel
    Inherits StandardFunctions

    'Week Properties
    Property WeekName As String
    'Fixture Properties
    Property Fixtures As String
    Property CompetitionName As String
    'Event Properties
    Property Goals As Integer
    Property Assists As Integer
    Property GoalsConceded As Integer
    Property CleanSheets As Integer
    Property YellowCards As Integer
    Property RedCards As Integer
    Property WeekPoints As Double
    Property MinutesOnPitch As Integer
    Property TotalPoints As Double
    Property AveragePoints As Double

    ' Transfer properties
    Property IsTransferREM As Boolean
    Property FromTeam As String
    Property ToTeam As String
    Property TransferFee As Decimal

    Property CurrentListData As List(Of TheList)

End Class
