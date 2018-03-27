class GuessingGame
{
    
    randomNumber: number;
    button: HTMLButtonElement;
    guessTextBox: HTMLInputElement;
    responseBox: HTMLDivElement;
    
    public StartGame()
    {
        this.ready(() => {
            this.randomNumber = Math.floor(Math.random() * 100) + 1;

            var element = document.createElement("div");

            element.innerHTML = `
            <input type='text' id="Guess" />
            <input type='button' id="GuessButton" value="Make a guess" />
            <div id="response"></div>
                 `;

            document.body.appendChild(element);

            this.button = document.getElementById("GuessButton") as HTMLButtonElement;
            this.guessTextBox = document.getElementById("Guess") as HTMLInputElement;
            this.responseBox = document.getElementById("response") as HTMLDivElement;

            this.button.onclick = () => { this.MakeGuess() };

            var me = this;

            this.guessTextBox.addEventListener("keyup", function (event) {
                console.log("does this click");
                event.preventDefault();
                if (event.keyCode === 13) {
                    me.button.click();
                }
            });

        });

    }

    private MakeGuess()
    {
        console.log("guessing");
        var answer: number = parseInt(this.guessTextBox.value);

        if (answer == this.randomNumber)
        {
            this.responseBox.innerText = "You are correct. the answer is " + this.randomNumber.toString();
        }

        if (answer > this.randomNumber) {
            this.responseBox.innerText = `Your guess was ${answer.toString()}. You must go lower`;
        }

        if (answer < this.randomNumber) {
            this.responseBox.innerText = `Your guess was ${answer.toString()}. You must go higher`;
        }

        this.guessTextBox.value = "";
        this.guessTextBox.focus();

        
    }


    private ready(fn) {
    if ((document as any).attachEvent ? document.readyState === "complete" : document.readyState !== "loading") {
        fn();
    } else {
        document.addEventListener('DOMContentLoaded', fn);
    }
}
    
}