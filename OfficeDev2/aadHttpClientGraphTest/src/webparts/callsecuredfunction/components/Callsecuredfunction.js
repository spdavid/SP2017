var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
import * as React from 'react';
import { AadHttpClient } from '@microsoft/sp-http';
var Callsecuredfunction = (function (_super) {
    __extends(Callsecuredfunction, _super);
    function Callsecuredfunction(props) {
        var _this = _super.call(this, props) || this;
        _this.state = { isReady: false };
        return _this;
    }
    Callsecuredfunction.prototype.componentWillMount = function () {
        var _this = this;
        var client = new AadHttpClient(this.props.context.serviceScope, "1c149e0d-91a9-452b-8782-2125c7db70cf")
            .get("https://davidscoolfunctions.azurewebsites.net/api/SecuredAPI", AadHttpClient.configurations.v1)
            .then(function (result) {
            if (result.ok) {
                result.json().then(function (data) {
                    console.log(data);
                    _this._data = data;
                    _this.setState({ isReady: true });
                });
            }
        });
    };
    Callsecuredfunction.prototype.render = function () {
        if (this.state.isReady) {
            return (React.createElement("div", null,
                "Hello ",
                this._data.developer,
                " from ",
                this._data.location));
        }
        else {
            return (React.createElement("div", null));
        }
    };
    return Callsecuredfunction;
}(React.Component));
export default Callsecuredfunction;
//# sourceMappingURL=Callsecuredfunction.js.map