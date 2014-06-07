Imports LaGiga.Helpers

Public Class FixtureRepository
    Inherits StandardFunctions
    Implements IFixtureRepository

    Public Function CreateFixture(ByVal FixtureToCreate As Fixture) As Fixture Implements IFixtureRepository.CreateFixture
        db.Fixtures.InsertOnSubmit(FixtureToCreate)
        db.SubmitChanges()
        Return FixtureToCreate
    End Function

    Public Function DeleteFixture(ByVal FixtureToDelete As Fixture) As Fixture Implements IFixtureRepository.DeleteFixture
        'also delete fixture events
        Dim oEvents = From c In db.Events Where c.FixtureID = FixtureToDelete.FixtureID
        For Each oevent In oEvents
            db.Events.DeleteOnSubmit(oevent)
        Next oevent
        db.Fixtures.DeleteOnSubmit(FixtureToDelete)
        db.SubmitChanges()

        Return FixtureToDelete
    End Function

    Public Function EditFixture(ByVal FixtureToEdit As Fixture) As Fixture Implements IFixtureRepository.EditFixture
        Dim originalFixture = (From c In db.Fixtures Where c.FixtureID = FixtureToEdit.FixtureID).Single()
        originalFixture.HomeTeamID = FixtureToEdit.HomeTeamID
        originalFixture.AwayTeamID = FixtureToEdit.AwayTeamID
        originalFixture.WeekID = FixtureToEdit.WeekID
        originalFixture.HomeScore = FixtureToEdit.HomeScore
        originalFixture.AwayScore = FixtureToEdit.AwayScore
        db.SubmitChanges()
        Return FixtureToEdit
    End Function

    Public Function FixtureCount(ByVal CompetitionID As Integer?, ByVal searchValue As String) As Integer Implements IFixtureRepository.FixtureCount
        If searchValue = "" Then
            Return (From c In db.Fixtures Join d In db.Weeks On d.WeekID Equals c.WeekID Where d.CompetitionID = CompetitionID).Count()
        End If
        Return (From c In db.Fixtures Join d In db.Weeks On d.WeekID Equals c.WeekID Where d.CompetitionID = CompetitionID And c.Week.WeekName.Contains(searchValue)).Count()
    End Function

    Public Function FixtureCount(ByVal searchValue As String) As Integer Implements IFixtureRepository.FixtureCount
        If searchValue = "" Then
            Return (From c In db.Fixtures).Count()
        End If
        Return (From c In db.Fixtures Where c.Week.WeekName.Contains(searchValue)).Count()
    End Function

    Public Function GetFixture(ByVal id As Integer) As Fixture Implements IFixtureRepository.GetFixture
        Dim oFixture = (From c In db.Fixtures _
          Where c.FixtureID = id _
          Select c).FirstOrDefault()
        Return oFixture
    End Function

    Public Function ListCompetitions() As System.Collections.Generic.IEnumerable(Of Competition) Implements IFixtureRepository.ListCompetitions
        Return (From c In db.Competitions).ToList()
    End Function

    Public Function ListFixture(ByVal CompetitionID As Integer?, ByVal searchValue As String, ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As System.Collections.Generic.IEnumerable(Of Fixture) Implements IFixtureRepository.ListFixture
        Dim oFixtures = From c In db.Fixtures Join d In db.Weeks On d.WeekID Equals c.WeekID Where d.CompetitionID = CompetitionID And d.WeekName.Contains(searchValue) Order By d.WeekNo Descending Select c
        If pageSize.HasValue And pageIndex.HasValue Then
            Return oFixtures.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList()
        End If
        Return oFixtures.ToList()
    End Function

    Public Function ListFixture(ByVal pageSize As Integer?, ByVal pageIndex As Integer?, ByVal searchValue As String) As System.Collections.Generic.IEnumerable(Of Fixture) Implements IFixtureRepository.ListFixture
        Dim oFixtures = From c In db.Fixtures Where c.Week.WeekName.Contains(searchValue) Order By c.WeekID Descending Select c
        If pageSize.HasValue And pageIndex.HasValue Then
            Return oFixtures.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList()
        End If
        Return oFixtures.ToList()
    End Function


    Function ListWeekDD() As IQueryable(Of SelectListItem) Implements IFixtureRepository.ListWeekDD
        Return From c In db.Weeks Order By c.WeekID Descending Select New SelectListItem _
               With {.Value = c.WeekID, _
                          .Text = c.WeekName}
    End Function

    Function ListTeamDD() As IQueryable(Of SelectListItem) Implements IFixtureRepository.ListTeamDD
        Dim oWeek = GetCurrentWeek()
        Dim TeamTypeID = 1
        If oWeek.Competition.Name.Contains("Premier League") Then
            TeamTypeID = 2
        Else
            TeamTypeID = 1
        End If
        Return From c In db.Teams Where c.TeamTypeID = TeamTypeID And c.TeamName <> "None" Order By c.TeamName Select New SelectListItem _
               With {.Value = c.TeamID, _
                          .Text = c.TeamName}
    End Function

    'Event Methods

    Public Function CreateEvent(ByVal EventToCreate As [Event]) As [Event] Implements IFixtureRepository.CreateEvent
        db.Events.InsertOnSubmit(EventToCreate)
        db.SubmitChanges()
        Return EventToCreate
    End Function

    Public Sub CreateEvents(ByVal EventsToCreate As List(Of [Event])) Implements IFixtureRepository.CreateEvents
        db.Events.InsertAllOnSubmit(EventsToCreate)
        db.SubmitChanges()
    End Sub

    Public Function DeleteEvent(ByVal EventToDelete As [Event]) As [Event] Implements IFixtureRepository.DeleteEvent
        db.Events.DeleteOnSubmit(EventToDelete)
        db.SubmitChanges()
        Return EventToDelete
    End Function

    Public Function EditEvent(ByVal EventToEdit As [Event]) As [Event] Implements IFixtureRepository.EditEvent
        Dim originalEvent = (From c In db.Events Where c.EventID = EventToEdit.EventID).Single()
        originalEvent.EventTypeID = EventToEdit.EventTypeID
        originalEvent.FixtureID = EventToEdit.FixtureID
        originalEvent.PlayerID = EventToEdit.PlayerID
        originalEvent.FromMinute = EventToEdit.FromMinute
        originalEvent.ToMinute = EventToEdit.ToMinute
        db.SubmitChanges()
        Return EventToEdit
    End Function

    Public Function EventCount(ByVal EventID As Integer) As Integer Implements IFixtureRepository.EventCount
        Return (From c In db.Events Join d In db.Fixtures On d.FixtureID Equals c.FixtureID Where d.FixtureID = EventID).Count()
    End Function

    Public Function GetEvent(ByVal id As Integer) As [Event] Implements IFixtureRepository.GetEvent
        Dim oEvent = (From c In db.Events _
          Where c.EventID = id _
          Select c).FirstOrDefault()
        Return oEvent
    End Function

    Public Function ListEvent(ByVal EventID As Integer, ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As System.Collections.Generic.IEnumerable(Of [Event]) Implements IFixtureRepository.ListEvent
        Dim oEvents = From c In db.Events Where c.FixtureID = EventID Order By c.FromMinute
        If pageSize.HasValue And pageIndex.HasValue Then
            Return oEvents.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList()
        End If
        Return oEvents.ToList()
    End Function


    Function ListPlayerDD() As List(Of SelectListItem) Implements IFixtureRepository.ListPlayerDD
        Return (From c In db.Players Order By c.PositionID Select New SelectListItem _
               With {.Value = c.PlayerID, _
                          .Text = (c.FirstName & " " & c.Surname)}).ToList()
    End Function

    Function ListHomePlayerDDForFixture(ByVal id As Integer) As List(Of SelectListItem) Implements IFixtureRepository.ListHomePlayerDDForFixture
        Dim oFixture = GetFixture(id)
        Dim HomeTeamID = oFixture.HomeTeamID

        Dim oPlayersForFixture = (From c In db.Players Where c.TeamID = HomeTeamID Order By c.TeamID, c.PositionID)

        Dim selectablePlayerDD As New List(Of SelectListItem)

        For Each player As Player In oPlayersForFixture

            selectablePlayerDD.Add(New SelectListItem _
                        With {.Value = player.PlayerID, _
                          .Text = (player.Surname & ", " & player.FirstName & " " & [Enum].GetName(GetType(Position), player.PositionID))}
                      )

        Next

        Return selectablePlayerDD

    End Function

    Function ListAwayPlayerDDForFixture(ByVal id As Integer) As List(Of SelectListItem) Implements IFixtureRepository.ListAwayPlayerDDForFixture
        Dim oFixture = GetFixture(id)
        Dim AwayTeamID = oFixture.AwayTeamID

        Dim oPlayersForFixture = (From c In db.Players Where c.TeamID = AwayTeamID Or c.TeamID = AwayTeamID Order By c.TeamID, c.PositionID)

        Dim selectablePlayerDD As New List(Of SelectListItem)

        For Each player As Player In oPlayersForFixture

            selectablePlayerDD.Add(New SelectListItem _
                        With {.Value = player.PlayerID, _
                          .Text = (player.Surname & ", " & player.FirstName & " " & [Enum].GetName(GetType(Position), player.PositionID))}
                      )

        Next

        Return selectablePlayerDD

    End Function

    Function GetEvents(ByVal FixtureID As Integer) As IEnumerable(Of [Event]) Implements IFixtureRepository.GetEvents
        Return (From c In db.Events Where c.FixtureID = FixtureID Order By c.FromMinute Select c)
    End Function

    Function GetCurrentWeek() As Week Implements IFixtureRepository.GetCurrentWeek
        Return Helpers.CurrentWeek()
    End Function

    Function AddEventCalculator(ByVal EventToCreate As [Event], ByVal FixtureEditDetail As FixtureEditModel) As [Event] Implements IFixtureRepository.AddEventCalculator
        Dim CalcEvent As New AddEventCalculator()
        CalcEvent.oFixtureDetail = GetFixture(FixtureEditDetail.Fixture.FixtureID)
        Dim ReturnEvent As [Event] = CalcEvent.Calculate(EventToCreate, FixtureEditDetail)

        If Not IsNothing(CalcEvent.o_lCleanSheetEvent) And CalcEvent.o_lCleanSheetEvent.Count <> 0 Then
            CreateEvents(CalcEvent.o_lCleanSheetEvent)
        End If

        If Not IsNothing(CalcEvent.o_lConcedeGoalEvent) And CalcEvent.o_lConcedeGoalEvent.Count <> 0 Then
            CreateEvents(CalcEvent.o_lConcedeGoalEvent)
        End If

        Return ReturnEvent
    End Function
End Class
