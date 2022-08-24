import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AccountService } from 'src/app/services/account.service';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {
    constructor(private accountService: AccountService, private router: Router) {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(catchError((error: HttpErrorResponse) => {
            switch (error.status) {
                case 404: {
                    this.router.navigate(['/404']);
                    break;
                }
                case 401:
                    this.accountService.logout();
                    break;
                case 403: {
                    this.router.navigate(['/403']);                    
                    break;
                }                    
                case 500: {
                    this.router.navigate(['/500']);
                    break;
                }
            }
            return throwError(error);
        }))
    }
}