' Note: For instructions on enabling IIS6 or IIS7 classic mode, 
' visit http://go.microsoft.com/?LinkId=9394802
Imports LaGiga.App_Start

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Sub Application_Start()
        AreaRegistration.RegisterAllAreas()

        ApplicationRoutes.RegisterRoutes(RouteTable.Routes)

        AutoMapperCommon.RegisterMappings()
    End Sub

    Sub Application_Error()
        Dim ex As Exception = Server.GetLastError
        Response.Write("ex.ToString")
    End Sub
End Class
