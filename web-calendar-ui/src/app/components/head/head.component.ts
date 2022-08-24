import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { DrawerService } from 'src/app/services/drawer.service';
import { FullCalendarService } from '../../services/fullcalendar.service';

@Component({
  selector: 'app-head',
  templateUrl: 'head.component.html',
  styleUrls: ['./head.component.css']
})
export class HeadComponent implements OnInit {
  constructor(public fullCalendarService: FullCalendarService, private drawerService: DrawerService, private changeDetector: ChangeDetectorRef) {
  }
  currentDate: string;
  calendarViews = this.fullCalendarService.calendarViews;
  initialView = this.fullCalendarService.calendarOptions.initialView;
  onViewSelected = this.fullCalendarService.onViewSelected.bind(this.fullCalendarService);
  next = this.fullCalendarService.next.bind(this.fullCalendarService);
  prev = this.fullCalendarService.prev.bind(this.fullCalendarService);
  today = this.fullCalendarService.today.bind(this.fullCalendarService);

  drawerToggle() {
    this.drawerService.toggle();
  }

  ngOnInit() {
    this.fullCalendarService.getDate().subscribe((val: string) => {
      this.currentDate = val;
      this.changeDetector.detectChanges();
    })
  };
}