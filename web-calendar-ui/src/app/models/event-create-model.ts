export class EventCreateModel {
    constructor(id: number) {
        this.calendarId = id;
    }
    title: string;
    start: string;
    end: string;
    place: string;
    notifyTime: number;
    repeatType: number;
    calendarId: number;
    attachedFile: File;
}
