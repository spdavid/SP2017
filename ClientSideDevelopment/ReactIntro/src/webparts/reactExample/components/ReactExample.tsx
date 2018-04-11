import * as React from 'react';
import styles from './ReactExample.module.scss';
import { IReactExampleProps } from './IReactExampleProps';
import { escape } from '@microsoft/sp-lodash-subset';
import { sp } from "@pnp/sp";
import { Dropdown, IDropdown, DropdownMenuItemType, IDropdownOption } from 'office-ui-fabric-react/lib/Dropdown';
import ListItems from './ListItems'
import { autobind } from 'office-ui-fabric-react/lib/Utilities';


export interface IReactExampleState {
  isReady: boolean;
  currentListId: string;
}

export default class ReactExample extends React.Component<IReactExampleProps, IReactExampleState> {

  private options: IDropdownOption[];

  constructor(props: any) {
    super(props);

    this.options = [];

    this.state = {
      isReady: false,
      currentListId : ""
    };

   // this.DropDownChanged= this.DropDownChanged.bind(this);

  }

  public componentWillMount() {
    sp.web.lists.select("Id", "Title").filter("Hidden eq false").get().then(
      (data : Array<any>) => {
        console.log(data);
        data.forEach(list => {
          this.options.push(
            {
              key : list.Id,
              text : list.Title
            }
          );
        });
        this.setState({isReady: true});
      }
    );
  }

  public render(): React.ReactElement<IReactExampleProps> {
    console.log("render");
    if (!this.state.isReady) {
      return <div></div>;
    }
    else {
      return (
        <div style={{ width: 300 }}>
          <Dropdown label="Select List" options={this.options} onChanged={this.DropDownChanged} />
        
          <ListItems listId={this.state.currentListId} userClickedEvent={this.ListItemsItemClicked} />
        </div>
      );
    }
  }

  @autobind
  private DropDownChanged(option) {
    this.setState({currentListId: option.key});
  }

  private ListItemsItemClicked(itemtext)
  {
      console.log("the user cliced on " + itemtext);
  }


}
