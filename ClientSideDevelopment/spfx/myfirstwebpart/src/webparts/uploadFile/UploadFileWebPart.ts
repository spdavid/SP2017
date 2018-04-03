import { Version } from '@microsoft/sp-core-library';
import {
  BaseClientSideWebPart,
  IPropertyPaneConfiguration,
  PropertyPaneTextField
} from '@microsoft/sp-webpart-base';
import { escape } from '@microsoft/sp-lodash-subset';

import styles from './UploadFileWebPart.module.scss';
import * as strings from 'UploadFileWebPartStrings';
import { sp } from "@pnp/sp";

export interface IUploadFileWebPartProps {
  description: string;
}

export default class UploadFileWebPart extends BaseClientSideWebPart<IUploadFileWebPartProps> {

  public onInit(): Promise<void> {

    return super.onInit().then(_ => {
      // other init code may be present
      sp.setup({
        spfxContext: this.context
      });

    });
  }

  public render(): void {
    this.domElement.innerHTML = `
    <input type="file" id="fileUpload">
    <input type="button" id="fileUploadButton" value="Upload To SharePoint" />
    `;

   var button = document.getElementById("fileUploadButton") as HTMLButtonElement
   button.onclick = () => {this.UploadFile()};


  }

  private UploadFile()
  {
    var filePicker = document.getElementById("fileUpload") as HTMLInputElement;
    var file = filePicker.files[0];
    
    // create a file reader to read your file info
    var reader = new FileReader();
    // do something when the file is loaded
    reader.onload = () => {
      var arrayBuffer = reader.result;
      sp.web.lists.getByTitle("Documents").rootFolder.files.add(file.name,arrayBuffer);
    };
    
    // this will load the file. 
    reader.readAsArrayBuffer(file);
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
