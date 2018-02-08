Connect-PnPOnline -Url https://folkis2017.sharepoint.com/sites/DavidClassic -UseWebLogin

Add-PnPCustomAction -Name "SiteActionsDavid" -Title "Open Google" -Description "opens a link to google" -Location "Microsoft.SharePoint.StandardMenu" -Group "SiteActions" -Sequence 1000 -Scope web -Url "https://www.google.com"