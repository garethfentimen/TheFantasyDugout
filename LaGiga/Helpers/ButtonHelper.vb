Public Module ButtonHelper

    <System.Runtime.CompilerServices.Extension()> _
    Public Function ButtonHelper(ByVal helper As AjaxHelper, ByVal nameText As String, ByVal actionName As String, ByVal classID As String, ByVal routeValues As Object, ByVal ajaxOptions As AjaxOptions) As String
        Dim builder = New TagBuilder("input")
        builder.MergeAttribute("Type", "button")
        builder.MergeAttribute("name", nameText)
        builder.MergeAttribute("value", nameText)
        builder.MergeAttribute("class", classID)
        Dim link = helper.ActionLink("[replaceme]", actionName, routeValues, ajaxOptions)

        Dim slink As String = link.ToString

        Return slink.Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing))
    End Function
End Module
