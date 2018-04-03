import { Version } from '@microsoft/sp-core-library';
import {
  BaseClientSideWebPart,
  IPropertyPaneConfiguration,
  PropertyPaneTextField
} from '@microsoft/sp-webpart-base';
import { escape } from '@microsoft/sp-lodash-subset';

import styles from './ListCrudWebPart.module.scss';
import * as strings from 'ListCrudWebPartStrings';

import ListCrudHelper from './ListCrudHelper'

export interface IListCrudWebPartProps {
  description: string;
}

export default class ListCrudWebPart extends BaseClientSideWebPart<IListCrudWebPartProps> {

  public render(): void {
    this.domElement.innerHTML = `
      <input type='button' id='createList' value="create list" />
      <input type='button' id='deleteList' value="delete list" />
      <input type='button' id='updateList' value="update list" />
    `;



      document.getElementById('createList').onclick = () => { this.CreateList()};
      document.getElementById('deleteList').onclick = () => { this.DeleteList()};
      document.getElementById('updateList').onclick = () => { this.UpdateList()};
      

  }


  private CreateList()
  {
    var listhelper = new ListCrudHelper(this.context);
    listhelper.CreateList();



  }

  private DeleteList()
  {
    var listhelper = new ListCrudHelper(this.context);
    listhelper.DeleteList();
  }

  private UpdateList()
  {
    var listhelper = new ListCrudHelper(this.context);
    listhelper.UpdateList();
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
