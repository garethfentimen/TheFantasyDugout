<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.PlayerTransferIndexModel)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="server">

<script type="text/javascript" >
    $(document).ready(function () {

        $("#TeamDropDown").change(function () {

            $("#newTeam").val($("#TeamDropDown").val());
            $("#FromDate").val("");

            var buttons = {
                'Yes with details I have just put in': function () {
                    $("form#TransferForm").trigger("submit");
                    $(this).dialog("close");
                },
                'No': function () {
                    $(this).dialog("close");
                }
            };

            $("#TransferInfoDialog").dialog({
                minHeight: 350,
                width: 480,
                bgiframe: true,
                autoOpen: true,
                draggable: true,
                modal: true,
                closeOnEscape: true,
                resizable: false,
                title: 'Create Transfer?',
                buttons: buttons
            });

            overlaycheck();
        });

        function overlaycheck() {
            $(".ui-widget-overlay").click(function () {
                $("#TransferInfoDialog").dialog("close");
            });
        }

        $("#FromDate").datepicker({ dateFormat: 'dd/mm/yy' });

    });

</script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <%If Model.Succeeded.HasValue AndAlso Model.Succeeded.value.Equals(True) Then%>
        <div id = "TransferResults">
            <p> <%= Model.SuccessMessage %></p>
        </div>
    <% End If%>

    <div id="EditPlayer">

        <% Using (Ajax.BeginForm("Save", "Player" , New AjaxOptions With {.UpdateTargetId = "results"},
               new with { .id ="SavePlayerForm" } ))%>
            <%: Html.ValidationSummary(True) %>
            <fieldset>
                <legend>Fields</legend>

                <div class="editor-field">
                    <%: Html.HiddenFor(Function(model) model.Player.PlayerID)%>
                    <%: Html.ValidationMessageFor(Function(model) model.Player.PlayerID)%>
                </div>
            
                <div class="editor-label">
                    <%: Html.LabelFor(Function(model) model.Player.FirstName)%>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(Function(model) model.Player.FirstName)%>
                    <%: Html.ValidationMessageFor(Function(model) model.Player.FirstName)%>
                </div>
            
                <div class="editor-label">
                    <%: Html.LabelFor(Function(model) model.Player.Surname)%>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(Function(model) model.Player.Surname)%>
                    <%: Html.ValidationMessageFor(Function(model) model.Player.Surname)%>
                </div>
            
                <div class="editor-label">
                    <%: Html.Label("Position") %>
                </div>
                <div class="editor-field">
                    <%: Html.DropDownListFor(Function(model) model.Player.PositionID, Model.Player.PositionValues)%>
                    <%: Html.ValidationMessageFor(Function(model) model.Player.PositionID)%>
                </div>
            
                <div class="editor-label">
                    <%: Html.Label("Club Team")%>
                </div>
                <div class="editor-field">
                    <%: Html.DropDownListFor(Function(model) model.Player.TeamID, Model.Player.ClubTeamValues, New With {.id = "TeamDropDown"})%>
                    <%: Html.ValidationMessageFor(Function(model) model.Player.TeamID)%>
                </div>
            
                <div class="editor-label">
                    <%: Html.Label("National Team")%>
                </div>
                <div class="editor-field">
                    <%: Html.DropDownListFor(Function(model) model.Player.NationalTeamID, Model.Player.NationalTeamValues)%>
                    <%: Html.ValidationMessageFor(Function(model) model.Player.NationalTeamID)%>
                </div>
            
                <p>
                    <input id="submit" type="submit" value="Save" />
                </p>

                <div id = "results">
                    &nbsp;
                </div>

            </fieldset>

        <% End Using %>

    </div>

    <div>
        <%: Html.ActionLink("Back to List", "PlayerIndex")%>
    </div>

    <div id="TransferInfoDialog">
    
        <%  Using (Ajax.BeginForm("CreateTransfer", "Player",
                 New AjaxOptions With {.UpdateTargetId = "TransferResults"}, New With {.id = "TransferForm"}))%>
        <%: Html.ValidationSummary(True) %>
        <fieldset>
            <legend>Fields</legend>
            
            <%: Html.Hidden("PlayerID", Model.Player.playerID) %>
            <%: Html.Hidden("TeamID", model.Player.TeamID) %>

            <%: Html.Hidden("NewTeamID", model.Player.TeamID, New With { .id = "newTeam" } ) %>
            
            <div class="editor-label">
                <%: Html.Label("Transfer From Date (default now if left empty)") %>
            </div>
            <div class="editor-field">
                <%: Html.TextBox("FromDate", Nothing)%>
                <%: Html.ValidationMessageFor(Function(model) model.Transfer.FromDate) %>
            </div>
            
            <div class="editor-label">
                <%: Html.Label("Transfer Fee")%>
            </div>
            <div class="editor-field">
                <%: Html.TextBox("TransferFee", 0)%>
                <%: Html.ValidationMessageFor(Function(model) model.Transfer.TransferFee) %>
            </div>
        </fieldset>

    <% End Using %>
    
    </div>

</asp:Content>

