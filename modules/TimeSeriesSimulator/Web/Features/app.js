import { PLATFORM } from 'aurelia-pal';

export class app {
    constructor() {
    }

    configureRouter(config, router) {
        config.options.pushState = true;
        config.map([
            { route: '', name: 'Simulations', moduleId: PLATFORM.moduleName('Simulations/index') }
        ]);

        this.router = router;
    }
}
