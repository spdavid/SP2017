import { Version } from '@microsoft/sp-core-library';
import {
  BaseClientSideWebPart,
  IPropertyPaneConfiguration,
  PropertyPaneTextField
} from '@microsoft/sp-webpart-base';
import { escape } from '@microsoft/sp-lodash-subset';

import styles from './FilterNMapWebPart.module.scss';
import * as strings from 'FilterNMapWebPartStrings';

export interface IFilterNMapWebPartProps {
  description: string;
}

export interface ITerm{
  name : string,
  id : string
}


export default class FilterNMapWebPart extends BaseClientSideWebPart<IFilterNMapWebPartProps> {

  public render(): void {

    console.log(this._mockTerms);

    var termNames = this._mockTerms.Terms.map(
      term => {
        return term.Name;
      });

    console.log(termNames);

    var termObjects : ITerm[] = this._mockTerms.Terms.map(
      term => {
        return { name : term.Name, id : term.Id};
      });

    console.log(termObjects); 

    // if react
    // var output : ITerm[] = this._mockTerms.Terms.map(
    //   term => {
    //     return <div>${term.Name}</div>
    //   });

  var termswithpathdepth2 = this._mockTerms.Terms.filter(t => t.PathDepth === 2);
 console.log(termswithpathdepth2);


 const euros = [29.76, 41.85, 46.5];
 const sum = euros.reduce((total, amount) => total + amount); 


    this.domElement.innerHTML = ``;
  }



  private  _mockTerms = 
  {"_ObjectType_":"SP.Taxonomy.TermSet",
  "_ObjectIdentity_":"a4f45d9e-7003-5000-7d35-b4064108885e|fec14c62-7c3b-481b-851b-c80d7802b224:se:15WaN9o+nUi6qkivmCMKhxA4k0b3ed9BqbnSqve6DjrKW1tjX4wxSIv4oNnV63Xg",
  "Id":"/Guid(635b5bca-8c5f-4831-8bf8-a0d9d5eb75e0)/",
  "Name":"Countries",
  "Description":"",
"Names":{"1033":"Countries"},
"Terms":[
  {
    "_ObjectType_": "SP.Taxonomy.Term",
    "_ObjectIdentity_": "5e06ddd0-d2dd-4fff-bcc0-42b40f4aa59e|4dbeb936-1813-4630-a4bd-9811df3fe7f1:te:generated-id-SPnCDng5nkmdP+UcRJTUTA==",
    "Name": "Belgium",
    "Id": "0ec2f948-3978-499e-9d3f-e51c4494d44c",
    "Description": "",
    "IsDeprecated": false,
    "IsRoot": true,
    "PathOfTerm": "Belgium",
    "PathDepth": 1,
    "TermSet": {
      "_ObjectType_": "SP.Taxonomy.TermSet",
      "_ObjectIdentity_": "5e06ddd0-d2dd-4fff-bcc0-42b40f4aa59e|4dbeb936-1813-4630-a4bd-9811df3fe7f1:se:generated-id-",
      "Id": "\/Guid(5b1b6df0-09a2-42eb-a3f6-006556621931)\/",
      "Name" : "Country"
    }
  }, {
    "_ObjectType_": "SP.Taxonomy.Term",
    "_ObjectIdentity_": "5e06ddd0-d2dd-4fff-bcc0-42b40f4aa59e|4dbeb936-1813-4630-a4bd-9811df3fe7f1:te:generated-id-1a3nKkDuZUOvMhLp9PvKFw==",
    "Id": "2ae7add5-ee40-4365-af32-12e9f4fbca17",
    "Name": "Antwerp",
    "Description": "",
    "IsDeprecated": false,
    "IsRoot": false,
    "PathOfTerm": "Belgium;Antwerp",
    "PathDepth": 2,
    "TermSet": {
      "_ObjectType_": "SP.Taxonomy.TermSet",
      "_ObjectIdentity_": "5e06ddd0-d2dd-4fff-bcc0-42b40f4aa59e|4dbeb936-1813-4630-a4bd-9811df3fe7f1:se:generated-id-",
      "Id": "\/Guid(5b1b6df0-09a2-42eb-a3f6-006556621931)\/",
      "Name" : "Country"
    }
  }, {
    "_ObjectType_": "SP.Taxonomy.Term",
    "_ObjectIdentity_": "5e06ddd0-d2dd-4fff-bcc0-42b40f4aa59e|4dbeb936-1813-4630-a4bd-9811df3fe7f1:te:generated-id-WCbUI7Ims0ysT\u002fBkk4NUhQ==",
    "Name": "Brussels",
    "Id": "23d42658-26b2-4cb3-ac4f-f06493835485",
    "Description": "",
    "IsDeprecated": false,
    "IsRoot": false,
    "PathOfTerm": "Belgium;Brussels",
    "PathDepth": 2,
    "TermSet": {
      "_ObjectType_": "SP.Taxonomy.TermSet",
      "_ObjectIdentity_": "5e06ddd0-d2dd-4fff-bcc0-42b40f4aa59e|4dbeb936-1813-4630-a4bd-9811df3fe7f1:se:generated-id-",
      "Id": "\/Guid(5b1b6df0-09a2-42eb-a3f6-006556621931)\/",
      "Name" : "Country"
    }
  }, {
    "_ObjectType_": "SP.Taxonomy.Term",
    "_ObjectIdentity_": "5e06ddd0-d2dd-4fff-bcc0-42b40f4aa59e|4dbeb936-1813-4630-a4bd-9811df3fe7f1:te:generated-id-WCbUI7Ims0ysT\u002fBkk4NUhQ==",
    "Name": "Deprecated",
    "Id": "23d42658-26b2-4cb3-ac4f-f06493835486",
    "Description": "",
    "IsDeprecated": true,
    "IsRoot": true,
    "PathOfTerm": "Deprecated",
    "PathDepth": 1,
    "TermSet": {
      "_ObjectType_": "SP.Taxonomy.TermSet",
      "_ObjectIdentity_": "5e06ddd0-d2dd-4fff-bcc0-42b40f4aa59e|4dbeb936-1813-4630-a4bd-9811df3fe7f1:se:generated-id-",
      "Id": "\/Guid(5b1b6df0-09a2-42eb-a3f6-006556621931)\/",
      "Name" : "Country"
    }
  }]};

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
