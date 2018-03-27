var Calculator = (function () {
    function Calculator() {
    }
    Calculator.prototype.SetUpCalculator = function () {
        var _this = this;
        console.log("calc setup");
        this.ready(function () {
            var elements = document.getElementsByClassName("calcbutton");
            _this.result = document.getElementById("result");
            var me = _this;
            for (var i = 0; i < elements.length; i++) {
                (function (idx) {
                    var btn = elements[idx];
                    btn.onclick = function () {
                        console.log("clicked");
                        me.ButtonClicked(btn.value);
                    };
                })(i);
            }
            _this.currentNumber = null;
        });
    };
    Calculator.prototype.ButtonClicked = function (value) {
        switch (value) {
            case "+":
                this.currentOperator = value;
                break;
            case "-":
                this.currentOperator = value;
                break;
            default:
                if (this.currentNumber == null) {
                    this.currentNumber = parseInt(value);
                }
                else {
                    if (this.currentOperator != null) {
                        this.doCalcuation(parseInt(value));
                    }
                    else {
                        this.currentNumber = parseInt(value);
                    }
                }
                break;
        }
    };
    Calculator.prototype.doCalcuation = function (secondNumber) {
        if (this.currentOperator == "+") {
            console.log(this);
            var result = (this.currentNumber + secondNumber).toString();
            this.result.value = result;
            this.currentNumber = parseInt(result);
        }
        else {
            var result = (this.currentNumber - secondNumber).toString();
            this.result.value = result;
            this.currentNumber = parseInt(result);
        }
    };
    Calculator.prototype.ready = function (fn) {
        if (document.attachEvent ? document.readyState === "complete" : document.readyState !== "loading") {
            fn();
        }
        else {
            document.addEventListener('DOMContentLoaded', fn);
        }
    };
    return Calculator;
}());
//# sourceMappingURL=calculator.js.map