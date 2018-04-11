import { Version } from '@microsoft/sp-core-library';
import {
  BaseClientSideWebPart,
  IPropertyPaneConfiguration,
  PropertyPaneTextField
} from '@microsoft/sp-webpart-base';
import { escape } from '@microsoft/sp-lodash-subset';

import styles from './ShowListsWebPart.module.scss';
import * as strings from 'ShowListsWebPartStrings';

import { IListData, ListsService }  from './ListsService'

export interface IShowListsWebPartProps {
  description: string;
}

export default class ShowListsWebPart extends BaseClientSideWebPart<IShowListsWebPartProps> {

  public render(): void {


this.domElement.innerHTML = `<div id='${styles.ShowListsContainer}'>hello world </div>`;

    ListsService.GetListsFromSharePoint(this.context)
      .then((lists : Array<any>) => {
        var promArray : Array<Promise<IListData>> = [];
        lists.forEach(list => {
          promArray.push(ListsService.GetDefaultUrlFromList(this.context, list.Title));
        });

        Promise.all(promArray).then(values => {
        var listContainer =  document.getElementById(styles.ShowListsContainer);
          values.forEach(listinfo => {
            listContainer.innerHTML += `
            <div><a class='${styles.listLink}'  href='${listinfo.Url}'>${listinfo.Title}</a></div>`;
          })
          console.log(values);
        });
      })
   
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
