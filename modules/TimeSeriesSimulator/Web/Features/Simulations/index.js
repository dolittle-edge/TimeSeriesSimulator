/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
import { CommandCoordinator } from '@dolittle/commands'
import { inject } from 'aurelia-framework';
import { PublishTagDataPoint } from './PublishTagDataPoint';
import { StartTagSimulation } from './StartTagSimulation';
import { PublishTimeSeriesDataPoint } from './PublishTimeSeriesDataPoint';
import { StartTimeSeriesSimulation } from './StartTimeSeriesSimulation';


@inject(CommandCoordinator)
export class index {
    #commandCoordinator;

    system="SomeSystem";
    tag="SomeTag";
    timeSeries="74265f94-f01e-4769-aee9-04fc369ae338";

    constructor(commandCoordinator) {
        this.#commandCoordinator = commandCoordinator;
        this.publishTagDataPoint = new PublishTagDataPoint();
        this.publishTimeSeriesDataPoint = new PublishTimeSeriesDataPoint();
    }

    async publishTag() {
        this.publishTagDataPoint.system = this.system;
        this.publishTagDataPoint.tag = this.tag;
        let result = await this.#commandCoordinator.handle(this.publishTagDataPoint);
    }

    async publishTimeSeries() {
        this.publishTimeSeriesDataPoint.timeSeries = this.timeSeries;
        let result = await this.#commandCoordinator.handle(this.publishTimeSeriesDataPoint);
    }


    async startTag() {
        let command = new StartTagSimulation();
        command.system = this.system;
        command.tag = this.tag;
        let result = await this.#commandCoordinator.handle(command);
    }

    async startTimeSeries() {
        let command = new StartTimeSeriesSimulation();
        command.timeSeries = this.timeSeries;
        let result = await this.#commandCoordinator.handle(command);
    }

}