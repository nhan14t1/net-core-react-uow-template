import moment from "moment";
import { MILISECOND_OF_DATE, MILISECOND_OF_HOUR } from "../constants/app-const";

export class DateUtils {
    static addHours(inDate: string | Date, hours: number): Date {
        // var date = typeof inDate === 'string' ? new Date(inDate) : inDate;
        return new Date(moment(inDate).valueOf() + hours * MILISECOND_OF_HOUR);
    }

    static formatDateString(inputDate: string | Date): string {
        return moment(inputDate).format("L LT");
    }

    static subDate(inDate: string | Date, numOfDate): Date {
        var date = typeof inDate === 'string' ? new Date(inDate) : inDate;
        return new Date(date.getTime() - numOfDate * MILISECOND_OF_DATE);
    }

    static getBrowserTimezone(): number {
        return 0 - new Date().getTimezoneOffset() / 60;
    }

    static UtcToLocalTime(date: string | Date): Date {
        var dateString = typeof date === 'string' ? date : date.toLocaleString();
        return this.addHours(dateString, this.getBrowserTimezone());
    }

    static localTimeToUtc(inDate: string | Date): string {
        return this.addHours(inDate, 0 - this.getBrowserTimezone()).toLocaleString();
    }

    static timeSince(inDate: Date | string): string {
        var date = typeof inDate === 'string' ? new Date(inDate) : inDate;
        var seconds = Math.floor((new Date().getTime() - date.getTime()) / 1000);

        var interval = seconds / 31536000;

        if (interval > 1) {
            return Math.floor(interval) + " years";
        }
        interval = seconds / 2592000;
        if (interval > 1) {
            return Math.floor(interval) + " months";
        }
        interval = seconds / 86400;
        if (interval > 1) {
            return Math.floor(interval) + " days";
        }
        interval = seconds / 3600;
        if (interval > 1) {
            return Math.floor(interval) + " hours";
        }
        interval = seconds / 60;
        if (interval > 1) {
            return Math.floor(interval) + " minutes";
        }
        return Math.floor(seconds) + " seconds";
    }
}