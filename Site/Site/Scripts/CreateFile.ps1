Param(
  [string]$pathToNewFile,
  [string]$fileName,
  [string]$text
)

New-Item -Path $pathToNewFile -ItemType "file" -Value $text -name $fileName 