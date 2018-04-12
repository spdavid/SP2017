import * as React from 'react';
import * as ReactDom from 'react-dom';
import { Version } from '@microsoft/sp-core-library';
import {
  BaseClientSideWebPart,
  IPropertyPaneConfiguration,
  PropertyPaneTextField
} from '@microsoft/sp-webpart-base';



import * as strings from 'GettingTaxonomyWebPartStrings';
import GettingTaxonomy from './components/GettingTaxonomy';
import { IGettingTaxonomyProps } from './components/IGettingTaxonomyProps';

export interface IGettingTaxonomyWebPartProps {
  description: string;
}

export default class GettingTaxonomyWebPart extends BaseClientSideWebPart<IGettingTaxonomyWebPartProps> {

  public render(): void {
    const element: React.ReactElement<IGettingTaxonomyProps > = React.createElement(
      GettingTaxonomy,
      {
        description: this.properties.description,
        context : this.context
        
      }
    );

    ReactDom.render(element, this.domElement);
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
