/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
import { CommandCoordinator } from '@dolittle/commands'
import { inject } from 'aurelia-framework';
import { PublishDataPoint } from './PublishDataPoint';
import { StartSimulation } from './StartSimulation';

@inject(CommandCoordinator)
export class index {
    #commandCoordinator;

    constructor(commandCoordinator) {
        this.#commandCoordinator = commandCoordinator;
        this.publishDataPoint = new PublishDataPoint();
        this.startSimulation = new StartSimulation();
    }
}