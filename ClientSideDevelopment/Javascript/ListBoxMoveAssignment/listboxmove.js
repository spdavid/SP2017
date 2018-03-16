

( () => { //iife
    // function hello() {
    //     alert("hello");
    // }
    
    function ready(fn) {
        if (document.attachEvent ? document.readyState === "complete" : document.readyState !== "loading") {
            fn();
        } else {
            document.addEventListener('DOMContentLoaded', fn);
        }
    }
    ready( () => {
        document.getElementById("moveleft").onclick =  () => { moveItems("right", "left") };
        document.getElementById("moveright").onclick =  () => { moveItems("left", "right") };;
    });
    function moveItems(from, to) {
        var fromElement = document.getElementById(from + "box");
        var toElement = document.getElementById(to + "box");


        console.log(fromElement.options);
        for (let i = fromElement.options.length - 1; i >= 0; i--) {
            const option = fromElement.options[i];
            if (option.selected) {
                toElement.appendChild(option);
                option.selected = false;
            }
        }
    }



        





})();


function runCallback(fn)
{
  fn("david");
}

function callback(name)
{
    alert("hello " + name);
}


//runCallback(callback);

runCallback(
    name => {  alert("hello2 " + name)}
);