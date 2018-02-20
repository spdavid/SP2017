Connect-PnPOnline -Url https://folkis2017.sharepoint.com/sites/David -UseWebLogin

#	1. Create a list called Furniture
#		a. Title
#		b. Furniture type : choice
#		c. Year Purchased : number
#		d. Description : Text
#
#Create fields
#Create content type
#Create list
#Add content type to list. 
#
#
#
#	2. Change the title of your web
#
#
#	3. Add sharepoint group called "New Readers" that has read access to the site. : Challeng
#	4. Create subweb : Challenge
## create a guid [guid]::NewGuid()

Add-PnPField -InternalName DAV_FurnType -Id a017c4bc-81fa-42bb-93d2-b808d58a3a6d -DisplayName "Furniture type" -Type Choice -Choices "Wood","Metal","Chair","coutch" -Group "davids columns"
Add-PnPField -InternalName DAV_yearPurch -Id f2dbc300-969c-422f-884e-861fd25badda -DisplayName "Year Purchased" -Type Number 
Add-PnPField -InternalName DAV_furndesc -Id a7d4fc99-fc68-4026-bb9e-200a64db5742 -DisplayName "Description" -Type Text

Add-PnPContentType -Name Furniture -ContentTypeId 0x0100043966338fa74353bad3517d3723823b -Group "davids cts"

Add-PnPFieldToContentType -Field a017c4bc-81fa-42bb-93d2-b808d58a3a6d -ContentType 0x0100043966338fa74353bad3517d3723823b
Add-PnPFieldToContentType -Field f2dbc300-969c-422f-884e-861fd25badda -ContentType 0x0100043966338fa74353bad3517d3723823b
Add-PnPFieldToContentType -Field a7d4fc99-fc68-4026-bb9e-200a64db5742 -ContentType 0x0100043966338fa74353bad3517d3723823b

New-PnPList -Title Furniture -Template 100 -Url lists/furniture 

Add-PnPContentTypeToList -List Furniture -ContentType 0x0100043966338fa74353bad3517d3723823b -DefaultContentType 
 
Remove-PnPContentTypeFromList -List Furniture -ContentType Item

$view = get-pnpview -List Furniture -Identity "All Items" 
$ctx = Get-PnPContext
$ctx.Load($view);
$ctx.ExecuteQuery()

$view.ViewFields.Add("DAV_FurnType")
$view.ViewFields.Add("DAV_yearPurch")
$view.ViewFields.Add("DAV_furndesc")

$view.Update();
$ctx.ExecuteQuery();


Set-PnPWeb -Title "Davids cool site"


$group = New-PnPGroup -Title "Cool Readers" 

Set-PnPGroupPermissions -Identity "Cool Readers" -AddRole "Add Only"


New-PnPWeb -Title Subwebforme -Url subwebforme -Description "" -Template "STS#0" -ErrorAction SilentlyContinue


$web = Get-PnPWeb
Apply-PnPProvisioningTemplate -Path "C:\Users\david\source\repos\SP2017\OfficeDev1\ContentTypesAndFields\TaxonomyFun\Template.xml"




