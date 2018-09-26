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
import { MSGraphClient } from '@microsoft/sp-client-preview';
var GraphTesting = (function (_super) {
    __extends(GraphTesting, _super);
    function GraphTesting(props) {
        var _this = _super.call(this) || this;
        _this.state = { isready: false };
        return _this;
    }
    GraphTesting.prototype.componentWillMount = function () {
        var _this = this;
        var graphClient = this.props.context.serviceScope.consume(MSGraphClient.serviceKey);
        graphClient
            .api("me/planner/tasks")
            .version("v1.0")
            .get(function (err, res) {
            if (err) {
                console.error(err);
                return;
            }
            console.log(res.value);
            _this._tasks = res.value.map(function (val) {
                return { title: val.title, date: new Date(val.createdDateTime) };
            });
            _this.setState({ isready: true });
        });
        // graphClient.get("https://graph.microsoft.com/v1.0/me/planner/tasks", GraphHttpClient.configurations.v1)
        //   .then(request => {
        //     if (request.ok)
        //     {
        //       request.json().then(data => {console.log(data)});
        //     }
        //     else
        //     {
        //       console.log(request.statusText);
        //     }
        //   })
    };
    GraphTesting.prototype.render = function () {
        return (React.createElement("div", null, this.state.isready &&
            this._tasks.map(function (task) {
                return React.createElement("div", null,
                    task.title,
                    " : ",
                    task.date.toLocaleDateString());
            })));
    };
    return GraphTesting;
}(React.Component));
export default GraphTesting;
//# sourceMappingURL=GraphTesting.js.map