import * as React from 'react';
import styles from './GettingTaxonomy.module.scss';
import { IGettingTaxonomyProps } from './IGettingTaxonomyProps';
import { escape } from '@microsoft/sp-lodash-subset';
import {TaxonomyHelper, ITerm} from '../../../Common/TaxonomyHelper'

export interface IGettingTaxonomyState {
  isReady: boolean;
}

export default class GettingTaxonomy extends React.Component<IGettingTaxonomyProps, IGettingTaxonomyState> {

  private _terms : ITerm[];


  constructor(props) {
    super(props);
    this.state = { isReady: false };
  }

  public componentWillMount() {
    var taxHelper = new TaxonomyHelper(this.props.context);
    taxHelper.GetTermsFromTermSet("635b5bca-8c5f-4831-8bf8-a0d9d5eb75e0").then(terms => {
        this._terms = terms;
        this.setState({isReady : true});
    });

  }

  public render(): React.ReactElement<IGettingTaxonomyProps> {
    if (this.state.isReady)
    {
      
      var termsOutput = this._terms.map(term => {
        return <div>{term.name}</div>
      });

      
      return (
        <div>{termsOutput}</div>
      );
    }
    else
    {
        return (
      <div></div>
    );
    }
  
  }
}
