import * as React from 'react';
import styles from './RecentSites.module.scss';
import { IRecentSitesProps } from './IRecentSitesProps';
import { escape, uniqBy } from '@microsoft/sp-lodash-subset';
import { MSGraphClient } from "@microsoft/sp-http";
import { Link } from 'office-ui-fabric-react/lib/components/Link';
import { Spinner, SpinnerSize } from 'office-ui-fabric-react/lib/components/Spinner';


export interface IRecentSitesProps
{

}


export interface IRecentlyVisitedSitesState {
  usedSites: IWebs[];
  error: string;
  loading: boolean;
}

export interface IRecentWebs {
  '@odata.context': string;
  value: IRecentWeb[];
}

export interface IRecentWeb {
  id: string;
  lastUsed: LastUsed;
  resourceVisualization: ResourceVisualization;
  resourceReference: ResourceReference;
}

export interface ResourceReference {
}

export interface ResourceVisualization {
  title: string;
  type: string;
  mediaType: string;
  previewImageUrl: string;
  previewText: string;
  containerWebUrl: string;
  containerDisplayName: string;
}

export interface LastUsed {
  lastAccessedDateTime: string;
  lastModifiedDateTime: string;
}

export interface IWebs {
  id: string;
  title: string;
  path: string;
}


export default class RecentSites extends React.Component<IRecentSitesProps, IRecentlyVisitedSitesState> {

  private _graphClient: MSGraphClient = null;

  /**
   * Constructor
   * @param props
   */
  constructor(props: IRecentSitesProps) {
    super(props);

    // this._graphClient = this.props.context.serviceScope.consume(
    //   MSGraphClient.serviceKey
    // );

    this.state = {
      usedSites: [],
      error: null,
      loading: true
    };
  }

  /**
   * Fetch the recent sites via the Microsoft Graph client
   */
  private _fetchRecentSites() {
  
      this.setState({
        loading: true
      });
      // Calling: beta/me/insights/used?$filter=ResourceVisualization/Type eq 'Web'

      console.log("are you working");
console.log(  this.props.context.msGraphClientFactory);
      this.props.context.msGraphClientFactory
      .getClient()
      .then((client: MSGraphClient): void => {
        // get information about the current user from the Microsoft Graph
        client
          .api("me/insights/used")
          .version("beta")
          .filter(`ResourceVisualization/Type eq 'Web'`)
          .get((err, response: any, res?: any) => {
            console.log(err);
            console.log(res);
            if (err) {
              // Something failed calling the MS Graph
              this.setState({
                error: err.message,
                usedSites: [],
                loading: false
              });
              return;
            }
    
            // Check if a response was retrieved
            if (res && res.value && res.value.length > 0) {
              this._processRecentSites(res.value);
            } else {
              // No sites retrieved
              this.setState({
                loading: false,
                usedSites: []
              });
            }
        }).catch(err => {
          console.log(err);
        });
      });


      // this._graphClient
      // .api("me/insights/used")
      // .version("beta") // API is currently only available in BETA
      // .filter(`ResourceVisualization/Type eq 'Web'`)
      // .top(30)
      // .get((err, res: IRecentWebs) => {
       
      // });
    
  }

  /**
   * Processes the retrieved web results from the Microsoft Graph
   * @param recentWebs
   */
  private _processRecentSites(recentWebs: IRecentWeb[]): void {
    // Map the MS Graph result
    const rWebs: IWebs[] = recentWebs.map(w => {
      return {
        id: w.id,
        title: w.resourceVisualization.containerDisplayName,
        path: this._updateSitePath(w.resourceVisualization.containerWebUrl)
      };
    });

    // Only retrieve the unique sites
    const uWeb = uniqBy(rWebs, 'path');

    // Get the latest 10 results
    this.setState({
      usedSites: uWeb.slice(0, 10),
      loading: false
    });
  }

  /**
   * Parse the retrieve URLs to return the site collection URLs
   * @param path
   */
  private _updateSitePath(path: string): string {
    if (path) {
      // Split the site on the sites path
      const pathSplit = path.split("/sites/");
      if (pathSplit.length === 2) {
        const siteUrlPath = pathSplit[1].substring(0, pathSplit[1].indexOf("/"));
        // Concatinate the URL
        return `${pathSplit[0]}/sites/${siteUrlPath}`;
      } else {
        // Return the root site
        const matches = path.match(/^https?\:\/\/([^\/?#]+)(?:[\/?#]|$)/i);
        if (matches && matches.length > 0) {
          return matches[0];
        }
      }
    }
    return path;
  }

  /**
   * componentDidMount lifecycle hook
   */
  public componentDidMount(): void {
    console.log("didmount");
    this._fetchRecentSites();
  }

  /**
   * Default React render method
   */
  public render(): React.ReactElement<IRecentSitesProps> {
    return (
      <div className={ styles.recentlyVisitedSites }>

        {
          this.state.loading && <Spinner label="loading" size={SpinnerSize.large} />
        }

        {
          this.state.usedSites && this.state.usedSites.length > 0 ? (
            <div className={styles.list}>
              <ul>
                {
                  this.state.usedSites.map(site => (
                    <li key={site.id} className={styles.site}>
                      <Link href={site.path} title={site.title}>{site.title}</Link>
                    </li>
                  ))
                }
              </ul>
            </div>
          ) : (
            !this.state.loading && (
              this.state.error ?
                <span className={styles.error}>{this.state.error}</span> :
                <span className={styles.noSites}>no sites</span>
            )
          )
        }
      </div>
    );
  }
}