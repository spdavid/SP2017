

function fixLogo() 
{
    document.getElementById("ctl00_onetidHeadbnnr2").src = "https://trellis.co/wp-content/uploads/2015/09/hidden_meanings_facts_within_famous_logos_cover_image.jpg";

}


function addCss()
{
    var link = document.createElement('link');
link.setAttribute('rel', 'stylesheet');
link.setAttribute('type', 'text/css');
link.setAttribute('href', 'https://folkis2017.sharepoint.com/sites/DavidPub/_catalogs/masterpage/ABrand/styles.css');
document.getElementsByTagName('head')[0].appendChild(link);
}

_spBodyOnLoadFunctionNames.push("fixLogo");
_spBodyOnLoadFunctionNames.push("addCss");
