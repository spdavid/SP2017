
if ("5" == 5)
{
    console.log("they are the same");
}

if ("5" === 5) {
    console.log("they are the same 2");
}
else
{
    console.log("they are not the same 2");
}


function calc(num1, num2)
{
    var newNum = num1 + num2;

   var mainElement = document.getElementById('main');
   mainElement.innerHTML = "<div>" + newNum + "</div>";
}

function calcInputs() {
    var num1 = parseInt(document.getElementById('firstNumber').value);
    var num2 = parseInt(document.getElementById('secondNumber').value);

    var newNum = num1 + num2;

    var mainElement = document.getElementById('main');
    mainElement.innerHTML = "<div>" + newNum + "</div>";
    
}


//window.onload = function ()
//{
//    calc(5, 3);
//}