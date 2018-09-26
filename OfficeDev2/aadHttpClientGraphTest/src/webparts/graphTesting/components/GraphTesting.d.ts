/// <reference types="react" />
import * as React from 'react';
import { IGraphTestingProps } from './IGraphTestingProps';
export interface ITask {
    title: string;
    date: Date;
}
export interface IGraphTestingState {
    isready: boolean;
}
export default class GraphTesting extends React.Component<IGraphTestingProps, IGraphTestingState> {
    private _tasks;
    constructor(props: any);
    componentWillMount(): void;
    render(): React.ReactElement<IGraphTestingProps>;
}
