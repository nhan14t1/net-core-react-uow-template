import { UserService } from "../../services/user.service";
import { PROFILE_PATH, TOPIC_PATH } from "../constants/app-const";
import { errorAlert } from "./alerts";

export function deepClone<T>(obj: T): T {
    return JSON.parse(JSON.stringify(obj));
}

export function getBrowserTimezone(): number {
    return 0 - new Date().getTimezoneOffset() / 60;
}

export function getUtcTimeStamp(): number {
    var now = new Date();
    now.setHours(0 - getBrowserTimezone());

    return now.getTime();
}

export function generateUniqueId(): number {
    return getUtcTimeStamp() + Math.floor(Math.random() * 10001) * 1000;
}

export function copyToClipboard(text: string): Promise<any> {
    return navigator.clipboard.writeText(text).catch(() => {
        errorAlert('Sorry, an error has occurred while copying top clipboard', 2);
    })
}

export function getTopicLink(uniqueId: number, isFull?: boolean): string {
    return (isFull ? location.origin : '') + TOPIC_PATH + uniqueId;
}

export function getProfileLink(uniqueName: string, isFull?: boolean): string {
    return (isFull ? location.origin : '') + PROFILE_PATH + uniqueName;
}