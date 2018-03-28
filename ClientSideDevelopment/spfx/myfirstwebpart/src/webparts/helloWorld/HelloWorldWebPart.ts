import { Version } from '@microsoft/sp-core-library';
import {
  BaseClientSideWebPart,
  IPropertyPaneConfiguration,
  PropertyPaneTextField
} from '@microsoft/sp-webpart-base';
import { escape } from '@microsoft/sp-lodash-subset';

import styles from './HelloWorldWebPart.module.scss';
import * as strings from 'HelloWorldWebPartStrings';

import {
  SPHttpClient,
  SPHttpClientResponse
} from '@microsoft/sp-http';

import WebInfoHelper from './WebInfoHelper'

export interface IHelloWorldWebPartProps {
  description: string;
}

export default class HelloWorldWebPart extends BaseClientSideWebPart<IHelloWorldWebPartProps> {

  public render(): void {

    WebInfoHelper.GetWebData(this.context).then(
      (data) => {
        this.domElement.innerHTML = `
                  <div>Welcome To your site : ${data.Title}</div>
                  <img src='${data.SiteLogoUrl}' />
                  `;
      });

  //var element = document.getElementById("results");

  // var url = this.context.pageContext.web.absoluteUrl + "/_api/web";
  // this.context.spHttpClient.get(url, SPHttpClient.configurations.v1).then(
  //   (response) => {
  //     if (response.ok) {
  //       response.json().then((data) => {
  //         console.log(data);

  //         this.domElement.innerHTML = `
  //               <div>Welcome To your site : ${data.Title}</div>
  //               <img src='${data.SiteLogoUrl}' />
  //               `;
  //       });

  //     }
  //   }

  // );





}

  protected get dataVersion(): Version {
  return Version.parse('1.0');
}

  protected getPropertyPaneConfiguration(): IPropertyPaneConfiguration {
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
}
}
