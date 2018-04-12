(function () {
    var helloworlddiv = document.getElementById("helloworld");

    function GetJson(Url, callback) {
        var request = new XMLHttpRequest();
        request.open('GET', Url, true);
        request.setRequestHeader('Accept', 'application/json');


        request.onload = function () {
            if (request.status >= 200 && request.status < 400) {
                // Success!
                var data = JSON.parse(request.responseText);

                callback(data);

            } else {
                // We reached our target server, but it returned an error

            }
        };

        request.onerror = function () {
            // There was a connection error of some sort
        };

        request.send();
    }

    GetJson(_spPageContextInfo.webAbsoluteUrl + '/_api/web/lists',
    function (data) {
        console.log(data);

        data.value.forEach(list => {
            helloworlddiv.innerHTML += "<div>" + list.Title + "</div>";
        });
     }
);



})(); 