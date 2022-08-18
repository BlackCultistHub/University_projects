<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
	<title>FlipTable</title>
	<link href="external.css" rel="stylesheet">
	<link href="navigation.css" rel="stylesheet">
	<script src="https://api-maps.yandex.ru/2.1/?lang=ru_RU&amp;apikey=fdc43ad0-8c88-48b8-98f1-b9321ae07ff2" type="text/javascript"></script>
    <script src="placemarks.js" type="text/javascript"></script>
	<script src="positions.js" type="text/javascript"></script>
	<script type="text/javascript">
		ymaps.ready(init);
	</script>
</head>
<?php
function check4all()
{
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
}
check4all();
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
		<!--<div class="Button">
			<div class="textPadding" style="left: 23%;">
				<a href="gameslist.php"><font face="sans-serif" color=black size=5>Games List</font></a>
			</div>
		</div>-->
	</div>
	<div class="barPadding">
		<!--<div class="Button">
			<div class="textPadding">
				<a href="calendar.php"><font face="sans-serif" color=black size=5>Calendar</font></a>
			</div>
		</div>-->
	</div>
	<div class="barPadding">
		<!--<div class="Button">
			<div class="textPadding">
				<a href="about.php"><font face="sans-serif" color=black size=5>About</font></a>
			</div>
		</div>-->
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
<div class="mainBox">
	<div id="map"></div>
</div>
</body>
