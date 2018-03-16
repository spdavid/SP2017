// waits for the page to be fully loaded. Then will run your function
function ready(fn) {
    if (document.attachEvent ? document.readyState === "complete" : document.readyState !== "loading") {
        fn();
    } else {
        document.addEventListener('DOMContentLoaded', fn);
    }
}

function addToMyElement () {
    var myelement = document.getElementById("myelement");
    var myTextBox = document.getElementById("txt");
    
    console.log(myelement);

   // myelement.innerText = "Hello World";
  // myelement.innerHTML = "<h1>Hello World</h1>";
  myelement.innerText = myTextBox.value;
 
}

ready(function () {
    var btn = document.getElementById("button");
    btn.onclick = addToMyElement;

});


//ready(addToMyElement);


