<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl(Of LaGiga.LogOnModel)"  %>
<%-- The following line works around an ASP.NET compiler warning --%>
<%: ""%>

<div id="divLogonForm">

    <% If Request.IsAuthenticated Then %>
        <span id="spnLoggedOnText">Hello <b><%: Page.User.Identity.Name %></b>!</span>
        <br />
        <%: Html.ActionLink("Change Password", "ChangePassword", "Account") %>
        <br />
        <%: Html.ActionLink("Log Off", "LogOff", "Account")%>
    <% Else%>
        
        <% Using Html.BeginForm("logon", "account") %>
            <%: Html.ValidationSummary(True, "Login was unsuccessful. Please correct the errors and try again.")%>
            <div class="LogonEditLabel">
                <%: Html.LabelFor(Function(m) m.UserName) %>
            </div>
            <div class="LogonEditField">
                <%: Html.TextBoxFor(Function(m) m.UserName) %>
                <%: Html.ValidationMessageFor(Function(m) m.UserName) %>
            </div>
                
            <div class="LogonEditLabel">
                <%: Html.LabelFor(Function(m) m.Password) %>
            </div>
            <div class="LogonEditField">
                <%: Html.PasswordFor(Function(m) m.Password) %>
                <%: Html.ValidationMessageFor(Function(m) m.Password) %>
            </div>
            <div id="divSignIn">
                <input type="submit" value="Sign In" />
            </div>
                
            <div id="divRememberMeSignIn" class="RightLogonEditField">
                <%: Html.CheckBoxFor(Function(m) m.RememberMe) %>
                <%: Html.LabelFor(Function(m) m.RememberMe) %>
            </div>
            <div id="divRegisterSignIn" class="RightLogonEditField">
                <%: Html.ActionLink("Register", "Register", "account") %>
            </div>
        <% End Using %>
    <% End If%>

</div>