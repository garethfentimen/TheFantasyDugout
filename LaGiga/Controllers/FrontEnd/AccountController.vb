﻿Imports System.Diagnostics.CodeAnalysis
Imports System.Security.Principal
Imports System.Web.Routing

<HandleError()> _
Public Class AccountController
    Inherits System.Web.Mvc.Controller

    Private formsServiceValue As IFormsAuthenticationService
    Private membershipServiceValue As IMembershipService

    Public Property FormsService() As IFormsAuthenticationService
        Get
            Return formsServiceValue
        End Get
        Set(ByVal value As IFormsAuthenticationService)
            formsServiceValue = value
        End Set
    End Property

    Public Property MembershipService() As IMembershipService
        Get
            Return membershipServiceValue
        End Get
        Set(ByVal value As IMembershipService)
            membershipServiceValue = value
        End Set
    End Property

    Protected Overrides Sub Initialize(ByVal requestContext As RequestContext)
        If FormsService Is Nothing Then FormsService = New FormsAuthenticationService()
        If MembershipService Is Nothing Then MembershipService = New AccountMembershipService()

        MyBase.Initialize(requestContext)
    End Sub

    ' **************************************
    ' URL: /Account/LogOn
    ' **************************************

    Public Function LogOn() As ActionResult

        Return View()
    End Function

    <HttpPost()> _
    Public Function LogOn(ByVal model As LogOnModel, ByVal returnUrl As String) As ActionResult
        If ModelState.IsValid Then
            If MembershipService.ValidateUser(model.UserName, model.Password) Then
                FormsService.SignIn(model.UserName, model.RememberMe)
                If Not String.IsNullOrEmpty(returnUrl) Then
                    Return Redirect(returnUrl)
                Else
                    Return RedirectToAction("PlayerHomeIndex", "PlayerHome")
                End If
            Else
                ModelState.AddModelError("", "The user name or password provided is incorrect.")
            End If
        End If

        ' If we got this far, something failed, redisplay form
        Return View(model)
    End Function

    ' **************************************
    ' URL: /Account/LogOff
    ' **************************************

    Public Function LogOff() As ActionResult
        FormsService.SignOut()

        Return RedirectToAction("Index", "Home")
    End Function

    ' **************************************
    ' URL: /Account/Register
    ' **************************************

    Public Function Register() As ActionResult
        ViewData("PasswordLength") = MembershipService.MinPasswordLength
        Dim model As New LaGigaRegisterIndexModel With {
            .RegModel = New RegisterModel,
            .UserGroup = New UserGroup
        }

        Return View(model)
    End Function

    <HttpPost()> _
    Public Function Register(ByVal model As LaGigaRegisterIndexModel) As ActionResult
        If ModelState.IsValid Then
            ' Attempt to register the user
            Dim createStatus As MembershipCreateStatus = MembershipService.CreateUser(model.RegModel.UserName, model.RegModel.Password, model.RegModel.Email)

            If createStatus = MembershipCreateStatus.Success Then
                If User IsNot Nothing AndAlso Roles.Enabled Then
                    Dim PlayerRoleName As String = ConfigurationManager.AppSettings("PlayerRoleName")
                    If Not Roles.IsUserInRole(PlayerRoleName) AndAlso Roles.RoleExists(PlayerRoleName) Then
                        Roles.AddUserToRole(model.RegModel.UserName, PlayerRoleName)
                    End If
                End If
                FormsService.SignIn(model.RegModel.UserName, False)

                '' create user team for the new user
                Dim newUserTeam As New UserTeam
                newUserTeam.UserId = Membership.GetUser(model.RegModel.UserName).ProviderUserKey
                newUserTeam.Name = model.RegModel.UserName
                newUserTeam.UserGroupID = model.UserGroup.UserGroupID
                Dim db As New LaGigaClassesDataContext
                db.UserTeams.InsertOnSubmit(newUserTeam)
                db.SubmitChanges()

                Return RedirectToAction("Index", "Home")
            Else
                ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus))
            End If
        End If

        ' If we got this far, something failed, redisplay form
        ViewData("PasswordLength") = MembershipService.MinPasswordLength
        Return View(model)
    End Function

    ' **************************************
    ' URL: /Account/ChangePassword
    ' **************************************

    <Authorize()> _
    Public Function ChangePassword() As ActionResult
        ViewData("PasswordLength") = MembershipService.MinPasswordLength
        Return View()
    End Function

    <Authorize()> _
    <HttpPost()> _
    Public Function ChangePassword(ByVal model As ChangePasswordModel) As ActionResult
        If ModelState.IsValid Then
            If MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword) Then
                Return RedirectToAction("ChangePasswordSuccess")
            Else
                ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.")
            End If
        End If

        ' If we got this far, something failed, redisplay form
        ViewData("PasswordLength") = MembershipService.MinPasswordLength
        Return View(model)
    End Function

    ' **************************************
    ' URL: /Account/ChangePasswordSuccess
    ' **************************************

    Public Function ChangePasswordSuccess() As ActionResult
        Return View()
    End Function
End Class
