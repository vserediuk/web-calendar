import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Inject, OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { EventEditModel } from 'src/app/models/event-edit-model';
import { RepeatTypes } from "src/app/models/repeat-type-model"
import { EventApi } from '@fullcalendar/angular';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-event-dialog',
  templateUrl: './edit-event-dialog.component.html',
  styleUrls: ['./edit-event-dialog.component.css']
})

export class EditEventDialogComponent implements OnInit {
  formData: FormGroup;
  event: EventEditModel;
  repeatTypes = RepeatTypes.data;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private dialogRef: MatDialogRef<EditEventDialogComponent>) {}

  ngOnInit() {
    let ev: EventApi = this.data.event;
    this.event = new EventEditModel(
      ev.extendedProps.eventId,
      ev.title,
      ev.start?.toISOString(),
      ev.end?.toISOString(),
      ev.extendedProps.place,
      ev.extendedProps.notifyTime,
      ev.extendedProps.repeatType,
      ev.extendedProps.calendarId,
      ev.extendedProps.attachedFile
    );

    this.formData = new FormGroup({
      title: new FormControl(ev.title, [Validators.required, Validators.maxLength(20)]),
      start: new FormControl(ev.start?.toLocaleString(), [Validators.required]),
      end: new FormControl(ev.end?.toLocaleString(), [Validators.required])
    });
  }

  public getFromForm(name: string) : FormControl {
    return this.formData.get(name) as FormControl;
  }

  public hasError = (controlName: string, errorName: string) =>{
    return this.formData.controls[controlName].hasError(errorName);
  }

  onSelectFile(fileInput: any) {
    this.event.attachedFile = <File>fileInput.target.files[0];
  }

  onDeleteEvent() {
    this.dialogRef.close({
      status: "delete",
      event: null
    });
  }

  closeDialog() {
    this.event.title = this.formData.controls['title'].value;
    this.event.start = this.formData.controls['start'].value;
    this.event.end = this.formData.controls['end'].value;
    this.dialogRef.close({
      event: this.event 
    });
  }
}