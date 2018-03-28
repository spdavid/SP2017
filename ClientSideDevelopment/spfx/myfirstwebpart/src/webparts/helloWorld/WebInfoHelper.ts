import {
    SPHttpClient,
    SPHttpClientResponse
} from '@microsoft/sp-http';

import {
    WebPartContext
} from '@microsoft/sp-webpart-base';

export default class WebInfoHelper {
    public static GetWebData(ctx: WebPartContext): Promise<any> {
        return new Promise((resolve, reject) => {
            var url = ctx.pageContext.web.absoluteUrl + "/_api/web";
            ctx.spHttpClient.get(url, SPHttpClient.configurations.v1).then(
                (response) => {
                    if (response.ok) {
                        response.json().then((data) => {
                            console.log(data);
                            resolve(data);
                        });

                    }
                }

            );

        });

    }


}