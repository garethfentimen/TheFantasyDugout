<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%: Html.DropDownList("PlayerID", DirectCast(ViewData("SecondListData"), IEnumerable(Of SelectListItem)))%>