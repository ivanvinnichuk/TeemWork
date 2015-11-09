Param(
  [string]$pathToNewFolder,
  [string]$folderName
)

New-Item -Path $pathToNewFolder -ItemType "directory" -name $folderName