Public Class ApplicationRoutes

    Shared Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")

        routes.MapRoute(
           "TheListIndex",
           "TheList/{action}/{pageIndex}",
           New With {.controller = "TheList", .action = "GetTheList", .pageIndex = 1}
       )

        routes.MapRoute(
           "LeagueIndex",
           "League/{action}",
           New With {.controller = "League", .action = "LeagueIndex"}
       )

        routes.MapRoute(
            "CompetitionIndex",
            "Competition/{action}",
            New With {.controller = "Competition", .action = "Index"}
        )

        ''Route to get from logon user control in shared to logon in accopunt controller
        routes.MapRoute(
            "logonUserCtrl",
            "Shared/{action}",
            New With {.controller = "Account", .action = "Logon"}
        )

        routes.MapRoute( _
            "Logon", _
            "Logon", _
            New With {.controller = "Account", .action = "Logon"} _
        )

        routes.MapRoute(
            "PlayerHomeRoute",
            "PlayerHome/{action}",
            New With {.controller = "PlayerHome", .action = "PlayerHomeIndex"}
        )

        routes.MapRoute(
            "CreateUserPlayer",
            "CreateUserPlayer/{action}",
            New With {.controller = "UserPlayer", .action = "CreateUserPlayer"}
        )

        routes.MapRoute(
            "UserPlayerIndex",
            "UserPlayers/All/{UserTeamID}",
            New With {.controller = "UserPlayer", .action = "UserPlayerIndex", .UserTeamID = 0}
        )

        routes.MapRoute(
            "UserTeams",
            "UserTeams/All/{PageIndex}",
            New With {.controller = "UserTeam", .action = "UserTeamIndex", .PageIndex = 1}
        )

        routes.MapRoute(
            "EventTypes",
            "EventTypes/All/{PageIndex}",
            New With {.controller = "EventType", .action = "EventTypeIndex", .PageIndex = 1}
        )

        routes.MapRoute(
            "EventType",
            "EventTypes/All/{PageIndex}",
            New With {.controller = "EventType", .action = "EventTypeIndex", .PageIndex = UrlParameter.Optional}
        )

        routes.MapRoute(
            "All Fixtures",
            "Fixtures/All/{PageIndex}",
            New With {.controller = "Fixture", .action = "FixtureIndex", .CompetitionID = 0, .PageIndex = 1}
        )

        routes.MapRoute(
            "World Cup Fixtures",
            "Fixtures/WorldCup/{PageIndex}",
            New With {.controller = "Fixture", .action = "FixtureIndex", .CompetitionID = 1, .PageIndex = 1}
        )

        routes.MapRoute(
            "Premier League Fixtures",
            "Fixtures/PremierLeague/{PageIndex}",
            New With {.controller = "Fixture", .action = "FixtureIndex", .CompetitionID = 2, .PageIndex = 1}
        )

        routes.MapRoute(
            "All Weeks",
            "Weeks/All/{PageIndex}",
            New With {.controller = "Week", .action = "WeekIndex", .PageIndex = 1}
        )

        routes.MapRoute(
            "Weeks",
            "Weeks/{action}/{id}",
            New With {.controller = "Week", .action = "WeekIndex", .id = UrlParameter.Optional}
        )

        routes.MapRoute(
            "All Players",
            "Players/All/{PageIndex}",
            New With {.controller = "Player", .action = "PlayerIndex", .TeamID = 0, .PositionID = 0, .PageIndex = 1}
        )

        routes.MapRoute(
           "GoalKeepers",
           "Players/GoalKeepers/{PageIndex}",
           New With {.controller = "Player", .action = "PlayerIndex", .PositionID = 1, .PageIndex = 1}
        )

        routes.MapRoute(
           "Defenders",
           "Players/Defenders/{PageIndex}",
           New With {.controller = "Player", .action = "PlayerIndex", .PositionID = 2, .PageIndex = 1}
        )

        routes.MapRoute(
           "Midfielders",
           "Players/Midfielders/{PageIndex}",
           New With {.controller = "Player", .action = "PlayerIndex", .PositionID = 3, .PageIndex = 1}
        )

        routes.MapRoute(
           "Forwards",
           "Players/Forwards/{PageIndex}",
           New With {.controller = "Player", .action = "PlayerIndex", .PositionID = 4, .PageIndex = 1}
        )

        routes.MapRoute(
            "All Teams",
            "Teams/All/{PageIndex}",
            New With {.controller = "Team", .action = "TeamIndex", .TeamTypeID = 0, .PageIndex = 1}
        )

        routes.MapRoute(
           "NationalTeams",
           "Teams/National/{PageIndex}",
           New With {.controller = "Team", .action = "TeamIndex", .TeamTypeID = 1, .PageIndex = 1}
        )

        routes.MapRoute(
            "ClubTeams",
            "Teams/Club/{PageIndex}",
            New With {.controller = "Team", .action = "TeamIndex", .TeamTypeID = 2, .PageIndex = 1}
        )

        routes.MapRoute(
            "Teams",
            "Teams/{action}/{id}",
            New With {.controller = "Team", .action = "TeamIndex", .id = UrlParameter.Optional}
        )

        ' MapRoute takes the following parameters, in order:
        ' (1) Route name
        ' (2) URL with parameters
        ' (3) Parameter defaults
        routes.MapRoute( _
            "Default", _
            "{controller}/{action}/{id}", _
            New With {.controller = "Home", .action = "Index", .id = UrlParameter.Optional} _
        )

        routes.MapRoute( _
            "Register", _
            "{controller}/{action}/{id}", _
            New With {.controller = "Account", .action = "Register", .id = UrlParameter.Optional} _
        )

    End Sub

End Class
