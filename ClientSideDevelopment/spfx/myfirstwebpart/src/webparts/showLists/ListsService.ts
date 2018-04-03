import {
    SPHttpClient,
    SPHttpClientResponse
} from '@microsoft/sp-http';

import {
    WebPartContext
} from '@microsoft/sp-webpart-base';

export interface IListData{
    Title: string,
    Url : string
}


export  class ListsService
{
    public static GetListsFromSharePoint(ctx : WebPartContext) : Promise<any>
    {
            return new Promise((resolve, reject) => {
                var siteUrl = ctx.pageContext.web.absoluteUrl;

                var url = siteUrl + "/_api/web/lists?$select=Title&$filter=Hidden%20eq%20false&$orderby=Title%20desc"

                ctx.spHttpClient.get(url,SPHttpClient.configurations.v1 )
                    .then(response => {
                         if (response.ok)
                         {
                            response.json().then(
                                data => {
                                    console.log(data.value);
                                    resolve(data.value);
                                }
                            );
                         }   
                         else
                         {
                                reject(response.statusMessage);

                         }

                    });

            });

    }

    public static GetDefaultUrlFromList(ctx : WebPartContext, listTitle : string) : Promise<IListData>
    {
            return new Promise((resolve, reject) => {
                var siteUrl = ctx.pageContext.web.absoluteUrl;

                var url = siteUrl + `/_api/web/lists/getbytitle('${listTitle}')/defaultview?$select=ServerRelativeUrl`;

                ctx.spHttpClient.get(url,SPHttpClient.configurations.v1 )
                    .then(response => {
                         if (response.ok)
                         {
                            response.json().then(
                                data => {
                                    console.log(data);

                                    var returnData : IListData = { Title :listTitle , Url : data.ServerRelativeUrl};
                                    resolve(returnData);
                                }
                            );
                         }   
                         else
                         {
                                reject(response.statusMessage);

                         }

                    });

            });

    }
}