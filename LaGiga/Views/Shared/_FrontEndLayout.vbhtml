<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <META NAME="robots" CONTENT="NOODP">
    <META http-equiv="Content-Type" CONTENT="text/html; charset=iso-8859-1">
    <META NAME="description" CONTENT="Alternative Fantasy Football - The Fantasy Dugout" />
    <META NAME="keywords" CONTENT="The Fantasy Dugout, football player stats, Alternative Fantasy Football, thefantasydugout, Fantasy Football, Dugout, Football" />

	<title>@ViewBag.Title</title>
	@*<link href="@Url.Content("~/stylesheets/Site.css")" rel="stylesheet" type="text/css" />*@
	<link href="@Url.Content("~/stylesheets/FrontEndMaster.css")" rel="stylesheet" type="text/css" />

    <link type="text/css" href="/stylesheets/app.css" rel="stylesheet" />
    <link type="text/css" href="/stylesheets/common.css" rel="stylesheet" />
    <link type="text/css" href="/Scripts/jquerytheme/flick/jquery-ui-flick.css" rel="stylesheet" />
	
    <script src="/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui-1.9.1.flick.min.js" type="text/javascript"></script>
	<script src="/Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="/Scripts/spin-1-2-7.min.js" type="text/javascript"></script>
	<script src="@Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js")" type="text/javascript"></script>

	<meta name="robots" content="NOODP">
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
	<meta name="description" content="Alternative Fantasy Football - The Fantasy Dugout" />
	<meta name="keywords" content="the fantasy dugout, The Fantasy Dugout, Alternative Fantasy Football, thefantasydugout, Fantasy Football, Dugout, Football" />
	<meta name="LatestPublicationDate" content="2012/09/30 20:13:41" />

	<link rel="shortcut icon" href="../../Content/favicon.ico"/>
</head>

<body>
    <div class="header">
        <div id="logindisplay">
		    @Html.Partial("LogOnUserControl", Model.LaGigaLogon)
	    </div>

        <div id="menucontainer">
            <a class="radius small button" title="Home" href="@Url.Action("Index", "Home")"><img src="/Content/home-on.png" alt="Home"/></a>
            <a class="radius small button" title="Your team" href="@Url.Action("PlayerHomeIndex", "PlayerHome")"><img src="/Content/football-icon.png" alt="Home"/></a>
            <a class="radius small button" title="The list" href="@Url.Action("GetTheList", "TheList")"><img src="/Content/thelist.png" alt="Home"/></a>
	    </div>

        @RenderSection("FDLogo")
    </div>

    <div class="leftContent">
        @RenderSection("LeftContent")
    </div>

    <div class="content">
        @RenderBody()
    </div>
    
    <div id="footer"></div>
</body>

<script type="text/javascript">

	var _gaq = _gaq || [];
	_gaq.push(['_setAccount', 'UA-25114624-1']);
	_gaq.push(['_trackPageview']);

	(function () {
		var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
		ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
		var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
	})();

</script>

</html>
