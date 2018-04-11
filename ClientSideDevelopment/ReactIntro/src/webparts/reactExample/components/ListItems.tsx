import * as React from 'react';
import { sp } from "@pnp/sp";
import { autobind } from 'office-ui-fabric-react/lib/Utilities';
export interface IListItemsProps
{
    listId : string
    userClickedEvent : (itemClicked) => void;
}
export interface IListItemsState
{
    itemTitles : string[];
}


export default class ListItems extends React.Component<IListItemsProps, IListItemsState> {

    constructor(props: any) {
        super(props);
    
        this.state = {itemTitles : []};
    }

    public componentWillReceiveProps(nextProps : IListItemsProps)
    {
           var listId = nextProps.listId;
           sp.web.lists.getById(listId).items.select("Title").getAll().then(
               (data: Array<any>) => {
                   // make new empty array
                   var newTitles : string[] = [];
                   // fill array
                    data.forEach(item => {
                        newTitles.push(item.Title);
                    });
                    // update state to trigger a re render
                    this.setState({itemTitles : newTitles});
               }
           );
    }

    public render() {
       
        if (this.props.listId == "")
        {
            return <div></div>

        }
        else
        {
            var titles = [];
            this.state.itemTitles.forEach(itemTitle => {
                titles.push(<div onClick={this.ItemClicked}>{itemTitle}</div>);
            });

          
            return <div>{titles}</div>

        }
    }

    @autobind 
    private ItemClicked(event)
    {
        var theDiv = event.target as HTMLDivElement;
        console.log(theDiv.innerText);

        this.props.userClickedEvent(theDiv.innerText);
    }

}