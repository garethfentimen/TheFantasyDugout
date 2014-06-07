Public Class UserGroupRepository
    Inherits StandardFunctions

    Public Function GetUserGroupList() As IEnumerable(Of SelectListItem)

        Return (From o As UserGroup In db.UserGroups _
                Select New SelectListItem With {.Text = o.Name, .Value = o.UserGroupID})

    End Function

End Class
