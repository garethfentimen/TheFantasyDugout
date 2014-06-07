Public Module RegScript

    <System.Runtime.CompilerServices.Extension()> _
    Public Function RegScript(ByVal helper As System.Web.Mvc.HtmlHelper, ByVal scriptLib As String) As String
        Dim scriptRoot As String = VirtualPathUtility.ToAbsolute("~/Scripts")
        Dim scriptFormat As String = "<script src='{0}/{1}' type='text/javascript'></script>" & vbCrLf
        Return String.Format(scriptFormat, scriptRoot, scriptLib)
    End Function

End Module
