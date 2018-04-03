import { SPHttpClient, SPHttpClientResponse, ISPHttpClientOptions } from '@microsoft/sp-http';

import {
    WebPartContext
} from '@microsoft/sp-webpart-base';

import { sp }from '@pnp/sp'

export default class ListCrudHelper {
    private Context: WebPartContext;
    private WebUrl: string;

    constructor(ctx: WebPartContext) {
        this.Context = ctx;
        this.WebUrl = ctx.pageContext.web.absoluteUrl;
    }

public getLists()
{
    sp.web.lists.getByTitle("AnewTitle").items.get().then(
        listitems => {
            console.log(listitems);
        }
    );
    sp.web.lists.select('Title').filter("Hidden eq false").get().then
        (data => {
            console.log(data);
            
        });

}


    public CreateList(){
        
            sp.web.lists.add("aTestlist2", "desc", 100).then(result => {
                    console.log(result);
            });
    }
    // public CreateList() {
    //     var body = {
    //         'BaseTemplate': 100,
    //         'Title': 'atest list'
    //     };
    //     var url = this.WebUrl + "/_api/web/lists";

    //     var options: ISPHttpClientOptions = { body: JSON.stringify(body) };

    //     this.Context.spHttpClient.post(url, SPHttpClient.configurations.v1, options)
    //         .then(response => {
    //             if (response.ok)
    //             {
    //                 response.json()
    //                 .then(data => {
    //                     console.log(data);
    //                 })
    //             }
    //             else
    //             {
    //                 console.log("failed");
    //                 console.log(response);
    //             }


    //         });

    // }


    public DeleteList() {


        sp.web.lists.getByTitle("aTestlist2").delete().then(result => {
            console.log(result);
    });
        // var url = this.WebUrl + "/_api/web/lists/getbytitle('atest list')";

        // var options: ISPHttpClientOptions = { headers: {
        //     'Accept': 'application/json;odata=nometadata',
        //     'Content-type': 'application/json;odata=verbose',
        //     'odata-version': '',
        //     'IF-MATCH': "*",
        //     'X-HTTP-Method': 'DELETE'} };

        // this.Context.spHttpClient.post(url, SPHttpClient.configurations.v1, options)
        //     .then(response => {
        //         if (response.ok)
        //         {
        //             console.log("list deleted");
        //             // response.json()
        //             // .then(data => {
        //             //     console.log(data);
        //             // })
        //         }
        //         else
        //         {
        //             console.log("failed");
        //             console.log(response);
        //         }


        //     });

    }

    public UpdateList() {

        sp.web.lists.getByTitle("aTestlist2").update({Title: "anewTitle"}).then(result => {
            console.log(result);
    });

        // var body = {
        //     'Title': 'atest list 22'
        // };
        // var url = this.WebUrl + "/_api/web/lists/getbytitle('atest list')";

        // var options: ISPHttpClientOptions = { body: JSON.stringify(body),
        //     headers: {
        //     'Accept': 'application/json;odata=nometadata',
        //     'Content-type': 'application/json;odata=verbose',
        //     'odata-version': '',
        //     'IF-MATCH': "*",
        //     'X-HTTP-Method': 'MERGE'} };

        // this.Context.spHttpClient.post(url, SPHttpClient.configurations.v1, options)
        //     .then(response => {
        //         if (response.ok)
        //         {
        //             response.json()
        //             .then(data => {
        //                 console.log(data);
        //             })
        //         }
        //         else
        //         {
        //             console.log("failed");
        //             console.log(response);
        //         }


        //     });
    }


}