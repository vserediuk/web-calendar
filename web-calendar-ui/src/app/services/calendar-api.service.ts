import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CalendarViewModel } from '../models/calendar-view-model';
import { Observable } from 'rxjs';
import { Config } from '../config/config';
import { CalendarCreateModel } from '../models/calendar-create-model';

@Injectable({
  providedIn: 'root'
})
export class CalendarApiService {
  private pathApi = this.config.setting['PathAPI'];

  constructor(private config: Config, private httpClient: HttpClient) { }

  getCalendars(userId: number): Observable<CalendarViewModel[]> {
    return this.httpClient.get<CalendarViewModel[]>(this.pathApi + 'Calendar/' + userId);
  }

  addCalendar(calendar: CalendarCreateModel): Observable<CalendarViewModel> {
    return this.httpClient.post<CalendarViewModel>(this.pathApi + 'Calendar', calendar);
  }
}