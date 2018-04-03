import { Version } from '@microsoft/sp-core-library';
import {
  BaseClientSideWebPart,
  IPropertyPaneConfiguration,
  PropertyPaneTextField,
  PropertyPaneDropdown,
  PropertyPaneDropdownOptionType,
  PropertyPaneSlider,
  IPropertyPaneDropdownOption
} from '@microsoft/sp-webpart-base';
import { escape } from '@microsoft/sp-lodash-subset';

import styles from './PropertyPanelFunWebPart.module.scss';
import * as strings from 'PropertyPanelFunWebPartStrings';
import { sp } from "@pnp/sp";

export interface IPropertyPanelFunWebPartProps {
  description: string;
  color: string;
  amount: number;
}

export default class PropertyPanelFunWebPart extends BaseClientSideWebPart<IPropertyPanelFunWebPartProps> {
  private colorOptions : IPropertyPaneDropdownOption[];

  public onInit(): Promise<void> {

    return super.onInit().then(_ => {
      // other init code may be present
      sp.setup({
        spfxContext: this.context
      });

      sp.web.lists.getByTitle("Color").items.select("Title").getAll()
        .then(data => {
            var options : IPropertyPaneDropdownOption[] = [];

            data.forEach((color, idx) => {
              options.push({
                key : color.Title,
                index : idx,
                text : color.Title
              });

            });;
            this.colorOptions = options;

        });


    });
  }

  public render(): void {
    this.domElement.innerHTML = `
      <div class="${ styles.propertyPanelFun}">
        <div class="${ styles.container}">
          <div class="${ styles.row}" style="background-color:${this.properties.color}">
            <div class="${ styles.column}">
              <span class="${ styles.title}">Welcome to SharePoint!</span>
              <p class="${ styles.subTitle}">Customize SharePoint experiences using Web Parts.</p>
              <p class="${ styles.description}">${escape(this.properties.description)}</p>
              <p class="${ styles.description}">${escape(this.properties.color)}</p>
              
              <a href="https://aka.ms/spfx" class="${ styles.button}">
                <span class="${ styles.label}">Learn more</span>
              </a>
            </div>
          </div>
        </div>
      </div>`;
  }

  protected get dataVersion(): Version {
    return Version.parse('1.0');
  }

  protected get disableReactivePropertyChanges(): boolean {
    return true;
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
            },
            {
              groupName: "Custom Props",
              groupFields: [
                PropertyPaneDropdown('color', {
                  label: "Color",
                  options : this.colorOptions
                  // options: [
                  //   {
                  //     index: 0,
                  //     key: "green",
                  //     text: "Green"
                  //   },
                  //   {
                  //     index: 1,
                  //     key: "red",
                  //     text: "Red"
                  //   },
                  //   {
                  //     index: 2,
                  //     key: "yellow",
                  //     text: "Yellow"
                  //   }
                  // ]

                }),
                PropertyPaneSlider("amount", {
                  min: 0,
                  max: 100,
                  label: "Amount",
                  step: 10
                })
              ]

            }
          ]
        }
      ]
    };
  }
}
