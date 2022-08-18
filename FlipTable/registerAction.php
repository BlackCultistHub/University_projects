<?php
$ok = 1;
if(file_exists("database\\".htmlspecialchars($_POST['nick']).".txt"))
{
	$ok = 0;
	header("Location: register.php?error=183");
}
if(htmlspecialchars($_POST['nick']) == "")
{
	$ok = 0;
	header("Location: register.php?error=100");
}
if(htmlspecialchars($_POST['passw']) == "")
{
	$ok = 0;
	header("Location: register.php?error=101");
}
if(htmlspecialchars($_POST['passw']) != htmlspecialchars($_POST['confPassw']))
{
	$ok = 0;
	header("Location: register.php?error=95");
}
$excepions = array("/","\\","?","!",":","*","\"","\'","`","#","%","&","{","}","(",")","[","]",":",";","<",">","|","№","$","^","-","+","=",".",",");
$arrayLen = count($excepions);
for($i=0; $i < $arrayLen; $i++)
{
	$pos = strpos(htmlspecialchars($_POST['nick']), $excepions[$i]);
	if($pos !== false)
		$ok = 0;
}
if (!$ok)
	header("Location: register.php?error=99");
if($ok)
{
	$file = @fopen("database\\".htmlspecialchars($_POST['nick']).".txt", "a+");
	flock($file, LOCK_EX);
	fwrite($file, password_hash(htmlspecialchars($_POST['passw']), PASSWORD_DEFAULT) );
	flock($file, LOCK_UN);
	fclose($file);
	$file = @fopen("profiles\\".htmlspecialchars($_POST['nick']).".txt", "a+");
	flock($file, LOCK_EX);
	fwrite($file, "<bio></bio>".PHP_EOL."<games></games>".PHP_EOL."<links></links>" );
	flock($file, LOCK_UN);
	fclose($file);
	$hash = md5(htmlspecialchars($_POST['nick']));
	setcookie("Token", $hash, time()+259200);
	$file = @fopen("tokenz\\".$hash.".txt", "w+");
	flock($file, LOCK_EX);
	fwrite($file, htmlspecialchars($_POST['nick']));
	flock($file, LOCK_UN);
	fclose($file);
}
?>
<head>
<table align=center>
  <tr>
    <td><h1>Welcome!</h1></td>
  </tr>
</table>
</head>
<table align=center>
  <tr>
    <td><b>Hello</b>, <?php echo htmlspecialchars($_POST['nick']); ?>.</td>
  </tr>
  <tr>
    <td><b>Your password is</b>: <?php echo htmlspecialchars($_POST['passw']); ?>.</td>
  </tr>
  <tr>
    <td><p>You'll be redirected in 10 sec.</p></td>
  </tr>
  <tr>
	<td><br></td>
  </tr>
  <tr>
    <td><form action="index.php?stat=11" method="POST">
    <input type="submit" value="Get me back now!" />
	</form></td>
  </tr>
</table>
<META HTTP-EQUIV="REFRESH" CONTENT="10; ..\index.php">