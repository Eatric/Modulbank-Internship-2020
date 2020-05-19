import ReactDOM from 'react-dom';
import React from 'react';

import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';

import Auth from './components/SignIn/SignIn.js';
import NotFound from './components/NotFound/NotFound.js';
import Register from './components/SignUp/SignUp.js';

ReactDOM.render(
    <Router>
        <Switch>
            <Route exact path="/signin" component={Auth} />
            <Route exact path="/signup" component={Register} />
            <Route component={NotFound} />
        </Switch>
    </Router>,
    document.getElementById("root")
)