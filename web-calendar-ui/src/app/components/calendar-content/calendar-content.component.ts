import { Component, ViewChild, OnInit } from '@angular/core';
import { MatDrawer } from '@angular/material/sidenav';
import { EventClickArg, FullCalendarComponent } from '@fullcalendar/angular';
import { CalendarOptions } from '@fullcalendar/core';
import { FullCalendarService } from 'src/app/services/fullcalendar.service';
import { DrawerService } from 'src/app/services/drawer.service';
import { EventCreateModel } from 'src/app/models/event-create-model';
import { EventApiService } from 'src/app/services/event-api.service';
import { CreateEventDialogComponent } from '../create-event-dialog/create-event-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { EditEventDialogComponent } from '../edit-event-dialog/edit-event-dialog.component';
import { EventEditModel } from 'src/app/models/event-edit-model';

@Component({
  selector: 'app-calendar-content',
  templateUrl: './calendar-content.component.html',
  styleUrls: ['./calendar-content.component.css']
})
export class ContentComponent {
  @ViewChild('drawer')
  drawer: MatDrawer;
  calendarOptions: CalendarOptions;
  constructor(private fullCalendarService: FullCalendarService, private drawerService: DrawerService,
    private eventApiService: EventApiService, private dialog: MatDialog) {
    this.calendarOptions = this.fullCalendarService.calendarOptions;
    this.calendarOptions.eventClick = this.onEventClick.bind(this);
  }

  editEvent: EventEditModel;
  event: EventCreateModel = new EventCreateModel(1);

  @ViewChild('fullcalendar')
  fullCalendarComponent: FullCalendarComponent;

  ngAfterViewInit(): void {
    if (!this.fullCalendarComponent) return;
    this.fullCalendarService.initCalendar(this.fullCalendarComponent);
    this.drawerService.setDrawer(this.drawer);
    this.eventApiService.getEvents(1).forEach(ev => this.fullCalendarService.setEventSource(ev));
  }
  
  onEventClick(info: EventClickArg) {
    let dialogRef = this.dialog.open(EditEventDialogComponent, {
      width: '400px',
      height: '660px',
      data: { event: info.event }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (!result.event && result.status == "delete") {
        this.eventApiService.deleteEvent(info.event.extendedProps.eventId).subscribe(() =>
          this.eventApiService.getEvents(1).forEach(ev => this.fullCalendarService.setEventSource(ev)));
      }
      else {
        this.eventApiService.updateEvent(result.event).subscribe(() =>
          this.eventApiService.getEvents(1).forEach(ev => this.fullCalendarService.setEventSource(ev)));
      }
    });
  }

  onAddEvent() {
    let dialogRef = this.dialog.open(CreateEventDialogComponent, {
      width: '400px',
      height: '660px'
    });

    dialogRef.afterClosed().subscribe(result => {
      this.event = result.event;
      this.eventApiService.addEvent(this.event).subscribe(() =>
        this.eventApiService.getEvents(1).forEach(ev => this.fullCalendarService.setEventSource(ev)));
    });
  }
}