<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
	<title>FlipTable - Event Overview</title>
	<link href="external.css" rel="stylesheet">
	<link href="navigation.css" rel="stylesheet">
</head>
<?php
if(isset($_COOKIE['Token']))
{
	$file = @ fopen("tokenz\\".$_COOKIE['Token'].".txt", "r");
	if($file)
		$login = file_get_contents("tokenz\\".$_COOKIE['Token'].".txt");
	if(!$file)
		$login = false;
}
else
	$login = false;
if (file_exists("admins\\".$login.".txt"))
{
	if (file_get_contents("admins\\".$login.".txt") == true)
		$admin = true;
	else
		$admin = false;
}
else
	$admin = false;
if ($admin == true && @$_GET['usersNick'] != '')
{
	$profileInfo = file_get_contents("events\\".$_GET['usersNick']."\\".$_GET['event']);
	$out = array();
	preg_match_all("|<[^>]+>(.*)</[^>]+>|U", $profileInfo, $out, PREG_PATTERN_ORDER);
}
else if($login != false)
{
	$profileInfo = file_get_contents("events\\".$login."\\".$_GET['event']);
	$out = array();
	preg_match_all("|<[^>]+>(.*)</[^>]+>|U", $profileInfo, $out, PREG_PATTERN_ORDER);
}
?>
<body>
<input type="checkbox" id="nav-toggle" hidden>
<div class="selector">
	<div class="barPadding">
		<div class="Button" style="background-color: #000">
			<div class="textPadding">
				<a href="index.php"><font face="sans-serif" color=white size=5>FlipTable</font></a>
			</div>
		</div>
	</div>
	<div class="barPadding">
		<div class="Button">
			<div class="textPadding" style="left: 23%;">
				<a href="gameslist.php"><font face="sans-serif" color=black size=5>Games List</font></a>
			</div>
		</div>
	</div>
	<div class="barPadding">
		<div class="Button">
			<div class="textPadding">
				<a href="calendar.php"><font face="sans-serif" color=black size=5>Calendar</font></a>
			</div>
		</div>
	</div>
	<div class="barPadding">
		<div class="Button">
			<div class="textPadding">
				<a href="about.php"><font face="sans-serif" color=black size=5>About</font></a>
			</div>
		</div>
	</div>
	<div class="barPadding">
		
	</div>
	<div class="barPadding">
		
	</div>
	<div class="barPaddingSmall">
		<div class="iconPadding">
			<?php
				if ($login != false)
					echo '<a href="profile.php"><img src="pics\profile.png" width=30 height=30></a>';
				else if ($login == false)
					echo '<a href="login.php"><img src="pics\login.jpg" width=30 height=30></a>';
			?>
		</div>
	</div>
	<div class="barPaddingSmall">
		<div class="iconPadding">
			<label for="nav-toggle" class="nav-toggle" onclick><img src="pics\menu.png" width=40 height=30></label>
		</div>
	</div>
</div>

<div class="nav">
	<h2 class="logo"> 
		<div style="background-color: #fff">
			<font color=black>Menu</font>
		</div>
	</h2>
	<ul>
		<div style="background-color: #fff">
		<li>Nothing here.
		</div>
	</ul>
</div>
<div class="mainBox">
	<div id="block-3">
		<div id="eventBlockTitle" style="text-align: center;">
			<?php echo '<font face="sans-serif" color=black size=7>'.$out[1][0].'</font>'; ?>
		</div>
	</div>
	<div id="block-3">
		<div id="eventBlockTitle" style="text-align: center;">
			
		</div>
		<div id="eventBlockTitle">
			<font face="sans-serif" color=black size=5>Games: </font>
			<?php 
				if ($out[1][1] == 'true')
				{
					echo '<font face="sans-serif" color=black size=4>Tabletop Games </font>'; 
				}
				if ($out[1][2] == 'true')
				{
					echo '<font face="sans-serif" color=black size=4>Role-Play Games </font>'; 
				}
				if ($out[1][3] == 'true')
				{
					echo '<font face="sans-serif" color=black size=4>CCG Games</font>'; 
				}
			?>
		</div>
		<div id="eventBlockTitle">
			<font face="sans-serif" color=black size=5>Date: </font>
			<?php echo '<font face="sans-serif" color=black size=4>'.substr($_GET['event'], 0, 10).'</font>'; ?>
		</div>
		<div id="eventBlockTitle">
			<font face="sans-serif" color=black size=5>Place: </font>
			<?php echo '<font face="sans-serif" color=black size=4>'.$out[1][4].'</font>'; ?>
		</div>
		<div id="eventBlock">
			<font face="sans-serif" color=black size=5>Description: </font>
			<?php echo '<font face="sans-serif" color=black size=4>'.$out[1][5].'</font>'; ?>
		</div>
	</div>
	<div id="block-3">
		<?php
			if ($admin = true && @$_GET['usersNick'] != '')
			{
				echo '<form action="eventCreate.php?stat=555&event='.$_GET['event'].'&nickName='.$_GET['usersNick'].'" method="POST" align="right">
					<input type="submit" value="Delete Event"></form>';
			}
			else if ($login != false && $login == $out[1][6] )
				echo '<form action="eventCreate.php?stat=555&event='.$_GET['event'].'&nickName='.$login.'" method="POST" align="right">
					<input type="submit" value="Delete Event"></form>';
		?>
	</div>
</div>
<div class="nav">
	<h2 class="logo"> 
		<div style="background-color: #fff">
			<font color=black>Menu</font>
		</div>
	</h2>
	<ul>
		<div style="background-color: #fff">
		<li>
			<?php 
				if ($login != false)
					echo '<a href="newEvent.php"><font color=black face="sans-serif" size=5>Create Event</font></a>';
				else if ($login == false)
					echo '<font color=black face="sans-serif" size=4>Please Sign in to create event</font>';
			?>
		<li>----------------
		<?php
			if ($admin == true)
				echo '<li><a href="adminPanel.php"><font color=black face="sans-serif" size=4>Admin Panel</font></a>';
		?>
		<li><font face="sans-serif">--+--</font>
		<li><font face="sans-serif">--+--</font>
		</div>
	</ul>
</div>
</body>