
Public Class PagingCalculator

    Private _pageIndex As Integer
    Private _totalPages As Integer

    Sub New(pageIndex As Integer, totalPages As Integer)
        _pageIndex = pageIndex
        _totalPages = totalPages

        Me.calculatePagingInfo()

    End Sub

    Public Property LowPageLimit As Integer
    Public Property HighPageLimit As Integer

    Private Sub calculatePagingInfo()
        If _pageIndex = 0 Then
            LowPageLimit = 1
            HighPageLimit = 10
        Else
            HighPageLimit = Math.Ceiling(_pageIndex / 10) * 10
            _pageIndex -= 1
            LowPageLimit = Math.Floor(_pageIndex / 10) * 10

            If LowPageLimit Mod 10 = 0 Then
                LowPageLimit += 1
            End If

            If HighPageLimit > _totalPages Then
                HighPageLimit = _totalPages
            End If
        End If
    End Sub
End Class
