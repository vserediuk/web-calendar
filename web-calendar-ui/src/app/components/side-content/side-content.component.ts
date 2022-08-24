import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CalendarCreateModel } from 'src/app/models/calendar-create-model';
import { CalendarViewModel } from 'src/app/models/calendar-view-model';
import { UserViewModel } from 'src/app/models/user-view-model';
import { AccountService } from 'src/app/services/account.service';
import { CalendarApiService } from 'src/app/services/calendar-api.service';
import { CreateCalendarDialogComponent } from '../create-calendar-dialog/create-calendar-dialog.component';

@Component({
  selector: 'app-side-content',
  templateUrl: './side-content.component.html',
  styleUrls: ['./side-content.component.css']
})

export class SideContentComponent implements OnInit {
  calendars: CalendarViewModel[];
  user: UserViewModel = this.accountService.userValue;
  calendar: CalendarCreateModel;

  calendarObserver = {
    next: (calendars: CalendarViewModel[]) => { this.calendars = calendars; }
  }

  constructor(private calendarApiService: CalendarApiService, private dialog: MatDialog, private accountService: AccountService) {
  }

  ngOnInit(): void {
    if (this.user) {
      this.calendarApiService.getCalendars(this.user.id).subscribe(this.calendarObserver);
      this.calendar = new CalendarCreateModel(this.user.id);
    }
  }

  onAddCalendar() {
    let dialogRef = this.dialog.open(CreateCalendarDialogComponent, {
      width: '270px',
      height: '370px'
    });

    dialogRef.afterClosed().subscribe(result => {
      this.calendar.name = result.name;
      this.calendar.description = result.desc;
      this.calendarApiService.addCalendar(this.calendar).subscribe((res: CalendarViewModel) => {
        this.calendars.push(res);
      })
    });
  }
}