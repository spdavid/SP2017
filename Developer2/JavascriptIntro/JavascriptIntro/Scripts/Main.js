

function myFunction(additionaltext) {
    var myFirstVar = "hello world";
    var myint = 3;
    console.log(myFirstVar + additionaltext);

    var person = {
        name: "david",
        age : 37
    };

    person.height = 182;

    console.log(person);

    var mainDivElement = document.getElementById('main');
    mainDivElement.innerText = "Hello my name is " + person.name;

}

window.onload = function () {
    myFunction(" hello folkis");
}