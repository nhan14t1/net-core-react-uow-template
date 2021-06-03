import React from 'react';
import { UserModel } from '../../shared/models/user-model';
import { UserService } from '../../services/user.service';
import styles from './account.module.scss';
import { HeadProvider, Title, Link, Meta } from 'react-head';

type LoginStage = {
    user: UserModel
}

export class Login extends React.Component<{}, LoginStage> {
    userService: UserService;

    constructor(props: {}) {
        super(props);

        this.state = {
            user: new UserModel()
        }

        this.userService = new UserService();
    }

    onLogin = () => {
        var user = this.state.user;
        if (!user.userName || !user.password) {
            return;
        }

        const returnUrl = new URLSearchParams(document.location.search).get("returnurl") || '/';

        this.userService.login(user.userName, user.password)
        .then(() => {
            location.href = returnUrl;
        });
    }

    onUserNameChanged = (e: any) => {
        this.state.user.userName = e.target.value;
    }

    onPasswordChanged = (e: any) => {
        this.state.user.password = e.target.value;
    }

    render() {
        return (
            <div className={styles['account-page']}>
                <HeadProvider>
                    <Title>Login</Title>
                    <Meta name="viewport" content="initial-scale=1.0, width=device-width" />
                    <Link rel="stylesheet" href="/libs/account/account.css"/>

                </HeadProvider>

                <div className="login-page">
                    <div className="form">
                        <div className="login-form">
                            <input type="text" placeholder="username" onChange={(e) => this.onUserNameChanged(e)}/>
                            <input type="password" placeholder="password" onChange={(e) => this.onPasswordChanged(e)}/>
                            <button onClick={() => this.onLogin()}>login</button>
                            <p className="message">Not registered? <a href="/register">Create an account</a></p>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}