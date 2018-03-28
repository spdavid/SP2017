interface Owner {
    login: string;
    id: number;
    avatar_url: string;
    gravatar_id: string;
    url: string;
    html_url: string;
    followers_url: string;
    following_url: string;
    gists_url: string;
    starred_url: string;
    subscriptions_url: string;
    organizations_url: string;
    repos_url: string;
    events_url: string;
    received_events_url: string;
    type: string;
    site_admin: boolean;
}

interface License {
    key: string;
    name: string;
    spdx_id: string;
    url: string;
}

interface Item {
    id: number;
    name: string;
    full_name: string;
    owner: Owner;
    private: boolean;
    html_url: string;
    description: string;
    fork: boolean;
    url: string;
    forks_url: string;
    keys_url: string;
    collaborators_url: string;
    teams_url: string;
    hooks_url: string;
    issue_events_url: string;
    events_url: string;
    assignees_url: string;
    branches_url: string;
    tags_url: string;
    blobs_url: string;
    git_tags_url: string;
    git_refs_url: string;
    trees_url: string;
    statuses_url: string;
    languages_url: string;
    stargazers_url: string;
    contributors_url: string;
    subscribers_url: string;
    subscription_url: string;
    commits_url: string;
    git_commits_url: string;
    comments_url: string;
    issue_comment_url: string;
    contents_url: string;
    compare_url: string;
    merges_url: string;
    archive_url: string;
    downloads_url: string;
    issues_url: string;
    pulls_url: string;
    milestones_url: string;
    notifications_url: string;
    labels_url: string;
    releases_url: string;
    deployments_url: string;
    created_at: Date;
    updated_at: Date;
    pushed_at: Date;
    git_url: string;
    ssh_url: string;
    clone_url: string;
    svn_url: string;
    homepage: string;
    size: number;
    stargazers_count: number;
    watchers_count: number;
    language: string;
    has_issues: boolean;
    has_projects: boolean;
    has_downloads: boolean;
    has_wiki: boolean;
    has_pages: boolean;
    forks_count: number;
    mirror_url?: any;
    archived: boolean;
    open_issues_count: number;
    license: License;
    forks: number;
    open_issues: number;
    watchers: number;
    default_branch: string;
    score: number;
}

interface ISearchResults {
    total_count: number;
    incomplete_results: boolean;
    items: Item[];
}



class GitHubSearch
{
   
    public static SetUpPage()
    {
        var button = document.getElementById("searchbutton") as HTMLButtonElement;

        button.onclick = () => { this.GetSearchResults(); };
    }



    public static GetSearchResults(): Promise<ISearchResults>
    {
        return new Promise(function (resolve, reject) {
            setTimeout(resolve, 100, 'foo');
        });


    }


    //public static GetSearchResults()
    //{
    //    var searchTextBox = document.getElementById("search") as HTMLInputElement;

    //    var searchText = searchTextBox.value;

    //    var url = `https://api.github.com/search/repositories?q=${searchText}`;

    //    fetch(url).then(
    //        (response) => {
    //            if (response.ok)
    //            {
    //                response.json().then(
    //                    data =>
    //                    {
    //                        this.RenderResults(data);
    //                    });
    //            }
    //            else
    //            {
    //                console.log(response.statusText);
    //            }

    //        });
    //}


    //private static RenderResults(data: ISearchResults)
    //{
        

    //    var resultsDiv = document.getElementById("results") as HTMLDivElement;
    //    resultsDiv.innerHTML = "";
    //    data.items.forEach((repo) => { 
    //        resultsDiv.innerHTML += `
    //             <div>
    //                <a href="${repo.html_url}">${repo.name}</a> by (${repo.owner.login})
    //            </div>`;
    //            });
    //    }

}

// wait for page to be fully loaded. 
document.addEventListener("DOMContentLoaded", function () {
    GitHubSearch.SetUpPage();
});
