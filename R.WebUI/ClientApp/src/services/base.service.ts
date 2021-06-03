import axios, {AxiosResponse, AxiosRequestConfig} from 'axios';
import { BASE_API_URL } from '../shared/constants/app-const';
import * as STORAGE_KEY from '../shared/constants/storage-key-const';
import { errorAlert, warningAlert } from '../shared/utils/alerts';

export class BaseService {
    get options() {
		return {
			headers: {
				'Content-Type': 'application/json',
				Authorization: 'Bearer ' + localStorage.getItem(STORAGE_KEY.ACCESS_TOKEN)
			}
		};
	}

	get fileOptions() {
		return {
			headers: {
				Authorization: 'Bearer ' + localStorage.getItem(STORAGE_KEY.ACCESS_TOKEN)
			}
		};
	}

    get<T>(url: string, isCatchError: boolean = true): Promise<AxiosResponse<T>> {
        return axios.get<T>(BASE_API_URL + url, this.options)
		.catch ((res: any) => {
			this.handleError(res.response, isCatchError);
			return res;
		});
	}

	post<T>(url: string, data: any, isCatchError: boolean = true): Promise<AxiosResponse<T>> {
        return axios.post<T>(BASE_API_URL + url, data, this.options)
		.catch ((res: any) => {
			this.handleError(res.response, isCatchError);
			return res;
		});;
	}

    put<T>(url: string, data: any, isCatchError: boolean = true): Promise<AxiosResponse<T>> {
		return axios.put<T>(BASE_API_URL + url, data, this.options)
		.catch ((res: any) => {
			this.handleError(res.response, isCatchError);
			return res;
		});;
	}

	delete<T>(url: string, isCatchError: boolean = true): Promise<AxiosResponse<T>> {
		return axios.delete<T>(BASE_API_URL + url, this.options)
		.catch ((res: any) => {
			this.handleError(res.response, isCatchError);
			return res;
		});;
	}

    private handleError(res: any, isCatchError: boolean) {
		if (res && res.status === 401) {
			// Clear cache
			// Remove old access token if have
			if (this.removeStoreLoggedUser) {
				this.removeStoreLoggedUser();
			}

            warningAlert('Your token has expired', 2500);
			setTimeout(() => {
				// Navigate to login page
				window.location.href = window.location.origin + '/login';
			}, 2000);
			return;
		}

		if (res && res.status === 403) {
			//
			// Navigate to forbidden page
			window.location.href = window.location.origin + '/forbidden';
		}

		let messageError = res && res.data && res.data.message
			? res.data.message : 'Sorry, an error has occurred';

		if (isCatchError) {
			errorAlert(messageError);
		} else {
			throw new Error(messageError);
		}
	}

    removeStoreLoggedUser() {
		localStorage.removeItem(STORAGE_KEY.ACCESS_TOKEN);
	}
}