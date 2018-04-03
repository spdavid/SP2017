import { sp } from "@pnp/sp";

export default class PetHelper
{
    public static CreatePetsList() {
        sp.web.lists.add("Pets", "", 100)
          .then(result => {
            alert("list is created");
          })
          .catch(error => {
            alert("list creation failed");
            console.log(error);
          });
      }

      public static AddField() {
        sp.web.lists.getByTitle("Pets").fields.addMultilineText("Description", 4, false);
      }

      
  public static AddPet(name: string, description : string)
  {
   
    var newItem = {
      Title : name,
      Description : description
    };

    sp.web.lists.getByTitle("Pets").items.add(newItem).then(result => {
      alert("pet added");
    });

  }
}