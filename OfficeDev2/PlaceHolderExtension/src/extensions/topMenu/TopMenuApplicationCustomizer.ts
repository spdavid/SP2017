import * as React from 'react';
import * as ReactDom from 'react-dom';

import { override } from '@microsoft/decorators';
import { Log } from '@microsoft/sp-core-library';
import {
  BaseApplicationCustomizer,
  PlaceholderContent,
  PlaceholderName
} from '@microsoft/sp-application-base';
import { Dialog } from '@microsoft/sp-dialog';

import { sp } from "@pnp/sp";
//import styles from './TopMenuStyles.Module.scss';
import { escape } from '@microsoft/sp-lodash-subset';

import TopMenu, {ITopMenuProps} from './components/TopMenu'

import * as strings from 'TopMenuApplicationCustomizerStrings';

const LOG_SOURCE: string = 'TopMenuApplicationCustomizer';

/**
 * If your command set uses the ClientSideComponentProperties JSON input,
 * it will be deserialized into the BaseExtension.properties object.
 * You can define an interface to describe it.
 */
export interface ITopMenuApplicationCustomizerProperties {
  // This is an example; replace with your own property
  TermSetId: string;
}

/** A Custom Action which can be run during execution of a Client Side Application */
export default class TopMenuApplicationCustomizer
  extends BaseApplicationCustomizer<ITopMenuApplicationCustomizerProperties> {

  private _topPlaceholder: PlaceholderContent | undefined;

  @override
  public onInit(): Promise<void> {
    Log.info(LOG_SOURCE, `Initialized ${strings.Title}`);

    return super.onInit().then(_ => {
      // other init code may be present
      sp.setup({
        spfxContext: this.context
      });
       // Added to handle possible changes on the existence of placeholders.
    this.context.placeholderProvider.changedEvent.add(this, this._renderPlaceHolders);

    // Call render method for generating the HTML elements.
    this._renderPlaceHolders();
    });

  }

  private _renderPlaceHolders() {

    this.context.placeholderProvider.placeholderNames.map(name => PlaceholderName[name]).join(', ');

    // Handling the top placeholder
    if (!this._topPlaceholder) {
      this._topPlaceholder =
        this.context.placeholderProvider.tryCreateContent(
          PlaceholderName.Top);

      // The extension should not assume that the expected placeholder is available.
      if (!this._topPlaceholder) {
        console.error('The expected placeholder (Top) was not found.');
        return;
      }


      if (this.properties) {
        let termSetId: string = this.properties.TermSetId;
        if (!termSetId) {
          termSetId = '(TermSetId property was not defined.)';
        }

        if (this._topPlaceholder.domElement) {
         
          const element: React.ReactElement<ITopMenuProps> = React.createElement(
            TopMenu,
            {
              terSetId : this.properties.TermSetId
            }
          );
  
          ReactDom.render(element, this._topPlaceholder.domElement);

        }
      }
    }


  }

}
