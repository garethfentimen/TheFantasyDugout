<head>
    <META http-equiv="Content-Type" CONTENT="text/html; charset=iso-8859-1">
    <META NAME="description" CONTENT="Alternative Fantasy Football - The Fantasy Dugout" />
    <META NAME="keywords" CONTENT="The Fantasy Dugout, Alternative Fantasy Football, thefantasydugout, Fantasy Football, Dugout, Football" />

    <link rel="shortcut icon" href="../../Content/favicon.ico"/>

    <title>The Fantasy Dugout</title>
    
    <script src="/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui.min.js" type="text/javascript"></script>
</head>

@section FDLogo
End Section

@section LeftContent
    @Html.Partial("RealWeekFixtures", Model.RealWeekFixtures)
End Section

<iframe seamless="true" allow-top-navigation="false" height="1000px" width="100%" src="http://thefantasydugout.blogspot.co.uk/"></iframe>

<br/>
<div>
    <p><a title ="The Fantasy Dugout Twitter"href="http://twitter.com/#!/fantasydugout" target="_blank">FD twitter account</a> for those that which to comment on the site / Mahalo</p>
    <p>For what is going on dev wise check out <a title ="The Fantasy Dugout Development board" href="https://trello.com/board/current-work/509699e1338f63e04a017426">the fantasy dugout development board</a>.</p>
    <p>All dev requests to <a title ="The Fantasy Dugout Development board" href='https://trello.com/board/the-fd-wish-list/50969ccbeeeaef20790158ff'>the fantasy dugout wish list board</a> sign up to trello to add feature/bug cards.</p>
</div>

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
