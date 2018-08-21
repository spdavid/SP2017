import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { BaseDialog, IDialogConfiguration } from '@microsoft/sp-dialog';
import {
  RowAccessor
    
  } from '@microsoft/sp-listview-extensibility';
import ItemViewer from './components/ItemViewer'

export default class CustomDialog extends BaseDialog {
    
  public selectedItems : ReadonlyArray<RowAccessor>;

   public render(): void {

    const element: React.ReactElement<ItemViewer> = React.createElement(
        ItemViewer,
        {
         selectedItems : this.selectedItems
        }
      );

      ReactDOM.render(element, this.domElement);
   }
 
   public getConfig(): IDialogConfiguration {
     return {
       isBlocking: false
     };
   }
 
 }