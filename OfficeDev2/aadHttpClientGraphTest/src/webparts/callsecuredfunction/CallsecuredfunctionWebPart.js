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
import * as ReactDom from 'react-dom';
import { Version } from '@microsoft/sp-core-library';
import { BaseClientSideWebPart, PropertyPaneTextField, } from '@microsoft/sp-webpart-base';
import * as strings from 'CallsecuredfunctionWebPartStrings';
import Callsecuredfunction from './components/Callsecuredfunction';
var CallsecuredfunctionWebPart = (function (_super) {
    __extends(CallsecuredfunctionWebPart, _super);
    function CallsecuredfunctionWebPart() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    CallsecuredfunctionWebPart.prototype.render = function () {
        var element = React.createElement(Callsecuredfunction, {
            description: this.properties.description,
            context: this.context
        });
        ReactDom.render(element, this.domElement);
    };
    CallsecuredfunctionWebPart.prototype.onDispose = function () {
        ReactDom.unmountComponentAtNode(this.domElement);
    };
    Object.defineProperty(CallsecuredfunctionWebPart.prototype, "dataVersion", {
        get: function () {
            return Version.parse('1.0');
        },
        enumerable: true,
        configurable: true
    });
    CallsecuredfunctionWebPart.prototype.getPropertyPaneConfiguration = function () {
        return {
            pages: [
                {
                    header: {
                        description: strings.PropertyPaneDescription
                    },
                    groups: [
                        {
                            groupName: strings.BasicGroupName,
                            groupFields: [
                                PropertyPaneTextField('description', {
                                    label: strings.DescriptionFieldLabel
                                })
                            ]
                        }
                    ]
                }
            ]
        };
    };
    return CallsecuredfunctionWebPart;
}(BaseClientSideWebPart));
export default CallsecuredfunctionWebPart;
//# sourceMappingURL=CallsecuredfunctionWebPart.js.map