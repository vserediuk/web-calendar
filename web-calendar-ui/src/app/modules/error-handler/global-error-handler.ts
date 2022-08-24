import { HttpErrorResponse } from '@angular/common/http';
import { ErrorHandler, Injectable, NgZone } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/components/error-dialog/error-dialog.component';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
    constructor(private zone: NgZone, private dialog: MatDialog) {
    }

    handleError(error: any): void {
        this.zone.run(() => {
            if (!this.isClientError(error)) return;
            this.dialog.open(ErrorDialogComponent, {
                width: '350px',
                height: '300px',
                data: error.message ?? 'undefined client error'
            });
            console.log(error);
        });
    }

    isClientError(error: any): error is HttpErrorResponse {
        return false;
    }
}