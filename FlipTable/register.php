<html>
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
<head>
	<meta charset="utf-8">
	<title>Register</title>
	<link href="external.css" rel="stylesheet">
	<link href="navigation.css" rel="stylesheet">
</head>
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
<br>
<br>
<br>
<br>
<br>
<table align=center>
	<tr>
		<td align=center><p><Big><B><font color="#000" face="arial">Register</font><B></Big><td>
	</tr>
	<tr><td><br></td></tr>
	<tr>
		<td>
		<?php
		if(@$_GET["error"] == 183)
			echo '<span style="color:#FF0000;text-align:center;">Username is already taken.</span>';
		if(@$_GET["error"] == 100)
			echo '<span style="color:#FF0000;text-align:center;">Username cannot be empty.</span>';
		if(@$_GET["error"] == 101)
			echo '<span style="color:#FF0000;text-align:center;">Password cannot be empty.</span>';
		if(@$_GET["error"] == 95)
			echo '<span style="color:#FF0000;text-align:center;">Passwords don\'t match.</span>';
		if(@$_GET["error"] == 99)
			echo '<span style="color:#FF0000;text-align:center;">Please use A-Z; a-z; 0-9 symbols for username.</span>';
		?>
		</td>
	</tr>
	<tr>
		<td><form action="registerAction.php" method="post">
		 <p><Big><B><font color="#000" face="arial">Login: <input type="text" name="nick" placeholder="Your nickname..." required></p></font><B></Big></a>
		 <p><Big><B><font color="#000" face="arial">Password: <input type="password" name="passw" placeholder="Password..." required></p></font><B></Big></a>
		 <p><Big><B><font color="#000" face="arial">Confirm Password: <input type="password" name="confPassw" placeholder="Confirm password..." required></p></font><B></Big></a>
		 <p><input type="submit" value="Sign Up"/></p>
		</form></td>
	</tr>
	<tr>
		<td><p><Big><B><font color="#000" face="arial">Already have an account?</font><B></Big></a></p>
		<form action="login.php" method="POST">
		<input type="submit" value="Sign In" />
		</form></td>	
	</tr>
</table>
</form>
</body>
</html>