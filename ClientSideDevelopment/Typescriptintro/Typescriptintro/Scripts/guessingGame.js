var GuessingGame = (function () {
    function GuessingGame() {
    }
    GuessingGame.prototype.StartGame = function () {
        var _this = this;
        this.ready(function () {
            _this.randomNumber = Math.floor(Math.random() * 100) + 1;
            var element = document.createElement("div");
            element.innerHTML = "\n            <input type='text' id=\"Guess\" />\n            <input type='button' id=\"GuessButton\" value=\"Make a guess\" />\n            <div id=\"response\"></div>\n                 ";
            document.body.appendChild(element);
            _this.button = document.getElementById("GuessButton");
            _this.guessTextBox = document.getElementById("Guess");
            _this.responseBox = document.getElementById("response");
            _this.button.onclick = function () { _this.MakeGuess(); };
            var me = _this;
            _this.guessTextBox.addEventListener("keyup", function (event) {
                console.log("does this click");
                event.preventDefault();
                if (event.keyCode === 13) {
                    me.button.click();
                }
            });
        });
    };
    GuessingGame.prototype.MakeGuess = function () {
        console.log("guessing");
        var answer = parseInt(this.guessTextBox.value);
        if (answer == this.randomNumber) {
            this.responseBox.innerText = "You are correct. the answer is " + this.randomNumber.toString();
        }
        if (answer > this.randomNumber) {
            this.responseBox.innerText = "Your guess was " + answer.toString() + ". You must go lower";
        }
        if (answer < this.randomNumber) {
            this.responseBox.innerText = "Your guess was " + answer.toString() + ". You must go higher";
        }
        this.guessTextBox.value = "";
        this.guessTextBox.focus();
    };
    GuessingGame.prototype.ready = function (fn) {
        if (document.attachEvent ? document.readyState === "complete" : document.readyState !== "loading") {
            fn();
        }
        else {
            document.addEventListener('DOMContentLoaded', fn);
        }
    };
    return GuessingGame;
}());
//# sourceMappingURL=guessingGame.js.map