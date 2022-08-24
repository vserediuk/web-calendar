import { EventInput } from "@fullcalendar/angular";

export class EventViewModel implements EventInput {
    constructor(
        public eventId: number,
        public title: string, 
        public start: string, 
        public end: string, 
        public place: string,
        public notifyTime: number,
        public repeatType: number,
        public calendarId: number,
        public attachedFile: File) {
    }
}