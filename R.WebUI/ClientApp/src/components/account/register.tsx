import React from 'react';
import { UserModel } from '../../shared/models/user-model';
import { HeadProvider, Title, Link, Meta } from 'react-head';
import { UserService } from '../../services/user.service';
import styles from './account.module.scss';
import { DatePicker, FormInstance } from 'antd';
import {
    Form,
    Input,
    Select,
    Button,
} from 'antd';

const { Option } = Select;

type State = {
    user: UserModel
}

export class Register extends React.Component<{}, State> {
    userService: UserService;
    formRef: FormInstance;

    constructor(props: {}) {
        super(props);

        this.state = {
            user: new UserModel()
        }

        this.userService = new UserService();
    }

    onRegister = () => {
        var self = this;

        this.formRef.validateFields().finally(() => {
            if (self.formRef.getFieldsError().findIndex(_ => _.errors.length > 0) < 0) {
                var user = self.state.user;

                const returnUrl = new URLSearchParams(document.location.search).get("returnurl") || '/';

                self.userService.register(user).then(() => {
                    location.href = returnUrl;
                });
            }
        });
    }

    onUserNameChanged = (e) => {
        this.state.user.userName = e.target.value;
    }

    onPasswordChanged = (e) => {
        this.state.user.password = e.target.value;
    }

    render() {
        var self = this;
        const user = this.state.user;

        return (
            <div className={styles['account-page']}>
                <HeadProvider>
                    <Title>Register new account</Title>
                    <Meta name="viewport" content="initial-scale=1.0, width=device-width" />
                    <Link rel="stylesheet" href="/libs/account/account.css"/>

                </HeadProvider>

                <div className={`${styles['account-box']} ${styles['register-box']}`}>
                    <Form
                        name="register" ref={el => self.formRef = el}>
                        <Form.Item
                            name="email"
                            rules={[
                                {
                                    type: 'email',
                                    message: 'The input is not valid E-mail!',
                                },
                                {
                                    required: true,
                                    message: 'Please input your E-mail!',
                                },
                            ]}
                        >
                            <Input placeholder="Email" onChange={(e) => self.onUserNameChanged(e)} />
                        </Form.Item>

                        <div className='d-flex'>
                            <div className="w-50 pr-1">
                                <Form.Item
                                    name="First Name"
                                    rules={[
                                        {
                                            required: true,
                                            message: '(*)',
                                        },
                                    ]}
                                >
                                    <Input placeholder="First name" onChange={(e) => { user.firstName = e.target.value }} />
                                </Form.Item>
                            </div>

                            <div className="w-50 pl-1">
                                <Form.Item
                                    name="Last Name"
                                    rules={[
                                        {
                                            required: true,
                                            message: '(*)',
                                        },
                                    ]}
                                >
                                    <Input placeholder="Last Name" onChange={(e) => { user.lastName = e.target.value }} />
                                </Form.Item>
                            </div>
                        </div>

                        <div className="w-50 pr-1">
                        <Form.Item
                            name="Date of Birth"
                            rules={[
                                {
                                    required: true,
                                    message: '(*)',
                                },
                            ]}
                        >
                            <DatePicker placeholder="Date of Birth"
                                className="w-100"
                                onChange={(e: any) => { debugger; user.dateOfBirth = e ? e._d : null }}></DatePicker>
                        </Form.Item>
                        </div>

                        <Form.Item
                            name="Password"
                            rules={[
                                {
                                    required: true,
                                    message: 'Please input your password!',
                                },
                            ]}
                            hasFeedback
                        >
                            <Input.Password placeholder="Password" onChange={(e) => { user.password = e.target.value }} />
                        </Form.Item>

                        <Form.Item
                            name="confirm"
                            dependencies={['password']}
                            hasFeedback
                            rules={[
                                {
                                    required: true,
                                    message: 'Please confirm your password!',
                                },
                                ({ getFieldValue }) => ({
                                    validator(_, value) {
                                        if (!value || getFieldValue('Password') === value) {
                                            return Promise.resolve();
                                        }
                                        return Promise.reject(new Error('The two passwords that you entered do not match!'));
                                    },
                                }),
                            ]}
                        >
                            <Input.Password placeholder="Confirm password"
                                onChange={(e) => { user.confirmedPassword = e.target.value }} />
                        </Form.Item>

                        <Button type="primary" onClick={() => self.onRegister()}>
                            Register
                        </Button>
                    </Form>
                </div>
            </div>
        );
    }
}