import * as React from 'react';
import styles from './ComponentStructure.module.scss';
import { IComponentStructureProps } from './IComponentStructureProps';
import { escape } from '@microsoft/sp-lodash-subset';
import IconWithText, {IconWithText2} from './IconWithText'
import { TextField } from 'office-ui-fabric-react/lib/TextField';
import { autobind } from 'office-ui-fabric-react/lib/Utilities';
import { DefaultButton, IButtonProps } from 'office-ui-fabric-react/lib/Button';

export default class ComponentStructure extends React.Component<IComponentStructureProps, {}> {

   _textfrombox: string;
   

  public render(): React.ReactElement<IComponentStructureProps> {
    return (
      <div>
        <div style={{ float: 'left', padding: 5 }}>
          <IconWithText iconText="icon1" fabricUiName="AddFriend" />
        </div>
        <div style={{ float: 'left', padding: 5 }}>
          <IconWithText iconText="icon2" fabricUiName="Emoji2" />
        </div>
        <div style={{ float: 'left', padding: 5 }}>
          <IconWithText iconText="icon3" fabricUiName="BugSolid" />
        </div>
        <div style={{ clear: 'both', padding: 5 }}>Below</div>

        <TextField onChanged={this.textChanged} label="This is a textbox" required={true} />

        <DefaultButton text="hello click me" onClick={this.buttonClicked} />


      </div>
    );
  }

  @autobind
  public buttonClicked(): void {
    alert("you have entereed" + this._textfrombox);
  }

  @autobind
  public textChanged(val: any): void {
    this._textfrombox = val;
  }

}
