export class UserModel {
	id: string;
	userName: string;
	password: string;
	firstName: string;
	lastName: string;
	accessToken: string;
	avatarUrl: string;
	tokenExpirationInTimeStamp: number;

	uniqueName?: string;
	confirmedPassword?: string;
	dateOfBirth?: Date;

	constructor(init?: Partial<UserModel>) {
		if (init) {
			Object.assign(this, init);
		}
	}
}