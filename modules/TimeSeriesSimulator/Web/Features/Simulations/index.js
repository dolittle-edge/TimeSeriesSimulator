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

    system="SomeSystem";
    tag="SomeTag";

    constructor(commandCoordinator) {
        this.#commandCoordinator = commandCoordinator;
        this.publishDataPoint = new PublishDataPoint();
    }

    async publish() {
        this.publishDataPoint.system = this.system;
        this.publishDataPoint.tag = this.tag;
        let result = await this.#commandCoordinator.handle(this.publishDataPoint);
    }

    async start() {
        let command = new StartSimulation();
        command.system = this.system;
        command.tag = this.tag;
        let result = await this.#commandCoordinator.handle(command);
    }
}