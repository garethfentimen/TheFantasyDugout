Class UserPlayerStaticClass

    Shared c_iUserTeamID As Integer

    Property UserTeamID As Integer
        Get
            UserTeamID = c_iUserTeamID
        End Get
        Set(ByVal value As Integer)
            c_iUserTeamID = value
        End Set
    End Property

End Class
