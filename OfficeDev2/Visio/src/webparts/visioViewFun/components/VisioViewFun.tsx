import * as React from 'react';
import styles from './VisioViewFun.module.scss';
import { IVisioViewFunProps } from './IVisioViewFunProps';
import { escape } from '@microsoft/sp-lodash-subset';
import 'officejs';
export interface IVisioSampleState {
  selectedShape: Visio.Shape;
}

export default class VisioViewFun extends React.Component<IVisioViewFunProps, {}> {
  
  private _session: OfficeExtension.EmbeddedSession = null;

  public  componentDidMount()
  {
    this.init();
  }

  private async init()
  {
    console.log("test");
    this._session = new OfficeExtension.EmbeddedSession(
     "https://zalolabs.sharepoint.com/:u:/r/sites/DEMO/_layouts/15/doc.aspx?sourcedoc=%7B5816e655-6b2d-4cbb-b4ea-7a4ed2365799%7D&action=default&uid=%7B5816E655-6B2D-4CBB-B4EA-7A4ED2365799%7D&ListItemId=1&ListId=%7B686EA7E1-CF34-43B9-A782-EFCC3C45E545%7D&odsp=1&env=prod", {
        id: "embed-iframe",
        container: document.getElementById("iframeHost"),
        width: "100%",
        height: "600px",
      }
    );
    await this._session.init();
  }
  
  
  public render(): React.ReactElement<IVisioViewFunProps> {


    return (
      <div className={styles.visioSample}>
      <div id='iframeHost' className={styles.iframeHost}></div>
      </div>
    ); 
  }
}
