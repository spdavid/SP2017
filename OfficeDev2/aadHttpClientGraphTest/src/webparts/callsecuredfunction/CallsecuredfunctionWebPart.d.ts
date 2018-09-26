import { Version } from '@microsoft/sp-core-library';
import { BaseClientSideWebPart, IPropertyPaneConfiguration } from '@microsoft/sp-webpart-base';
export interface ICallsecuredfunctionWebPartProps {
    description: string;
}
export default class CallsecuredfunctionWebPart extends BaseClientSideWebPart<ICallsecuredfunctionWebPartProps> {
    render(): void;
    protected onDispose(): void;
    protected readonly dataVersion: Version;
    protected getPropertyPaneConfiguration(): IPropertyPaneConfiguration;
}
