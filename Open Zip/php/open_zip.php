<?php $sendBuffer = TRUE; ob_start(); ?>
<html>
<head>
<title>IPWorks ZIP 2022 Demos - Open Zip</title>
<link rel="stylesheet" type="text/css" href="stylesheet.css">
<meta name="description" content="IPWorks ZIP 2022 Demos - Open Zip">
</head>

<body>

<div id="content">
<h1>IPWorks ZIP - Demo Pages</h1>
<h2>Open Zip</h2>
<p>Shows how to examine and extract the contents of a zip file.</p>
<a href="default.php">[Other Demos]</a>
<hr/>

<?php
require_once('../include/ipworkszip_zip.php');
require_once('../include/ipworkszip_const.php');

?>

<form method=post>
<center>
<table width="90%">

 <tr><td>Full Path To Archive File:      <td><input type=text name=archivefile value="<?php echo isset($_POST["archivefile"])?$_POST["archivefile"]:"" ?>" size=80>
 <tr><td>Extract To Path:        <td><input type=text name=extracttopath value="<?php echo isset($_POST["extracttopath"])?$_POST["extracttopath"]:"" ?>" size=80>
 <tr><td>Archive Password (if applicable):    <td><input type=password name=password value="<?php echo isset($_POST["password"])?$_POST["password"]:"" ?>" size=20>

 <tr><td><td><input type=submit value="  Go!  ">

</table>
</center>
</form>

<?php
$zip = new IPWorksZip_Zip();
if ($_SERVER['REQUEST_METHOD'] == "POST") {


  $zip->setArchiveFile($_POST["archivefile"]);
  $zip->setExtractToPath($_POST["extracttopath"]);
  $zip->setPassword($_POST["password"]);

  try{
  $zip->doExtractAll();
  echo "<p>Files successfully extracted.<p>";
  } catch (Exception $e) {
    echo 'Error: ',  $e->getMessage(), "\n";
  }
?>

<center>
<table width="90%">
  <tr>
    <th>Name</th>
    <th>Size</th>
  </tr>

<?php
  for($i= 0;$i < $zip->getFileCount();$i++){
?>
  <tr>
    <td nowrap><?php echo htmlspecialchars($zip->getFileCompressedName($i)) ?></td>
    <td nowrap><?php echo htmlspecialchars($zip->getFileCompressedSize($i)) ?></td>
  </tr>

<?php
  } //for loop
}
?>

</table>
</center>

<br/>
<br/>
<br/>
<hr/>
NOTE: These pages are simple demos, and by no means complete applications.  They
are intended to illustrate the usage of the IPWorks ZIP objects in a simple,
straightforward way.  What we are hoping to demonstrate is how simple it is to
program with our components.  If you want to know more about them, or if you have
questions, please visit <a href="http://www.nsoftware.com/?demopg-IZPHA" target="_blank">www.nsoftware.com</a> or
contact our technical <a href="http://www.nsoftware.com/support/">support</a>.
<br/>
<br/>
Copyright (c) 2023 /n software inc.
<br/>
<br/>
</div>

<div id="footer">
<center>
IPWorks ZIP 2022 - Copyright (c) 2023 /n software inc. - For more information, please visit our website at <a href="http://www.nsoftware.com/?demopg-IZPHA" target="_blank">www.nsoftware.com</a>.
</center>
</div>

</body>
</html>

<?php if ($sendBuffer) ob_end_flush(); else ob_end_clean(); ?>
