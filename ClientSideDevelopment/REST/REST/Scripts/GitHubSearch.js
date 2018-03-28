var GitHubSearch = (function () {
    function GitHubSearch() {
    }
    GitHubSearch.SetUpPage = function () {
        var _this = this;
        var button = document.getElementById("searchbutton");
        button.onclick = function () { _this.GetSearchResults(); };
    };
    GitHubSearch.GetSearchResults = function () {
        var _this = this;
        var searchTextBox = document.getElementById("search");
        var searchText = searchTextBox.value;
        var url = "https://api.github.com/search/repositories?q=" + searchText;
        fetch(url).then(function (response) {
            if (response.ok) {
                response.json().then(function (data) {
                    _this.RenderResults(data);
                });
            }
            else {
                console.log(response.statusText);
            }
        });
    };
    GitHubSearch.RenderResults = function (data) {
        var resultsDiv = document.getElementById("results");
        resultsDiv.innerHTML = "";
        data.items.forEach(function (repo) {
            resultsDiv.innerHTML += "\n                 <div>\n                    <a href=\"" + repo.html_url + "\">" + repo.name + "</a> by (" + repo.owner.login + ")\n                </div>";
        });
    };
    return GitHubSearch;
}());
// wait for page to be fully loaded. 
document.addEventListener("DOMContentLoaded", function () {
    GitHubSearch.SetUpPage();
});
//# sourceMappingURL=GitHubSearch.js.map