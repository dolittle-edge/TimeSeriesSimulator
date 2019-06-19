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

    source="SomeSource";
    tag="SomeTag";
    timeSeries="c51ff4fc-13ad-4dae-bc78-e82fa2887847";

    constructor(commandCoordinator) {
        this.#commandCoordinator = commandCoordinator;
        this.publishTagDataPoint = new PublishTagDataPoint();
        this.publishTimeSeriesDataPoint = new PublishTimeSeriesDataPoint();
    }

    async publishTag() {
        this.publishTagDataPoint.source = this.source;
        this.publishTagDataPoint.tag = this.tag;
        let result = await this.#commandCoordinator.handle(this.publishTagDataPoint);
    }

    async publishTimeSeries() {
        this.publishTimeSeriesDataPoint.timeSeries = this.timeSeries;
        let result = await this.#commandCoordinator.handle(this.publishTimeSeriesDataPoint);
    }


    async startTag() {
        let command = new StartTagSimulation();
        command.source = this.source;
        command.tag = this.tag;
        let result = await this.#commandCoordinator.handle(command);
    }

    async startTimeSeries() {
        let command = new StartTimeSeriesSimulation();
        command.timeSeries = this.timeSeries;
        let result = await this.#commandCoordinator.handle(command);
    }

}