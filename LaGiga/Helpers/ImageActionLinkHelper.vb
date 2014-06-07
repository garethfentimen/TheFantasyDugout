Public Module ImageActionLinkHelper

    <System.Runtime.CompilerServices.Extension()> _
    Public Function ImageActionLink(ByVal helper As AjaxHelper, ByVal imageUrl As String, ByVal altText As String, ByVal actionName As String, ByVal routeValues As Object, ByVal ajaxOptions As AjaxOptions) As String
        Dim builder = New TagBuilder("img")
        builder.MergeAttribute("src", imageUrl)
        builder.MergeAttribute("alt", altText)
        builder.MergeAttribute("border", "0")
        Dim link = helper.ActionLink("[replaceme]", actionName, routeValues, ajaxOptions)

        Dim slink As String = link.ToString

        Return slink.Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing))
    End Function
End Module
