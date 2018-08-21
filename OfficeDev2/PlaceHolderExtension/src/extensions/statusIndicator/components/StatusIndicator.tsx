import { Log } from '@microsoft/sp-core-library';
import { override } from '@microsoft/decorators';
import * as React from 'react';

import styles from './StatusIndicator.module.scss';

export interface IStatusIndicatorProps {
  text: string;
}

const LOG_SOURCE: string = 'StatusIndicator';

export default class StatusIndicator extends React.Component<IStatusIndicatorProps, {}> {
  @override
  public componentDidMount(): void {
    Log.info(LOG_SOURCE, 'React Element: StatusIndicator mounted');
  }

  @override
  public componentWillUnmount(): void {
    Log.info(LOG_SOURCE, 'React Element: StatusIndicator unmounted');
  }

  @override
  public render(): React.ReactElement<{}> {

    let statusIcon = <div></div>;

    switch (this.props.text) {
      case "Good":
        statusIcon = <i style={{color:"green"}} className="ms-Icon ms-Icon--LikeSolid" aria-hidden="true"></i>;
        break;
      case "Ok":
        statusIcon = <i style={{color:"orange"}} className="ms-Icon ms-Icon--MegaphoneSolid" aria-hidden="true"></i>;

        break;
      case "Bad":
        statusIcon = <i style={{color:"red"}} className="ms-Icon ms-Icon--StatusErrorFull" aria-hidden="true"></i>;

        break;

      default:
        break;
    }


    return (
      <div className={styles.cell}>
        {statusIcon}
      </div>
    );
  }
}
