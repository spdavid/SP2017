import * as React from 'react';
import styles from './Callsecuredfunction.module.scss';
import { ICallsecuredfunctionProps } from './ICallsecuredfunctionProps';
import { escape } from '@microsoft/sp-lodash-subset';
import { AadHttpClient, AadHttpClientConfiguration } from '@microsoft/sp-http';

export interface ICallsecuredfunctionState {
  isReady?: boolean;
}


export default class Callsecuredfunction extends React.Component<ICallsecuredfunctionProps, ICallsecuredfunctionState> {

  private _data : any;

  constructor(props) {
    super(props);

    this.state = { isReady: false };
  }


  public componentWillMount() {
    var client = new AadHttpClient(this.props.context.serviceScope, "1c149e0d-91a9-452b-8782-2125c7db70cf")
      .get("https://davidscoolfunctions.azurewebsites.net/api/SecuredAPI", AadHttpClient.configurations.v1)
      .then(result => {
        if (result.ok) {
          result.json().then(data => {
            console.log(data);
            this._data = data;
            this.setState({isReady : true});
          });
        }

      });
  }

  public render(): React.ReactElement<ICallsecuredfunctionProps> {
    if (this.state.isReady) {
      return (
        <div>
          Hello {this._data.developer} from {this._data.location}
        </div>
      );
    }
    else {
      return (
      <div></div>
      );
    }
  }
}
