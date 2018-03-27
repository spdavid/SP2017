var github = (function () {
    function github() {
    }
    github.GetRepos = function () {
        var url = "https://api.github.com/users/spdavid/repos";
        console.log("1");
        fetch(url).then(function (response) {
            if (response.ok) {
                response.json().then(function (reposList) {
                    console.log(reposList);
                    var resultDiv = document.getElementById("result");
                    reposList.forEach(function (repo) {
                        resultDiv.innerHTML += "\n                            <div><a href=\"" + repo.html_url + "\">" + repo.name + "</a> by (" + repo.owner.login + ")</div>";
                    });
                });
            }
            else {
                console.log(response.statusText);
            }
        });
        console.log("3");
    };
    return github;
}());
document.addEventListener("DOMContentLoaded", function () {
    github.GetRepos();
});
//# sourceMappingURL=github.js.map