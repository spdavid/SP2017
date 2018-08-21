
import * as React from 'react';

import { escape } from '@microsoft/sp-lodash-subset';
import {
  RowAccessor
    
  } from '@microsoft/sp-listview-extensibility';
import { sp } from "@pnp/sp";
import { taxonomy, ITermData, ITerm } from "@pnp/sp-taxonomy";
import { CommandBar, ICommandBarProps, ICommandBarItemProps } from 'office-ui-fabric-react/lib/CommandBar';


export interface IItemViewerProps {
    selectedItems : ReadonlyArray<RowAccessor> 
}




export default class ItemViewer extends React.Component<IItemViewerProps, {}> {

    public constructor(props) {
        super();
    
    }

    public componentWillMount() {

    }

    public render(): React.ReactElement<IItemViewerProps> {
        return (
            <div>
                The colors you have picked are:
               {this.props.selectedItems.map(item => {
                   return <div>{item.getValueByName("Title")}</div>
               })}
      </div>
        );
    }
}
