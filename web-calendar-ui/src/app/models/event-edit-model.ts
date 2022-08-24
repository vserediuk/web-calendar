import { EventInput } from "@fullcalendar/angular";

export class EventEditModel implements EventInput {
    constructor(
        public eventId: number,
        public title: string, 
        public start: string | undefined, 
        public end: string | undefined, 
        public place: string,
        public notifyTime: number,
        public repeatType: number,
        public calendarId: number,
        public attachedFile: File) {
    }
}