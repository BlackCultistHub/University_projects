<?php
$file = @ fopen("database\\".htmlspecialchars($_POST['nick']).".txt", "r");
if ($file != false)
{
	$hashToCheck = file_get_contents("database\\".htmlspecialchars($_POST['nick']).".txt");
	if (password_verify(htmlspecialchars($_POST['passw']), $hashToCheck))
	{
		$hash = md5(htmlspecialchars($_POST['nick']));
		setcookie("Token", $hash, time()+259200);
		$file = fopen("tokenz\\".$hash.".txt", "w+");
		flock($file, LOCK_EX);
		fwrite($file, htmlspecialchars($_POST['nick']));
		flock($file, LOCK_UN);
		fclose($file);
		header("Location: index.php?stat=11");
	}
	if(!password_verify(htmlspecialchars($_POST['passw']), $hashToCheck))
	{
		header("Location: login.php?error=1");
	}
}
if ($file == false)
{
	header("Location: login.php?error=404");
}
?>