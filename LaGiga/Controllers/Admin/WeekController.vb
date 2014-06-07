Namespace LaGiga
    Public Class WeekController
        Inherits System.Web.Mvc.Controller

        Const pageSize As Integer = 10
        Private _Repository As New WeekRepository

        <Authorize(Roles:="Administrator")> _
        Function WeekIndex(ByVal pageIndex As Integer?) As ActionResult

            Dim model As New WeekIndexModel With { _
                .selectedWeeks = _Repository.ListWeeks(pageSize, pageIndex),
                .PageCount = Math.Floor(_Repository.WeeksCount() / pageSize + 1),
                .PreviousPage = IIf(pageIndex > 1, pageIndex - 1, 1),
                .NextPage = IIf(pageIndex < .PageCount, pageIndex + 1, .PageCount),
                .CurrentWeek = IIf(IsNothing(_Repository.GetCurrentWeek()), Nothing, _Repository.GetCurrentWeek())
            }

            If Request.IsAjaxRequest() Then
                Return PartialView("WeekList", model)
            End If

            Return View("WeekIndex", model)

        End Function

        <Authorize(Roles:="Administrator")> _
        Function Create() As ActionResult
            ViewData("CompetitionTypes") = _Repository.GetCompetitionDD()
            Return View()
        End Function

        <Authorize(Roles:="Administrator")> _
        <HttpPost()> _
        Function Create(ByVal CompetitionTypes As Integer, ByVal WeekToCreate As Week) As ActionResult

            ''default to now if empty
            If Not WeekToCreate.FromDate.HasValue OrElse WeekToCreate.FromDate.Equals(Date.MinValue) Then
                WeekToCreate.FromDate = Now.Date
            End If

            If (Request.IsAjaxRequest) Then
                If _Repository.CreateWeek(CompetitionTypes, WeekToCreate) Then
                    Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.Success})
                Else
                    Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.IsError})
                End If
            End If

            Return RedirectToAction("TeamIndex")
        End Function

        <Authorize(Roles:="Administrator")> _
        Function Edit(ByVal id As Integer) As ActionResult
            Return View(_Repository.GetWeek(id))
        End Function

        <Authorize(Roles:="Administrator")> _
        <AcceptVerbs(HttpVerbs.Post)> _
        Function Save(ByVal WeekToEdit As Week) As ActionResult

            If _Repository.EditWeek(WeekToEdit) Then
                Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.Success})
            Else
                Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.IsError})
            End If
            Return View()
        End Function

        <AcceptVerbs(HttpVerbs.Delete), ActionName("Delete")> _
        Public Function AjaxDelete(ByVal id As Integer, ByVal PageIndex As Integer?) As ActionResult
            ' Get week
            Dim WeekToDelete = _Repository.GetWeek(id)

            ' Delete from database
            _Repository.DeleteWeek(WeekToDelete)

            Dim model As New WeekIndexModel With { _
                .selectedWeeks = _Repository.ListWeeks(pageSize, PageIndex),
                .PageCount = Math.Floor((_Repository.WeeksCount() / pageSize) + 1),
                .PreviousPage = IIf(PageIndex > 1, PageIndex - 1, 1),
                .NextPage = IIf(PageIndex < .PageCount, PageIndex + 1, .PageCount)
            }

            ' Return week List
            If Request.IsAjaxRequest() Then
                Return PartialView("WeekList", model)
            End If

            Return View("WeekList", model)
        End Function

        <Authorize(Roles:="Administrator")> _
        <AcceptVerbs(HttpVerbs.Post)> _
        Function SetResultTables(ByVal WeekID As Integer) As ActionResult

            Dim seasonResultCollection As New List(Of SeasonResult)
            Dim currentCompetitionID As Integer = Helpers.CurrentCompetition.CompetitionID
            Dim week As Week = _Repository.GetWeek(WeekID)


            Dim newWeek As New Week
            If week.CompetitionID <> currentCompetitionID Then
                '' must be a new competition so get first week
                newWeek = _Repository.GetWeekByCompetitionAndNo(1, currentCompetitionID)
            Else
                newWeek = _Repository.GetWeekByCompetitionAndNo(week.WeekNo + 1, currentCompetitionID)
            End If

            If IsNothing(newWeek) Then

                '' if we don't have new week to go to then error safely
                Return PartialView("results", New ErrorIndexModel With {
                                   .Message = String.Format("Please add a week to go to next. This will be week {0} in competition {1}. " &
                                                "If this competiton has finished add a new competition and week starting at week number 1.", week.WeekNo + 1, week.Competition.Name),
                                   .ErrorType = ErrorType.IsError})

            Else

                For Each userGroup As UserGroup In _Repository.GetAllUserGroups()

                    Dim userTeams As List(Of UserTeam) = _Repository.GetUserTeamsInUserGroup(userGroup.UserGroupID)

                    For Each userTeam As UserTeam In userTeams

                        Dim resultCollection As New List(Of Result)
                        '' collect the weeks results
                        resultCollection = _Repository.getUserResults(userTeam.UserTeamID, currentCompetitionID)

                        '' create additional season result as the actual season result records
                        Dim seasonResult As New SeasonResult
                        If Not IsNothing(resultCollection) AndAlso resultCollection.Count > 0 Then
                            '' loop around week results adding the total points achieved to new collection (seasonResultCollection)
                            For Each oResult In resultCollection

                                seasonResult.Points = oResult.TotalScore

                            Next oResult
                        End If

                        seasonResult.WeekID = WeekID
                        seasonResult.UserTeamID = userTeam.UserTeamID
                        seasonResultCollection.Add(seasonResult)

                    Next userTeam

                    Dim db As New LaGigaClassesDataContext

                    Dim userFixtures As List(Of UserFixtureCalculation) = (From c In db.UserFixtureCalculations Where c.WeekNo = Helpers.CalculateWeekNumber(userGroup, week.WeekNo) _
                                                                           AndAlso c.NoPlayers.Equals(userGroup.NoPlayers)).ToList()

                    '' if we're changing competition don't bother as will have a week called END 
                    '' Otherwise work out who won, drew etc..
                    ''
                    If week.CompetitionID.Equals(currentCompetitionID) Then
                        '' Loop around all the week results for each user team
                        '' week results represented by a season result record with "points" being the weeks total score
                        ''
                        For Each seasonResult As SeasonResult In seasonResultCollection
                            Dim OppositionUserTeamID As Integer = 0
                            Dim OppositionPoints As Double = 0

                            For Each userFixture In userFixtures
                                If userFixture.UserTeamIDA.Equals(seasonResult.UserTeamID) OrElse userFixture.UserTeamIDB.Equals(seasonResult.UserTeamID) Then

                                    If userFixture.UserTeamIDA.Equals(seasonResult.UserTeamID) Then
                                        OppositionUserTeamID = userFixture.UserTeamIDB
                                    Else
                                        OppositionUserTeamID = userFixture.UserTeamIDA
                                    End If

                                    '' find the opposition score
                                    If OppositionPoints = 0 Then
                                        For Each oppositionTeam In seasonResultCollection
                                            If oppositionTeam.UserTeamID.Equals(OppositionUserTeamID) Then
                                                OppositionPoints = oppositionTeam.Points
                                                Exit For
                                            End If
                                        Next
                                    End If

                                End If
                            Next

                            Select Case seasonResult.Points
                                Case Is > OppositionPoints
                                    seasonResult.Won = 1
                                Case Is = OppositionPoints
                                    seasonResult.Drawn = 1
                                Case Is < OppositionPoints
                                    seasonResult.Lost = 1
                            End Select

                            OppositionPoints = 0
                            OppositionUserTeamID = 0
                        Next
                    End If
                Next

                '' Populate The List
                Dim res As ErrorIndexModel = _Repository.PopulateTheList(week)
                If res.ErrorType = ErrorType.Success Then

                    '' insert season results into db
                    If _Repository.AddSeasonResult(seasonResultCollection) Then
                        week.CurrentWeek = False
                        week.ToDate = Now.Date
                        _Repository.EditWeek(week)

                        newWeek.CurrentWeek = True
                        _Repository.EditWeek(week)
                        Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.Success})
                    Else
                        Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.IsError})
                    End If

                Else
                    Return PartialView("results", New ErrorIndexModel With {.ErrorType = ErrorType.IsError, .Message = res.Message})
                End If
            End If

        End Function

        <Authorize(Roles:="Administrator")> _
        <HttpPost()> _
        Function PopulateHistoricalListData() As ActionResult

            Return PartialView("results", _Repository.populateListBackData())
        End Function

    End Class
End Namespace