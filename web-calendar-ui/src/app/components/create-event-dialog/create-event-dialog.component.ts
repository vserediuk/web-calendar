import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { EventCreateModel } from 'src/app/models/event-create-model';
import { RepeatTypes } from 'src/app/models/repeat-type-model';

@Component({
  selector: 'app-create-event-dialog',
  templateUrl: './create-event-dialog.component.html',
  styleUrls: ['./create-event-dialog.component.css']
})

export class CreateEventDialogComponent implements OnInit {
  event: EventCreateModel = new EventCreateModel(1);
  formData: FormGroup;
  repeatTypes = RepeatTypes.data;

  constructor(private dialogRef: MatDialogRef<CreateEventDialogComponent>) {
  }

  ngOnInit() {
    this.formData = new FormGroup({
      title: new FormControl('', [Validators.required, Validators.maxLength(20)]),
      start: new FormControl('', [Validators.required]),
      end: new FormControl('', [Validators.required])
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

  closeDialog() {
    this.event.title = this.formData.controls['title'].value;
    this.event.start = this.formData.controls['start'].value;
    this.event.end = this.formData.controls['end'].value;
    this.dialogRef.close({ 
      event: this.event 
    });
  }
}