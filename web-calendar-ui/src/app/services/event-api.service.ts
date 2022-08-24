import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EventApiViewModel } from '../models/event-api-view-model';
import { Observable } from 'rxjs';
import { Config } from '../config/config';
import { EventCreateModel } from '../models/event-create-model';
import { EventViewModel } from '../models/event-view-model';
import { map } from 'rxjs/operators';
import { EventEditModel } from '../models/event-edit-model';

@Injectable({
  providedIn: 'root'
})
export class EventApiService {
  private pathApi = this.config.setting['PathAPI'];

  constructor(private config: Config, private httpClient: HttpClient) { }

  apiModelToUiModel(event: EventApiViewModel) {
    let eventViewModel: EventViewModel = new EventViewModel(
      event.id,
      event.name,
      event.startDate, 
      event.endDate, 
      event.place,
      event.notifyTime,
      event.repeatType,
      event.calendarId = event.calendarId,
      event.attachedFile
    )
    return eventViewModel;
  }

  getEvents(calendarId: number): Observable<EventViewModel[]> {
    return this.httpClient.get<EventApiViewModel[]>(this.pathApi + 'Event/' + calendarId)
      .pipe(map(evs => evs.map(ev => this.apiModelToUiModel(ev))));

  }

  addEvent(event: EventCreateModel): Observable<EventViewModel> {
    let formData = new FormData();
    formData.append("name", event.title);
    formData.append("startDate", event.start);
    formData.append("endDate", event.end);
    formData.append("place", event.place);
    formData.append("notifyTime", event.notifyTime.toString());
    formData.append("calendarID", event.calendarId.toString());
    formData.append("repeatType", event.repeatType.toString());
    formData.append("attachedFile", event.attachedFile);

    return this.httpClient.post<EventApiViewModel>(this.pathApi + 'Event', formData)
      .pipe(map(ev => this.apiModelToUiModel(ev)));
  }

  updateEvent(event: EventEditModel): Observable<EventViewModel> {
    let formData = new FormData();
    formData.append("id", event.eventId.toString())
    formData.append("name", event.title);
    formData.append("startDate", event.start || "");
    formData.append("endDate", event.end || "");
    formData.append("place", event.place);
    formData.append("notifyTime", event.notifyTime.toString());
    formData.append("calendarID", event.calendarId.toString());
    formData.append("repeatType", event.repeatType.toString());
    formData.append("attachedFile", event.attachedFile);

    return this.httpClient.put<EventApiViewModel>(this.pathApi + 'Event', formData)
      .pipe(map(ev => this.apiModelToUiModel(ev)));
  }

  deleteEvent(id: number) {
    return this.httpClient.delete(this.pathApi + 'Event/' + id);
  }
}