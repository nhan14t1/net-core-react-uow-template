import {UserModel} from '../shared/models/user-model'
import * as StorageKey from '../shared/constants/storage-key-const'
import { AVATAR_PATH, DEFAULT_AVATAR } from '../shared/constants/app-const';
import { BaseService } from './base.service';
import { AxiosResponse } from 'axios';
import { generateUniqueId } from '../shared/utils/app-utils';

export class UserService {
    baseService: BaseService;

    constructor() {
        this.baseService = new BaseService();
    }

    saveInfo(user: UserModel) {
        localStorage.setItem(StorageKey.USER_ID, user.id);
        localStorage.setItem(StorageKey.USER_NAME, user.userName);
        localStorage.setItem(StorageKey.ACCESS_TOKEN, user.accessToken);
        localStorage.setItem(StorageKey.ACCESS_TOKEN_EXPIRATION_TIMESTAMP, user.tokenExpirationInTimeStamp.toString());
        localStorage.setItem(StorageKey.FIRST_NAME, user.firstName || '');
        localStorage.setItem(StorageKey.LAST_NAME, user.lastName || '');
        localStorage.setItem(StorageKey.AVATAR_URL, user.avatarUrl || '');
    }

    clearInfo() {
        localStorage.clear();
    }

    isLoggedIn(): boolean {
        var expirationTimestamp = Number(localStorage.getItem(StorageKey.ACCESS_TOKEN_EXPIRATION_TIMESTAMP) || '');

        if (!localStorage.getItem(StorageKey.ACCESS_TOKEN)
            || isNaN(expirationTimestamp)) {
            return false;
        }

        return new Date().getTime() < expirationTimestamp;
    }

    getCurrenUser(): UserModel | null {
        if (!this.isLoggedIn()) {
            return null;
        }

        return new UserModel ({
            id: localStorage.getItem(StorageKey.USER_ID) || '',
            userName: localStorage.getItem(StorageKey.USER_NAME) || '',
            accessToken: localStorage.getItem(StorageKey.ACCESS_TOKEN) || '',
            firstName: localStorage.getItem(StorageKey.FIRST_NAME) || '',
            lastName: localStorage.getItem(StorageKey.LAST_NAME) || '',
            avatarUrl: this.getFullAvatarUrl(localStorage.getItem(StorageKey.AVATAR_URL) || '')
        });
    }

    login(userName: string, password: string): Promise<AxiosResponse<UserModel>> {
        var self = this;
        return this.baseService.post<any>('/Account/login', {userName: userName, password: password})
        .then(function(res: AxiosResponse<UserModel>) {
            self.saveInfo(res.data);
            return res;
        });
    }

    register(user: UserModel): Promise<AxiosResponse<UserModel>> {
        var self = this;
        
        return this.baseService.post<UserModel>('/Account/register', user)
        .then(function(res: AxiosResponse<UserModel>) {
            self.saveInfo(res.data);
            return res;
        });
    }

    logout() {
        this.clearInfo();
        location.href = '/';
    }

    getFullAvatarUrl(rawAvatarUrl: string) {
        rawAvatarUrl = !!rawAvatarUrl ? rawAvatarUrl : DEFAULT_AVATAR;
        return AVATAR_PATH + rawAvatarUrl;
    }
}