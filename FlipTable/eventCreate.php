<?php
	
	if (@$_GET['stat'] == 555)
	{
		unlink("events\\".$_GET['nickName']."\\".$_GET['event']);
		//echo "<pre>";
		$coordCatalog = file_get_contents("positions.js");
		$file = @fopen("positions.js", "w");
		flock($file, LOCK_EX);
		//echo $_GET["nickName"].'__'.substr($_GET["event"], 0, 10).'__'.$_GET["event"];
		//echo "<br><br>";
		//echo $coordCatalog;
/*POINT -> ";"*/ //$coordCatalog = preg_replace('/\/\*\['.$_GET["nickName"].'_'.substr($_GET["event"], 0, 10).'\]\*\/(.*)[;]/', 'dick', $coordCatalog);
		$coordCatalog = preg_replace('/\/\*\['.$_GET["nickName"].'_'.substr($_GET["event"], 0, 10).'\]\*\/(.*)\/\*\['.$_GET["nickName"].'_'.substr($_GET["event"], 0, 10).'\]\*\//', '', $coordCatalog);
		//echo "<br><br>";
		//echo $coordCatalog;
		fwrite($file, $coordCatalog);
		flock($file, LOCK_UN);
		fclose($file);
		//echo "</pre>";
		header("Location: index.php");
	}
	else
	{
		if (file_exists("events\\".htmlspecialchars($_GET['nickName'])."\\"))
			$file = @fopen("events\\".$_GET['nickName']."\\".$_POST['date'].".txt", "a+");
		else
		{
			mkdir("events/".htmlspecialchars($_GET['nickName']), 0777, true);
			$file = @fopen("events\\".$_GET['nickName']."\\".$_POST['date'].".txt", "a+");
		}
		flock($file, LOCK_EX);
		if (empty($_POST['tabletop']) && empty($_POST['rp']) && empty($_POST['ccg']))
		{
			fwrite($file, "<title>".$_POST['title']."</titile>
			<game-tabletop>false</game-tabletop>
			<game-rp>false</game-rp>
			<game-ccg>false</game-ccg>
			<place>".$_POST['place']."</place>
			<description>".$_POST['description']."</description>");
			$gameTypes = "Else";
		}
		else if (empty($_POST['tabletop']) && empty($_POST['rp']))
		{
			fwrite($file, "<title>".$_POST['title']."</titile>
			<game-tabletop>false</game-tabletop>
			<game-rp>false</game-rp>
			<game-ccg>".$_POST['ccg']."</game-ccg>
			<place>".$_POST['place']."</place>
			<description>".$_POST['description']."</description>");
			$gameTypes = "Collectible card games";
		}
		else if (empty($_POST['rp']) && empty($_POST['ccg']))
		{
			fwrite($file, "<title>".$_POST['title']."</titile>
			<game-tabletop>".$_POST['tabletop']."</game-tabletop>
			<game-rp>false</game-rp>
			<game-ccg>false</game-ccg>
			<place>".$_POST['place']."</place>
			<description>".$_POST['description']."</description>");
			$gameTypes = "Tabletop games";
		}
		else if (empty($_POST['tabletop']) && empty($_POST['ccg']))
		{
			fwrite($file, "<title>".$_POST['title']."</titile>
			<game-tabletop>false</game-tabletop>
			<game-rp>".$_POST['rp']."</game-rp>
			<game-ccg>false</game-ccg>
			<place>".$_POST['place']."</place>
			<description>".$_POST['description']."</description>");
			$gameTypes = "Role-Play tabletop games";
		}
		else if (empty($_POST['tabletop']))
		{
			fwrite($file, "<title>".$_POST['title']."</titile>
			<game-tabletop>false</game-tabletop>
			<game-rp>".$_POST['rp']."</game-rp>
			<game-ccg>".$_POST['ccg']."</game-ccg>
			<place>".$_POST['place']."</place>
			<description>".$_POST['description']."</description>");
			$gameTypes = "Role-Play and collecible card games";
		}
		else if (empty($_POST['rp']))
		{
			fwrite($file, "<title>".$_POST['title']."</titile>
			<game-tabletop>".$_POST['tabletop']."</game-tabletop>
			<game-rp>false</game-rp>
			<game-ccg>".$_POST['ccg']."</game-ccg>
			<place>".$_POST['place']."</place>
			<description>".$_POST['description']."</description>");
			$gameTypes = "Tabletop and collecible card games";
		}
		else if (empty($_POST['ccg']))
		{
			fwrite($file, "<title>".$_POST['title']."</titile>
			<game-tabletop>".$_POST['tabletop']."</game-tabletop>
			<game-rp>".$_POST['rp']."</game-rp>
			<game-ccg>false</game-ccg>
			<place>".$_POST['place']."</place>
			<description>".$_POST['description']."</description>");
			$gameTypes = "Tabletop and role-play games";
		}
		else
		{
			fwrite($file, "<title>".$_POST['title']."</titile>
			<game-tabletop>".$_POST['tabletop']."</game-tabletop>
			<game-rp>".$_POST['rp']."</game-rp>
			<game-ccg>".$_POST['ccg']."</game-ccg>
			<place>".$_POST['place']."</place>
			<description>".$_POST['description']."</description>");
			$gameTypes = "Tabletop, role-play and collecible card games";
		}
		fwrite($file, "
		<creator>".$_GET['nickName']."</creator>");
		flock($file, LOCK_UN);
		fclose($file);
		//----
		$gameTitle = $_POST['title'];
		$gameDate = substr($_POST['date'], 0, 10);
		$gameCreator = $_GET['nickName'];
		$gamePlace = $_POST['place'];
		$gameDesc = $_POST['description'];
		//----
		$params = array(
		'geocode' => $_POST['place'], // адрес
		'format'  => 'json',                          // формат ответа
		'results' => 1,                               // количество выводимых результатов
		'apikey'     => 'fdc43ad0-8c88-48b8-98f1-b9321ae07ff2',                           // ваш api key
		'sco' => 'latlong',
		'lang' => 'ru_RU',
		);
		$response = json_decode(file_get_contents('http://geocode-maps.yandex.ru/1.x/?' . http_build_query($params, '', '&')));
		 
		if ($response->response->GeoObjectCollection->metaDataProperty->GeocoderResponseMetaData->found > 0)
		{
			//echo substr($response->response->GeoObjectCollection->featureMember[0]->GeoObject->Point->pos, 10, 19);
			//echo " ";
			//echo substr($response->response->GeoObjectCollection->featureMember[0]->GeoObject->Point->pos, 0, 9);
			$coord1 = substr($response->response->GeoObjectCollection->featureMember[0]->GeoObject->Point->pos, 10, 19);
			$coord2 = substr($response->response->GeoObjectCollection->featureMember[0]->GeoObject->Point->pos, 0, 9);
		}
		else
		{
			echo 'Ничего не найдено';
		}
		//----
		$coordCatalog = file_get_contents("positions.js");
		$file = @fopen("positions.js", "w");
		flock($file, LOCK_EX);
		//fseek($file, -2);
		fwrite($file, substr($coordCatalog, 0, strlen($coordCatalog)-2));
		fwrite($file, "/*[".$_GET['nickName']."_".$_POST['date']."]*/myMap.geoObjects.add(new ymaps.Placemark([$coord1, $coord2],{balloonContent: 'Title: $gameTitle<br>Creator: $gameCreator<br>Date: $gameDate<br>Game types: $gameTypes<br>Address: $gamePlace<br>Description: $gameDesc',iconCaption: '$gameTitle'},{preset: 'islands#blueBookIcon'}));/*[".$_GET['nickName']."_".$_POST['date']."]*/".PHP_EOL."}");
		flock($file, LOCK_UN);
		fclose($file);
		usleep(650000);
		header("Location: index.php");
	}
?>