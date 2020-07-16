import { FunctionalComponent, h } from "preact";
import {
    Route,
    Router,
    RouterOnChangeArgs,
    CustomHistory
} from "preact-router";
import { createHashHistory } from "history";
import { Fabric } from '@fluentui/react';

import Home from "../routes/home";
import NotFoundPage from "../routes/notfound";
import Header from "./header";

// eslint-disable-next-line @typescript-eslint/no-explicit-any
if ((module as any).hot) {
    // tslint:disable-next-line:no-var-requires
    require("preact/debug");
}

const App: FunctionalComponent = () => {
    let currentUrl: string;
    const handleRoute = (e: RouterOnChangeArgs) => {
        currentUrl = e.url;
    };

    const history: CustomHistory = createHashHistory() as any;

    return (
        <Fabric id="app">
            <Header />
            <Router history={history} onChange={handleRoute}>
                <Route path="/" component={Home} />
                <NotFoundPage default />
            </Router>
        </Fabric>
    );
};

export default App;
