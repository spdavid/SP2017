connect-pnponline -url https://folkis2017.sharepoint.com/sites/davidclassic

New-PnPList -Title Person -Template GenericList 

$list = get-pnpList -Identity Person

Add-PnPField -Id 88e6f56d-bc95-4ef7-854f-0d8924b4173e -List $list -InternalName persLName -DisplayName "Last Name" -Type Text 
Add-PnPField -Id 89b81482-1bb5-40f5-91ff-f9589b8d7383 -List $list -InternalName persFullName -DisplayName "Full Name" -Type Text


$TitleField = Get-PnPField -List $list -Identity Title
$TitleField.Title = "First Name"
$TitleField.UpdateAndPushChanges($true)
$TitleField.Update()

$f = Get-PnPField -List $list -Identity 89b81482-1bb5-40f5-91ff-f9589b8d7383
$f.ReadOnlyField = $true
$f.UpdateAndPushChanges($true)
$f.Update()

Execute-PnPQuery

$views = Get-PnPView -List $list 
$defaultView = $Views[0]



$defaultView.ViewFields.Add("persLName")
$defaultView.ViewFields.Add("persFullName")
$defaultView.Update()

Execute-PnPQuery
