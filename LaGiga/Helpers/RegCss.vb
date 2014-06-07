
Public Module RegCss

    <System.Runtime.CompilerServices.Extension()> _
    Public Function RegCss(ByVal helper As System.Web.Mvc.HtmlHelper, ByVal siteCSS As String) As String
        Dim scriptRoot As String = VirtualPathUtility.ToAbsolute("~/stylesheets")
        Dim scriptFormat As String = "<link href='{0}/{1}' rel='stylesheet' type='text/css' />" & vbCrLf
        Return String.Format(scriptFormat, scriptRoot, siteCSS)
    End Function

End Module
