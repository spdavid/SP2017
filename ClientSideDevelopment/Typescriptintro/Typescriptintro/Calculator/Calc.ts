
class Calculator
{
    result: HTMLInputElement;
    currentNumber: number
    currentOperator: string


    public SetUpCalculator()
    {
        console.log("calc setup");
        this.ready(() => {
            var elements = document.getElementsByClassName("calcbutton");
           this.result = document.getElementById("result") as HTMLInputElement;

            var me = this;
            for (var i = 0; i < elements.length; i++) {
                (function (idx) {
                    var btn = elements[idx] as HTMLButtonElement;
                    btn.onclick = () => {
                        console.log("clicked");
                        me.ButtonClicked(btn.value);
                    };
                })(i);
            }

            this.currentNumber = null;

        });

    }

    private ButtonClicked(value: string)
    {
        switch (value) {
            case "+":
                this.currentOperator = value;
                break;
            case "-":
                this.currentOperator = value;
                break;

            default:
                if (this.currentNumber == null)
                {
                    this.currentNumber = parseInt(value);
                }
                else
                {
                    if (this.currentOperator != null) {
                        this.doCalcuation(parseInt(value));
                    }
                    else
                    {
                        this.currentNumber = parseInt(value);
                    }
                }
                break;
        }


    }

    private doCalcuation(secondNumber : number)
    {
        if (this.currentOperator == "+")
        {
            console.log(this);
            var result = (this.currentNumber + secondNumber).toString();
            this.result.value = result;
            this.currentNumber = parseInt(result);
        }
        else
        {
            var result = (this.currentNumber - secondNumber).toString();
            this.result.value = result;
            this.currentNumber = parseInt(result);
        }
    }


    private ready(fn) {
        if ((document as any).attachEvent ? document.readyState === "complete" : document.readyState !== "loading") {
            fn();
        } else {
            document.addEventListener('DOMContentLoaded', fn);
        }
    }


}