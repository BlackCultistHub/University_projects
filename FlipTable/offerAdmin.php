<?php
	if (!file_exists("admins\\".$_POST['offer']))
	{
		$file = @fopen("admins\\".$_POST['offer'].".txt", "w+");
		flock($file, LOCK_EX);
		fwrite($file, "false".PHP_EOL."<DELETE_IF_CONFIRMED>Offered: ".$_GET['adminName']."</DELETE_IF_CONFIRMED>");
		flock($file, LOCK_UN);
		fclose($file);
	}
	header("Location: adminPanel.php?stat=115");
?>