export class EventApiViewModel {
    constructor(
        public id: number,
        public description: string, 
        public name: string, 
        public startDate: string, 
        public endDate: string, 
        public place: string,
        public notifyTime: number,
        public repeatType: number,
        public calendarId: number,
        public attachedFile: File) {
    }
}
