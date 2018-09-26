/// <reference types="react" />
import * as React from 'react';
import { ICallsecuredfunctionProps } from './ICallsecuredfunctionProps';
export interface ICallsecuredfunctionState {
    isReady?: boolean;
}
export default class Callsecuredfunction extends React.Component<ICallsecuredfunctionProps, ICallsecuredfunctionState> {
    private _data;
    constructor(props: any);
    componentWillMount(): void;
    render(): React.ReactElement<ICallsecuredfunctionProps>;
}
