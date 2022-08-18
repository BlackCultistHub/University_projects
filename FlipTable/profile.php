<head>
	<meta charset="utf-8">
	<title>FlipTable - Profile</title>
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
//----
if($login != false)
{
	$profileInfo = file_get_contents("profiles\\".$login.".txt");
	$out = array();
	preg_match_all("|<[^>]+>(.*)</[^>]+>|U", $profileInfo, $out, PREG_PATTERN_ORDER);
}
//----
$dir = opendir('events/'.$login);
$count = 0;
while($file = readdir($dir)){
    if($file == '.' || $file == '..' || is_dir('events/'.$login . $file)){
        continue;
    }
    $count++; //amount of created events
}
//----
if (file_exists("admins\\".$login.".txt"))
{
	if (file_get_contents("admins\\".$login.".txt") == true)
		$admin = true;
	else
		$admin = false;
}
else
		$admin = false;
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
<div class="mainBox">
	<div style="position: realtive; 
	float:left; 
	margin-top: 15; 
	margin-left: 10;
	width: 23%;
	height: 100%;">
		<!-- Profile Nick + Pic -->
		<div style="position: relative;
		margin-top: 5;
		left: 2.5%;
		width: 95%;
		height: 30%;">
			<!-- Nick -->
			<div style="position: relative; text-align: center;">
				<?php echo '<font size=7>'.$login.'</font>'?>
			</div>
			<!-- Pic -->
			<div style="position: relative; text-align: center;">
				<img src="pics\profile.png" width=100 height=100>
			</div>
			<!-- Buttons -->
			<!-- <div style="position: relative; top: 5;">
				<form action="" method="POST" align="center">
				<input type="submit" value="Change avatar";></form>
				<form action="" method="POST" align="center">
				<input type="submit" value="Delete avatar";></form>
			</div> -->
		</div>
		<!-- Biography -->
		<div style="position: relative;
		margin-top: 5;
		left: 2.5%;
		width: 95%;
		height: 30%;">
			<!-- Header -->
			<div style="position: relative; text-align: left;">
				<font size=5>Biography:</font>
			</div>
			<!-- Block -->
			<div style="position: relative; left: 5%; width: 90%; height: 85%; border: 1px solid black;">
				<?php
					echo '<font size=4 face="sans-serif" color=black>'.$out[1][0].'</font>';
				?>
			</div>
		</div>
		<!-- "Plays" -->
		<div style="position: relative;
		margin-top: 5;
		left: 2.5%;
		width: 95%;
		height: 30%;">
			<!-- Header -->
			<div style="position: relative; text-align: left;">
				<font size=5>Plays:</font>
			</div>
			<!-- Block -->
			<div style="position: relative; left: 5%; width: 90%; height: 85%; border: 1px solid black;">
				<?php
					echo '<font size=4 face="sans-serif" color=black>'.$out[1][1].'</font>';
				?>
			</div>
		</div>
	</div>
	<div style="position: realtive; 
	float:left; 
	margin-top: 15; 
	margin-left: 10;
	width: 23%;
	height: 100%;">
		<!-- Member of -->
		<div style="position: relative;
		margin-top: 5;
		left: 2.5%;
		width: 95%;
		height: 30%;">
			<!-- Header -->
			<div style="position: relative; text-align: left;">
				<font size=5>Member of:</font>
			</div>
			<!-- Block -->
			<div style="position: relative; left: 5%; width: 90%; height: 85%; border: 1px solid black;">
				
			</div>
		</div>
		<!-- Creator of -->
		<div style="position: relative;
		margin-top: 5;
		left: 2.5%;
		width: 95%;
		height: 30%;">
			<!-- Header -->
			<div style="position: relative; text-align: left;">
				<font size=5>Creator of:</font>
			</div>
			<!-- Block -->
			<div style="overflow: hidden; position: relative; left: 5%; width: 90%; height: 85%; border: 1px solid black;">
				<?php
					if ($handle = opendir('events/'.$login)) 
					{
						//echo "Дескриптор каталога: $handle\n";
						//echo "Файлы:\n";
						while (false !== ($file = readdir($handle))) 
						{ 
							if ($file != '.' && $file != '..' && $file != '.txt')
							{
								preg_match_all("|<[^>]+>(.*)</[^>]+>|U", file_get_contents("events\\".$login."\\".$file), $outFiles, PREG_PATTERN_ORDER);
								echo '---[> <a href=eventOverView.php?event='.$file.'><font face="sans-serif" color=black size=4>'.$outFiles[1][0].' </font><font face="sans-serif" color=black size=3>('.substr($file, 0, 10).')</font></a><br>';
							}
						}
						closedir($handle); 
					}
				?>
			</div>
		</div>
		<!-- Links -->
		<div style="position: relative;
		margin-top: 5;
		left: 2.5%;
		width: 95%;
		height: 30%;">
			<!-- Header -->
			<div style="position: relative; text-align: left;">
				<font size=5>Links:</font>
			</div>
			<!-- Block -->
			<div style="position: relative; left: 5%; width: 90%; height: 85%; border: 1px solid black;">
				<?php
					echo '<font size=4 face="sans-serif" color=black>'.$out[1][2].'</font>';
				?>
			</div>
		</div>
	</div>
	<div style="position: realtive; 
	float:left; 
	margin-top: 15; 
	margin-left: 10;
	width: 23%;
	height: 100%;">
	</div>
	<div style="position: realtive; 
	float:left; 
	margin-top: 15; 
	margin-left: 10;
	width: 25%;
	height: 100%;">
		<!-- Microblog -->
		<div style="position: relative;
		margin-top: 5;
		left: 2.5%;
		width: 95%;
		height: 92%;">
			<!-- Buttons -->
			<form action="login.php?stat=100" method="POST" align="right">
			<input type="submit" value="Exit account">
			</form>
			<form action="profileSettings.php" method="POST" align="right">
			<input type="submit" value="Profile Settings">
			</form>
			<!-- Header -->
			<div style="position: relative; text-align: left;">
				<font size=5>Microblog:</font>
			</div>
			<!-- Block -->
			<div style="position: relative; left: 5%; width: 90%; height: 85%; border: 1px solid black;">
				
			</div>
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
</body>