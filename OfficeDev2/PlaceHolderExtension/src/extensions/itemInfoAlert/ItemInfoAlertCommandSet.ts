import { override } from '@microsoft/decorators';
import { Log } from '@microsoft/sp-core-library';
import {
  BaseListViewCommandSet,
  Command,
  IListViewCommandSetListViewUpdatedParameters,
  IListViewCommandSetExecuteEventParameters,

} from '@microsoft/sp-listview-extensibility';
import CustomDialog from './CustomDialog';

import * as strings from 'ItemInfoAlertCommandSetStrings';
import { BaseDialog } from '../../../node_modules/@microsoft/sp-dialog';

/**
 * If your command set uses the ClientSideComponentProperties JSON input,
 * it will be deserialized into the BaseExtension.properties object.
 * You can define an interface to describe it.
 */
export interface IItemInfoAlertCommandSetProperties {
  // This is an example; replace with your own properties
  sampleTextOne: string;
  sampleTextTwo: string;
}

const LOG_SOURCE: string = 'ItemInfoAlertCommandSet';

export default class ItemInfoAlertCommandSet extends BaseListViewCommandSet<IItemInfoAlertCommandSetProperties> {

  @override
  public onInit(): Promise<void> {
    Log.info(LOG_SOURCE, 'Initialized ItemInfoAlertCommandSet');
    return Promise.resolve();
  }

  @override
  public onListViewUpdated(event: IListViewCommandSetListViewUpdatedParameters): void {

console.log(this.context.pageContext.list.title);
const compareOneCommand: Command = this.tryGetCommand('ShowSingleItem');
const compareMutliCommand: Command = this.tryGetCommand('ShowMultiItem');

if (this.context.pageContext.list.title != "color")
{
  compareOneCommand.visible = false;
  compareMutliCommand.visible = false;
}
else{
  if (compareOneCommand) {
    // This command should be hidden unless exactly one row is selected.
    compareOneCommand.visible = event.selectedRows.length === 1;
  }
  if (compareMutliCommand) {
    // This command should be hidden unless exactly one row is selected.
    compareMutliCommand.visible = event.selectedRows.length > 1;
  }
}
  
    
  }

  @override
  public onExecute(event: IListViewCommandSetExecuteEventParameters): void {




    let custDialog = new CustomDialog();
    custDialog.selectedItems = event.selectedRows;
    switch (event.itemId) {
      case 'ShowSingleItem':
        custDialog.show();
        break;
      case 'ShowMultiItem':
        custDialog.show();

        break;
      default:
        throw new Error('Unknown command');
    }
  }
}
