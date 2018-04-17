import { IWebPartContext } from '@microsoft/sp-webpart-base';
import { Environment, EnvironmentType } from '@microsoft/sp-core-library';

export interface ITerm {
    id: string,
    name: string
}


export class TaxonomyHelper {

    private _context: IWebPartContext;


    constructor(context: IWebPartContext) {
        this._context = context;
    }



    public GetTermsFromTermSet(termSetId: string): Promise<ITerm[]> {
        if (Environment.type === EnvironmentType.Local) {
            return new Promise((resolve, reject) => {

                var mockterms: ITerm[] = [
                    {
                        id: "123",
                        name: "term1"
                    },
                    {
                        id: "1233",
                        name: "ter2"
                    },
                    {
                        id: "1234",
                        name: "term3"
                    },
                    {
                        id: "1235",
                        name: "term4"
                    }
                ];

                resolve(mockterms)

            });
        }
        else {

            return new Promise((resolve, reject) => {
                this.loadTaxScripts().then(() => {
                    const ctx = SP.ClientContext.get_current();
                    const session = SP.Taxonomy.TaxonomySession.getTaxonomySession(ctx);
                    const store = session.getDefaultKeywordsTermStore();
                    // "549859df-a871-4248-a8f2-49a3bf77951e"
                    var termSetIdGuid = new SP.Guid(termSetId);

                    var ts = store.getTermSet(termSetIdGuid);

                    //var terms = ts.getAllTerms();

                    var term = ts.getTerm(new SP.Guid("78d5cc84-7e29-4f9a-8636-686d819ea4b4"))

                    var terms = term.get_terms();

                    ctx.load(ts);
                    ctx.load(terms, 'Include(IsRoot, Labels, TermsCount, CustomSortOrder, Id, Name, PathOfTerm, Parent, LocalCustomProperties)');

                    ctx.executeQueryAsync(
                        // success
                        () => {
                            var returnTerms: ITerm[] = [];
console.log(terms);
                            for (let i = 0; i < terms.get_count(); i++) {
                                const term: SP.Taxonomy.Term = terms.get_item(i);

                                returnTerms.push({
                                    id: term.get_id().toString(),
                                    name: term.get_name()
                                });

                                resolve(returnTerms);

                            }


                        },
                        // failed
                        (sender, args) => {
                            console.log(args.get_message());
                            reject(args.get_message());
                        }
                    );






                });
            });

        }
    }




    /**
      * Loads needed jsom javascript files needed to access the taxonomy api's
      */
    private loadTaxScripts(): Promise<void> {
        return new Promise<void>((resolve) => {
            let layoutsBase: string = this._context.pageContext.web.absoluteUrl;
            if (layoutsBase.lastIndexOf('/') === layoutsBase.length - 1)
                layoutsBase = layoutsBase.slice(0, -1);
            layoutsBase += '/_layouts/15/';

            this.loadScript(layoutsBase + 'init.js', 'Sod').then(() => {
                return this.loadScript(layoutsBase + 'sp.runtime.js', 'sp_runtime_initialize');
            }).then(() => {
                return this.loadScript(layoutsBase + 'sp.js', 'sp_initialize');
            }).then(() => {
                return this.loadScript(layoutsBase + 'sp.taxonomy.js', 'SP.Taxonomy');
            }).then(() => {
                resolve();
            });
        });
    }

    /**
     * inserts script into header of page
     */
    private loadScript(url: string, globalObjectName: string): Promise<void> {
        return new Promise<void>((resolve) => {
            let isLoaded = true;
            if (globalObjectName.indexOf('.') !== -1) {
                const props = globalObjectName.split('.');
                let currObj: any = window;
                for (let i = 0, len = props.length; i < len; i++) {
                    if (!currObj[props[i]]) {
                        isLoaded = false;
                        break;
                    }
                    currObj = currObj[props[i]];
                }
            }
            else {
                isLoaded = !!window[globalObjectName];
            }
            if (isLoaded || document.head.querySelector('script[src="' + url + '"]')) {
                resolve();
                return;
            }
            const script = document.createElement('script');
            script.type = 'text/javascript';
            script.src = url;
            script.onload = () => {
                resolve();
            };
            document.head.appendChild(script);
        });
    }





}