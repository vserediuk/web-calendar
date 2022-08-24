import { Injectable } from '@angular/core';
import { CalendarApi, CalendarOptions, DatesSetArg, EventApi, EventSourceInput, FullCalendarComponent } from '@fullcalendar/angular';
import { Observable, ReplaySubject } from 'rxjs';
import { EventViewModel } from '../models/event-view-model';

interface CalendarView {
  value: string,
  viewValue: string
};

@Injectable({
  providedIn: 'root'
})
export class FullCalendarService {
  fullCalendarComponent: FullCalendarComponent;
  private fullCalendarApi: CalendarApi;
  currentDate: ReplaySubject<string> = new ReplaySubject<string>(0);
  constructor() { }

  initCalendar(component: FullCalendarComponent): void {
    this.fullCalendarComponent = component;
    this.fullCalendarApi = this.fullCalendarComponent.getApi();
  }

  addEvent(event: EventViewModel) {
    this.fullCalendarApi.addEvent(event);
  }

  setEventSource(source: EventSourceInput) {
    this.fullCalendarApi.removeAllEvents();
    this.fullCalendarApi.removeAllEventSources();
    this.fullCalendarApi.addEventSource(source);
    this.fullCalendarApi.refetchEvents();
  }

  getEventById(id: number): EventApi | null {
    return this.fullCalendarApi.getEventById(id.toString());
  }

  next() {
    this.fullCalendarApi.next();
  }

  prev() {
    this.fullCalendarApi.prev();
  }

  today() {
    this.fullCalendarApi.today();
  }

  calendarOptions: CalendarOptions = {
    headerToolbar: false,
    initialView: 'dayGridMonth',
    datesSet: this.onDateSet.bind(this)
  }

  onDateSet(arg: DatesSetArg) {
    this.currentDate.next(arg.startStr);
  }

  onViewSelected(event: any) {
    this.fullCalendarApi.changeView(event.value);
  }

  getDate(): Observable<string> {
    return this.currentDate;
  }

  calendarViews: CalendarView[] = [
    { value: 'dayGridMonth', viewValue: 'Month' },
    { value: 'dayGridWeek', viewValue: 'Week' },
    { value: 'dayGridDay', viewValue: 'Day' }
  ]
}