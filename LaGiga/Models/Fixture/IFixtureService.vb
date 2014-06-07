﻿Public Interface IFixtureService

    Function CreateFixture(ByVal FixtureToCreate As Fixture) As Boolean
    Function DeleteFixture(ByVal FixtureToDelete As Fixture) As Boolean
    Function EditFixture(ByVal FixtureToEdit As Fixture) As Boolean
    Function GetFixture(ByVal id As Integer) As Fixture
    Function ListFixture(ByVal CompetitionID As Integer?, ByVal searchValue As String, ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As IEnumerable(Of Fixture)
    Function ListFixture(ByVal pageSize As Integer?, ByVal pageIndex As Integer?, ByVal searchValue As String) As IEnumerable(Of Fixture)

    Function ListCompetitions() As IEnumerable(Of Competition)
    Function FixtureCount(ByVal CompetitionID As Integer?, ByVal searchValue As String) As Integer
    Function FixtureCount(ByVal searchValue As String) As Integer

    Function ListWeekDD() As IQueryable(Of SelectListItem)
    Function ListTeamDD() As IQueryable(Of SelectListItem)

    'event methods
    Function CreateEvent(ByVal EventToCreate As [Event]) As Boolean
    Function DeleteEvent(ByVal EventToDelete As [Event]) As Boolean
    Function EditEvent(ByVal EventToEdit As [Event]) As Boolean
    Function GetEvent(ByVal id As Integer) As [Event]
    Function GetEvents(ByVal FixtureID As Integer) As IEnumerable(Of [Event])
    Function ListEvent(ByVal EventID As Integer, ByVal pageSize As Integer?, ByVal pageIndex As Integer?) As IEnumerable(Of [Event])

    Function EventCount(ByVal EventID As Integer) As Integer

    Function ListPlayerDD() As List(Of SelectListItem)

    Function ListHomePlayerDDForFixture(ByVal id As Integer) As List(Of SelectListItem)
    Function ListAwayPlayerDDForFixture(ByVal id As Integer) As List(Of SelectListItem)

    Function AddEventCalculator(ByVal EventToCreate As [Event], ByVal FixtureEditDetail As FixtureEditModel) As [Event]

End Interface
