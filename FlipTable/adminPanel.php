<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
	<title>Admin Panel</title>
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
	width: 50%;
	height: 100%;">
		<div style="position: realtive;
		margin-top: 10; 
		margin-left: 15%;
		width: 95%;
		height: 100%;">
			<?php 
				$catalogs = scandir('events/');
				//print_r($catalogs);
				$howManyCatalog = count($catalogs);
				for ($i = 2; $i < $howManyCatalog; $i++)
				{
					$files = scandir('events/'.$catalogs[$i]);
					$howManyFiles = count($files);
					echo '<font color=black face="sans-serif" size=5>'.$catalogs[$i].': </font><br>';
					$fcount = 0;
					for ($j = 2; $j < $howManyFiles; $j++)
					{
						echo '<p>----[> <a href=eventOverView.php?event='.$files[$j].'&usersNick='.$catalogs[$i].'><font face="sans-serif" color=black size=4>'.substr($files[$j], 0, 10).'</font></a></p>';
						$fcount++;
					}
					if ($fcount == 0)
						echo '<p><font face="sans-serif" color=black size=3>No events created by this user.</font></p>';
					echo '<br>';
				}
			?>
		</div>
	</div>
	<div style="position: realtive; 
	float:left; 
	margin-top: 15; 
	margin-left: 10;
	width: 44%;
	height: 100%;
	text-align: right;">
		<?php echo '<form action="offerAdmin.php?adminName='.$login.'" method="POST">'; ?>
		<p><font color=black face="sans-serif" size=5>Offer admin permissions:</font></p>
		<input type="text" name="offer" placeholder="Nickname..." required>
		<?php if (!empty($_GET)){ if (@$_GET['stat'] == 115){
			echo '<p><span style="color:#008000;text-align:center;">Your request has been sent.</span></p>'; }}?>
		<p><input type="submit" value="Send request"></p>
		</form>
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