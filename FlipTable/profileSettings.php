<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
	<title>FlipTable - Change Profile Info</title>
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
		
	</div>
	<div id="block-3">
		<?php echo '<form action="applyNewSettings.php?nickName='.$login.'" method="POST">'; ?>
			<p><font color=black face="sans-serif" size=5>Your biography:</font></p>
			<textarea name="biography" placeholder="Tell everybody about yourself..." cols=35 rows=5></textarea><br>
			<p><font color=black face="sans-serif" size=5>Which games do you play?</font><Br></p>
			<textarea name="playsGames" placeholder="Do you like something special?..." cols=35 rows=5></textarea><br>
			<p><font color=black face="sans-serif" size=5>Links:</font></p>
			<textarea name="links" placeholder="Maybe ou have FB/Telegram/etc?..." cols=35 rows=7></textarea><br>
			<p><input type="submit" value="Change"></p>
		</form>
	</div>
	<div id="block-3">
		
	</div>
</div>
</body>