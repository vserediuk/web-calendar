export class CalendarCreateModel {
    constructor(userId: number) {
        this.userId = userId;
    }
    userId: number;
    description: string;
    name: string;
}
