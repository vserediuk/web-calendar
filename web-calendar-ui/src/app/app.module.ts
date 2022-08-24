import { JwtInterceptor } from './helpers/jwt-interceptor';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FullCalendarModule } from '@fullcalendar/angular'; // must go before plugins
import dayGridPlugin from '@fullcalendar/daygrid'; // a plugin!
import interactionPlugin from '@fullcalendar/interaction'; // a plugin!
import { AppComponent } from './app.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular//material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSidenavModule } from '@angular/material/sidenav';
import { HeadComponent } from './components/head/head.component';
import { ContentComponent } from './components/calendar-content/calendar-content.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { ErrorComponent } from './components/error/error.component'
import { AppRoutingModule } from './app-routing.module';
import { ErrorDialogComponent } from './components/error-dialog/error-dialog.component';
import { ErrorHandlerModule } from './modules/error-handler/error-handler.module';
import { SideContentComponent } from './components/side-content/side-content.component';
import { MatListModule } from '@angular/material/list'
import { MatMenuModule } from '@angular/material/menu'
import { MatExpansionModule } from '@angular/material/expansion';
import { CreateCalendarDialogComponent } from './components/create-calendar-dialog/create-calendar-dialog.component'
import { FormsModule } from '@angular/forms'
import { CreateEventDialogComponent } from './components/create-event-dialog/create-event-dialog.component';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { ReactiveFormsModule } from '@angular/forms';
import { EditEventDialogComponent } from './components/edit-event-dialog/edit-event-dialog.component';
import { LowerCaseUrlSerializer } from './helpers/urlserializer';
import { UrlSerializer } from '@angular/router';

FullCalendarModule.registerPlugins([ // register FullCalendar plugins
  dayGridPlugin,
  interactionPlugin
]);

@NgModule({
  declarations: [
    AppComponent,
    HeadComponent,
    ContentComponent,
    ErrorComponent,
    ErrorDialogComponent,
    SideContentComponent,
    CreateCalendarDialogComponent,
    CreateEventDialogComponent,
    EditEventDialogComponent,
    LoginComponent,
    RegistrationComponent
  ],
  imports: [
    ReactiveFormsModule,
    FormsModule,
    MatExpansionModule,
    MatListModule,
    AppRoutingModule,
    MatInputModule,
    MatDialogModule,
    HttpClientModule,
    MatSidenavModule,
    BrowserAnimationsModule,
    MatSelectModule,
    MatDatepickerModule,
    MatIconModule,
    MatToolbarModule,
    BrowserModule,
    MatButtonModule,
    MatMenuModule,
    MatFormFieldModule,
    FullCalendarModule,
    ErrorHandlerModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },
    {
      provide: UrlSerializer,
      useClass: LowerCaseUrlSerializer
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }