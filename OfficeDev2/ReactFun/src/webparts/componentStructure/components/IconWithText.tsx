import * as React from 'react';
import styles from './ComponentStructure.module.scss';
import { IComponentStructureProps } from './IComponentStructureProps';
import { escape } from '@microsoft/sp-lodash-subset';

export interface IIconWithText {
  fabricUiName : string;
  iconText : string;
}

export default class IconWithText extends React.Component<IIconWithText, {}> {
  public render(): React.ReactElement<IIconWithText> {
    return (
    <div>
      <i className={"ms-Icon ms-Icon--" + this.props.fabricUiName} aria-hidden="true"></i>
      <div>{this.props.iconText}</div>
    </div>
    );
  }
}


export const IconWithText2 = (props :IIconWithText) => { 
  return (
    <div>
      <i className={"ms-Icon ms-Icon--" + this.props.fabricUiName} aria-hidden="true"></i>
      <div>{this.props.iconText}</div>
    </div>
    );
 }
