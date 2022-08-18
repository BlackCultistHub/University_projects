<html>
<?php
if(@$_GET["stat"] == 100)
{
	@unlink("tokenz\\".$_COOKIE['Token'].".txt");
	@setcookie("Token", "", time()-10800);
	header("Location: index.php");
}
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
	<title>Login</title>
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
		<td align=center><p><Big><B><font color="#000" face="arial">LogIn</font><B></Big><td>
	</tr>
	<tr><td><br></td></tr>
	<tr>
		<td><?php
		if(@$_GET["error"] == 404)
			echo '<span style="color:#FF0000;text-align:center;">User with such name not found.</span>';
		if(@$_GET["error"] == 1)
			echo '<span style="color:#FF0000;text-align:center;">Incorrect password</span>';
		?></td>
	</tr>
	<tr>
		<td><form action="loginAction.php" method="post">
		 <p><Big><B><font color="#000" face="arial">Login: <input type="text" name="nick" placeholder="Type login here..." autocomplete="on" required></font><B></Big></a></p>
		 <p><Big><font color="#000" face="arial">Password: <input type="password" name="passw" placeholder="Type password here..." required></font></Big></a></p>
		 <p><input type="submit" value="Sign In"/></p>
		</form></td>
	</tr>
	<tr>
		<td><p><Big><B><font color="#000" face="arial">Don't Have an account?</font></B></Big></a>
		<form action="register.php" method="POST">
		<input type="submit" value="Sign Up" />
		</form></td>	
	</tr>
</table>
</body>
</html>