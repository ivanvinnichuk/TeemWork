Param
(
    [string]$itemToCopy,
    [string]$destination
)

Copy-Item $itemToCopy $destination
Remove-Item $itemToCopy
