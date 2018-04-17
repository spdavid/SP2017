import { Version } from '@microsoft/sp-core-library';
import {
  BaseClientSideWebPart,
  IPropertyPaneConfiguration,
  PropertyPaneTextField
} from '@microsoft/sp-webpart-base';
import { escape } from '@microsoft/sp-lodash-subset';

import styles from './SearchTestWebPart.module.scss';
import * as strings from 'SearchTestWebPartStrings';

import {sp, SearchQueryBuilder, SortDirection} from '@pnp/sp'

export interface ISearchTestWebPartProps {
  description: string;
}

export default class SearchTestWebPart extends BaseClientSideWebPart<ISearchTestWebPartProps> {


  public onInit(): Promise<void> {
    return super.onInit().then(_ => {
      sp.setup({
        spfxContext: this.context
      });
    });
  }
  


  public render(): void {


    const q = SearchQueryBuilder
      .create()
      .text("FileType:docx")
      .selectProperties('Author','Filename','Path','SecondaryFileExtension','ContentTypeId');
      // .sortList({Property: "RefinableInt00", Direction : SortDirection.Descending});


    sp.search(q).then
    (results => {
      console.log(results);
      results.PrimarySearchResults.forEach((result : any) => {
        this.domElement.innerHTML += `<div><a href='${result.Path}'>${result.Filename}</a> by ${result.Author}</div>`;

      });
    });

    
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
