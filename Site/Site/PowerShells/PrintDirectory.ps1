Param(
  [string]$path,
  [string]$pathToStore
)
$file = $path + ".json"
Get-ChildItem $path | Select-Object Name, Mode, LastWriteTime, FullName | ConvertTo-Json | Out-File $file.Replace("\","_")
Copy-Item $PSScriptRoot\*.json $pathToStore
Remove-Item $PSScriptRoot\*.json
