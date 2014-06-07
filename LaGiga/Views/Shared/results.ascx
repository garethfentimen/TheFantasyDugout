<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl(Of LaGiga.ErrorIndexModel)" %>
<%@ import Namespace="LaGiga" %>

<div id="divUserFeedback">
    <% If Model.ErrorType = ErrorType.IsError Then%>
        <img src="../../Content/icon-error.png" alt="error!" />
    <% elseif Model.ErrorType = ErrorType.Warning then %>
        <img src="../../Content/Warning_Icon_96x96.png" alt="error!" />
    <% ElseIf Model.ErrorType = ErrorType.Success Then%>
        <img id="imgSuccess" src="../../Content/tick.png" alt="success!" />
    <% End If %>
    <% If Not String.IsNullOrEmpty(Model.Message) Then%>
        <p><%: Model.Message %></p>
    <% end if%>
</div>