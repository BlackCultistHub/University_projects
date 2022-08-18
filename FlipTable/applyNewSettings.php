<?php
	$content = file_get_contents("profiles\\".@$_GET['nickName'].".txt");
	$file = @fopen("profiles\\".@$_GET['nickName'].".txt", "w+");
	flock($file, LOCK_EX);
	if (@$_POST['biography'] != '')
		$content = preg_replace("/<bio>(.*)<\/bio>/", "<bio>".$_POST['biography']."</bio>", $content);
	if (@$_POST['playsGames'] != '')
		$content = preg_replace("/<games>(.*)<\/games>/", "<games>".$_POST['playsGames']."</games>", $content);
	if (@$_POST['links'] != '')
		$content = preg_replace("/<links>(.*)<\/links>/", "<links>".$_POST['links']."</links>", $content);
	fwrite($file, $content);
	flock($file, LOCK_UN);
	fclose($file);
	header("Location: profile.php");
?>