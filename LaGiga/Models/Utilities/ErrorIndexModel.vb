Public Enum ErrorType
    IsError = 0
    Warning = 1
    Success = 2
End Enum

Public Class ErrorIndexModel

    Property Message As String
    Property ErrorType As ErrorType

End Class
