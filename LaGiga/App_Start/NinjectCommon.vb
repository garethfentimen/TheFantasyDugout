'Imports Ninject
'Imports Microsoft.Web.Infrastructure.DynamicModuleHelper
'Imports Ninject.Extensions.Conventions
'Imports Ninject.Web.Common

'<Assembly: WebActivator.PreApplicationStartMethod(GetType(Global.LaGiga.App_Start.Ninject), "StartNinject")> 
'<Assembly: WebActivator.ApplicationShutdownMethodAttribute(GetType(Global.LaGiga.App_Start.Ninject), "StopNinject")> 

'Namespace App_Start

'    Public Module Ninject
'        Private ReadOnly bootstrapper As New Bootstrapper()

'        ''' <summary>
'        ''' Starts the application
'        ''' </summary>
'        Public Sub StartNinject()
'            DynamicModuleUtility.RegisterModule(GetType(OnePerRequestHttpModule))
'            bootstrapper.Initialize(AddressOf CreateKernel)
'        End Sub

'        ''' <summary>
'        ''' Stops the application.
'        ''' </summary>
'        Public Sub StopNinject()
'            bootstrapper.ShutDown()
'        End Sub

'        ''' <summary>
'        ''' Creates the kernel that will manage your application.
'        ''' </summary>
'        ''' <returns>The created kernel.</returns>
'        Private Function CreateKernel() As IKernel
'            Dim kernel = New StandardKernel()
'            RegisterServices(kernel)
'            Return kernel
'        End Function

'        ''' <summary>
'        ''' Load your modules or register your services here!
'        ''' </summary>
'        ''' <param name="kernel">The kernel.</param>
'        Private Sub RegisterServices(ByVal kernel As IKernel)

'        End Sub
'    End Module

'End Namespace
