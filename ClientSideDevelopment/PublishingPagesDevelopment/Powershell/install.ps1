Connect-PnPOnline -Url https://folkis2017.sharepoint.com/sites/DavidPub/ -UseWebLogin

$w = Get-PnPWeb

$w.AlternateCssUrl =   "https://folkis2017.sharepoint.com/sites/DavidPub/_catalogs/masterpage/ABrand/styles.css";
$w.Update()

Execute-PnPQuery


Add-PnPJavaScriptLink -Name davscript -Url "https://folkis2017.sharepoint.com/sites/DavidPub/_catalogs/masterpage/ABrand/main.js" -Scope Site