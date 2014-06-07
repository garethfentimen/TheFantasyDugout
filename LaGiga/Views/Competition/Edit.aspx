﻿<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.Competition)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="server">

    <script type="text/javascript" >

        $(document).ready(function () {
            $("#FromDate").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#ToDate").datepicker({ dateFormat: 'dd/mm/yy' });
        });

    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <%-- The following line works around an ASP.NET compiler warning --%>
    <%: ""%>

    <% Using Html.BeginForm() %>
        <%: Html.ValidationSummary(True) %>
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.CompetitionID) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.CompetitionID) %>
                <%: Html.ValidationMessageFor(Function(model) model.CompetitionID) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.Name) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.Name) %>
                <%: Html.ValidationMessageFor(Function(model) model.Name) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.CurrentCompetition) %>
            </div>
            <div class="editor-field">
                <%: Html.CheckBox("CurrentCompetition", isChecked:=model.CurrentCompetition.HasValue andalso model.CurrentCompetition.Value.Equals(True) )%>
                <%: Html.ValidationMessageFor(Function(model) model.CurrentCompetition) %>
            </div>

            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.FromDate) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.FromDate) %>
                <%: Html.ValidationMessageFor(Function(model) model.FromDate)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.ToDate) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.ToDate) %>
                <%: Html.ValidationMessageFor(Function(model) model.ToDate)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.SquadSize) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.SquadSize) %>
                <%: Html.ValidationMessageFor(Function(model) model.SquadSize)%>
            </div>

            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% End Using %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeaderMainContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

