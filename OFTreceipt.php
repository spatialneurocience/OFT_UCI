<?php
	$text1 = $_POST["input"];
	$filename = $_POST["filename"];

	if ($text1 != "")
	{
		$file = fopen($filename, "a"); 
		fwrite($file, $text1);
		fclose($file);
	} else
	{
		echo("Messsage delivery failed...");
	}

	$text2 = $_POST["input2"];
	$filename2 = $_POST["filename2"];

	if ($text2 != "")
	{
		$file2 = fopen($filename2, "a"); 
		fwrite($file2, $text2);
		fclose($file2);
	} else
	{
		echo("Messsage delivery failed...");
	}
?>