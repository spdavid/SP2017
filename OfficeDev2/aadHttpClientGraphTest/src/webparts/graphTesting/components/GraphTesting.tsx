import * as React from 'react';
import styles from './GraphTesting.module.scss';
import { IGraphTestingProps } from './IGraphTestingProps';
import { escape } from '@microsoft/sp-lodash-subset';
import { MSGraphClient } from '@microsoft/sp-client-preview';

export interface ITask
{
  title :string, 
  date : Date
}

export interface IGraphTestingState
{
  isready: boolean;
}

export default class GraphTesting extends React.Component<IGraphTestingProps,IGraphTestingState> {



private _tasks : ITask[];

  constructor(props)
  {
    super();

    this.state = {isready :false};
  }


public componentWillMount()
{
  var graphClient = this.props.context.serviceScope.consume(
    MSGraphClient.serviceKey
  );

graphClient
  .api("me/planner/tasks")
  .version("v1.0")
  .get((err, res) => {  

    if (err) {
      console.error(err);
      return;
    }
    console.log(res.value);
    this._tasks = res.value.map(val => {
      return { title : val.title, date: new Date(val.createdDateTime)}
    });

    this.setState({isready:true});
  });

  // graphClient.get("https://graph.microsoft.com/v1.0/me/planner/tasks", GraphHttpClient.configurations.v1)
  //   .then(request => {
  //     if (request.ok)
  //     {
  //       request.json().then(data => {console.log(data)});
  //     }
  //     else
  //     {
  //       console.log(request.statusText);
  //     }

  //   })

}


  public render(): React.ReactElement<IGraphTestingProps> {
    
    return (
      <div >
        {this.state.isready &&
            this._tasks.map(task => {
              return <div>{task.title} : {task.date.toLocaleDateString()}</div>
            })
          }
            
      </div>
    );
  }
}
