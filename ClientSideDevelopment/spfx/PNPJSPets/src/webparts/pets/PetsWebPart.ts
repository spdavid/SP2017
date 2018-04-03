import { Version } from '@microsoft/sp-core-library';
import {
  BaseClientSideWebPart,
  IPropertyPaneConfiguration,
  PropertyPaneTextField
} from '@microsoft/sp-webpart-base';
import { escape } from '@microsoft/sp-lodash-subset';

import styles from './PetsWebPart.module.scss';
import * as strings from 'PetsWebPartStrings';
import { sp } from "@pnp/sp";

import PetHelper from './Helpers/PetHelper'



export interface IPetsWebPartProps {
  description: string;
}

export default class PetsWebPart extends BaseClientSideWebPart<IPetsWebPartProps> {

  public onInit(): Promise<void> {
    return super.onInit().then(_ => {
      sp.setup({
        spfxContext: this.context
      });
    });
  }

  public render(): void {
    this.domElement.innerHTML = `
    <input type='Button' id='createList' value='Create Pets List'/>
    <input type='Button' id='displayPets' value='Display Pets'/>
    <input type='Button' id='addField' value='Add Description Field'/>
    <div>
      <input type='text' id='petName' />
      <input type='text' id='petDescription' />
      <input type='Button' id='addPet' value='Add Pet'/>
      
    </div>
    <div id="petsResult"></div>`;

    var createListButton = document.getElementById('createList') as HTMLButtonElement;
    var displayPetsButton = document.getElementById('displayPets') as HTMLButtonElement;
    var addFieldButton = document.getElementById('addField') as HTMLButtonElement;
    var addPetButton = document.getElementById('addPet') as HTMLButtonElement;

    createListButton.onclick = () => { PetHelper.CreatePetsList() };
    displayPetsButton.onclick = () => { this.DisplayPets() };
    addFieldButton.onclick = () => { PetHelper.AddField() };
    addPetButton.onclick = () =>{ this.AddPet()};

  }

  private AddPet()
  {
    var petName = document.getElementById('petName') as HTMLInputElement;
    var petDescription = document.getElementById('petDescription') as HTMLInputElement;
    
    PetHelper.AddPet(petName.value, petDescription.value);

  }

  private DisplayPets() {
    var petsResult = document.getElementById("petsResult") as HTMLDivElement;
    sp.web.lists.getByTitle("Pets").items.select("Title" , "Description").getAll(500).then(data => {
      petsResult.innerHTML = "";
      data.forEach(pet => {
        petsResult.innerHTML += `<div>${pet.Title} : ${pet.Description}</div>`;
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
