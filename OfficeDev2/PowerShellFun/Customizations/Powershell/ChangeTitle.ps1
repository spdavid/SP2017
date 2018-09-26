Write-Host "Running update Title" -ForegroundColor Green

$web = Get-PnPWeb 
$web.Title = "David New Title"
$web.Update()

Invoke-PnPQuery

Write-Host "Done update Title" -ForegroundColor Green

