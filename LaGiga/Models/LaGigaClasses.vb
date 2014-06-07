Partial Class Team

    Public ReadOnly Property TeamTypeValues As IEnumerable(Of SelectListItem)
        Get
            Return From teamType In New TeamRepository().ListTeamTypes()
                   Select New SelectListItem() With {
                       .Text = [Enum].GetName(GetType(Helpers.TeamType), teamType),
                       .Value = teamType
                   }
        End Get
    End Property

End Class

Partial Class Player

    Public ReadOnly Property PositionValues As IEnumerable(Of SelectListItem)
        Get
            Return From Position In New PlayerRepository().ListPositionsDD()
        End Get
    End Property

    Public ReadOnly Property NationalTeamValues As IEnumerable(Of SelectListItem)
        Get
            Return From Team In New PlayerRepository().GetNationalTeams()
        End Get
    End Property

    Public ReadOnly Property ClubTeamValues As IEnumerable(Of SelectListItem)
        Get
            Return From Team In New PlayerRepository().GetClubTeams()
        End Get
    End Property
End Class

Partial Class Fixture

    Public ReadOnly Property HomeTeamTypeValues As IEnumerable(Of SelectListItem)
        Get
            Return From Fixture In New FixtureRepository().ListTeamDD()
        End Get
    End Property

    Public ReadOnly Property AwayTeamTypeValues As IEnumerable(Of SelectListItem)
        Get
            Return From Fixture In New FixtureRepository().ListTeamDD()
        End Get
    End Property

    Public ReadOnly Property WeekTypeValues As IEnumerable(Of SelectListItem)
        Get
            Return From Fixture In New FixtureRepository().ListWeekDD()
        End Get
    End Property

End Class

Partial Class EventType

    Public ReadOnly Property PositionTypeValues As IEnumerable(Of SelectListItem)
        Get
            Return From EventType In New EventTypeRepository().ListPositionDD()
        End Get
    End Property

End Class

Partial Class week

    Public ReadOnly Property CompetitionTypeValues As IEnumerable(Of SelectListItem)
        Get
            Return From week In New WeekRepository().GetAllCompetitionsDD
        End Get
    End Property

End Class

Partial Class [event]

    Public ReadOnly Property EventTypeValues As IEnumerable(Of SelectListItem)
        Get
            Return From [event] In Helpers.EventTypeList
        End Get
    End Property

    Public ReadOnly Property EventTypeValuesWithZero As IEnumerable(Of SelectListItem)
        Get
            Dim initiallist As New List(Of SelectListItem)
            initiallist = From [event] In Helpers.EventTypeList
            Dim newitem As New SelectListItem With {.Value = 0, .Text = "**Please choose**", .Selected = True}
            initiallist.add(newitem)
            Return initiallist
        End Get
    End Property

    Public ReadOnly Property PlayerDDValues As IEnumerable(Of SelectListItem)
        Get
            Return From player In New FixtureRepository().ListPlayerDD()
        End Get
    End Property

End Class

Partial Class UserTeam
    Public ReadOnly Property UserGroupValues As IEnumerable(Of SelectListItem)
        Get
            Return From UserGroup In New UserTeamRepository().UserGroupDDList()
        End Get
    End Property
End Class

Partial Class UserGroup
    Public ReadOnly Property UserGroupTypes As IEnumerable(Of SelectListItem)
        Get
            Return From Status In New UserGroupRepository().GetUserGroupList()
        End Get
    End Property
End Class
