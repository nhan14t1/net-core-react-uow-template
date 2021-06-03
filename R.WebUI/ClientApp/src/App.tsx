import React, { Component } from 'react';
// import { BrowserRouter } from 'react-router-dom';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';

import './custom.scss'
import { Login } from './components/account/login';
import { Register } from './components/account/register';

export default class App extends Component {
	static displayName = App.name;

	render() {
		return (
			<>
				<Route exact path='/' >
					<Layout></Layout><Home></Home>
				</Route>
				<Route path='/counter' >
					<Layout><Counter></Counter></Layout>
				</Route>
				<Route path='/fetch-data'>
					<Layout><FetchData></FetchData></Layout>
				</Route>
				<Route path='/login' component={Login} />
				<Route path='/register' component={Register} />
			</>
		);
	}
}
