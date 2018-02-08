Connect-PnPOnline -Url https://folkis2017.sharepoint.com/sites/DavidClassic -UseWebLogin

Add-PnPCustomAction -Name "SiteActionsDavid" -Title "Open Google" -Description "opens a link to google" -Location "Microsoft.SharePoint.StandardMenu" -Group "SiteActions" -Sequence 1000 -Scope web -Url "https://www.google.com" 
Add-PnPCustomAction -Name "SiteActionsDavid2" -Title "Javascript" -Description "runs javascript"  -Location "Microsoft.SharePoint.StandardMenu" -Group "SiteActions" -Sequence 1002 -Scope web -Url "javascript:alert('hello world');"


$list = Get-PnPList -Identity "Documents"
$listAction = $list.UserCustomActions.Add();
$listAction.Location = "EditControlBlock";
$listAction.Name = "customname3";
$listAction.Title = "Do Something 3";
$listAction.Url = "https://fakeurl?listid={ListId}&ItemId={ItemId}";
$listAction.Sequence = 5;
$listAction.Update();

Execute-PnPQuery




#Add-PnPCustomAction -RegistrationType List -RegistrationId $list -Group "notapplicable" -Name "DocumentAction" -Title "Do Something" -Description "custom context menu item"  -Location "EditControlBlock"  -Sequence 1004 -Scope Site -Url "https://spdavid.com"

#Add-PnPCustomAction -RegistrationType List -RegistrationId $list -Name "Do something" -Title "JavaScript" -Description "Something description" -Location "EditControlBlock" -Sequence 1004 -Scope Site -Url "http://www.webhallen.se"

