import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-create-calendar-dialog',
  templateUrl: './create-calendar-dialog.component.html',
  styleUrls: ['./create-calendar-dialog.component.css']
})

export class CreateCalendarDialogComponent {
  name: string = '';
  description: string = '';

  constructor(private dialogRef: MatDialogRef<CreateCalendarDialogComponent>) {
  }

  closeDialog() {
    this.dialogRef.close({ name: this.name, desc: this.description });
  }
}