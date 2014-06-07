<%@ Page Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.LaGigaRegisterIndexModel)" %>

<asp:Content ID="registerTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Register
</asp:Content>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Create a New Account</h2>
    <p>
        Use the form below to create a new account. 
    </p>
    <p>
        Passwords are required to be a minimum of <%: ViewData("PasswordLength") %> characters in length.
    </p>

    <% Using Html.BeginForm() %>
        <%: Html.ValidationSummary(True, "Account creation was unsuccessful. Please correct the errors and try again.")%>
        <div>
            <fieldset>
                <legend>Account Information</legend>
                
                <div class="editor-label">
                    <%: Html.LabelFor(Function(m) m.RegModel.UserName)%>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(Function(m) m.RegModel.UserName)%>
                    <%: Html.ValidationMessageFor(Function(m) m.RegModel.UserName)%>
                </div>
                
                <div class="editor-label">
                    <%: Html.LabelFor(Function(m) m.RegModel.Email)%>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(Function(m) m.RegModel.Email)%>
                    <%: Html.ValidationMessageFor(Function(m) m.RegModel.Email)%>
                </div>
                
                <div class="editor-label">
                    <%: Html.LabelFor(Function(m) m.RegModel.Password)%>
                </div>
                <div class="editor-field">
                    <%: Html.PasswordFor(Function(m) m.RegModel.Password)%>
                    <%: Html.ValidationMessageFor(Function(m) m.RegModel.Password)%>
                </div>
                
                <div class="editor-label">
                    <%: Html.LabelFor(Function(m) m.RegModel.ConfirmPassword)%>
                </div>
                <div class="editor-field">
                    <%: Html.PasswordFor(Function(m) m.RegModel.ConfirmPassword)%>
                    <%: Html.ValidationMessageFor(Function(m) m.RegModel.ConfirmPassword) %>
                </div>

                <div class="editor-label">
                    <%: Html.Label("User Group")%>
                </div>
                <div class="editor-field">
                <%: Html.DropDownListFor(Function(model) model.UserGroup.UserGroupID, Model.UserGroup.UserGroupTypes)%>
                <%: Html.ValidationMessageFor(Function(model) model.UserGroup.UserGroupID) %>
            </div>
                
                <p>
                    <input type="submit" value="Register" />
                </p>
            </fieldset>
        </div>
    <% End Using %>
</asp:Content>
