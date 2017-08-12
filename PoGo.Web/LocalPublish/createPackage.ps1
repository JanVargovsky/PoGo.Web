Add-Type -Assembly "System.IO.Compression.FileSystem" ;
$inputFolder = $PSScriptRoot + "\Output";
$date = [System.DateTime]::Now.ToString("yyyy-MM-dd--HH-mm");
$outputZip = $PSScriptRoot + "\PoGoWeb_" + $date + ".zip";
Write-Debug $inputFolder
Write-Debug $outputZip
[System.IO.Compression.ZipFile]::CreateFromDirectory($inputFolder, $outputZip) ;
Write-Host $outputZip " created";