import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { CalendarApiService } from 'src/app/services/calendar-api.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  submitted = false;
  loading = false;
  get f() { return this.registrationForm.controls; }
  constructor(private accountService: AccountService, private router: Router, private calendarApiService: CalendarApiService) { }
  registrationForm: FormGroup = new FormGroup({
    firstName: new FormControl('',Validators.required),
    lastName: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required)
  });

  ngOnInit(): void {
  }

  onSubmit() {
    this.accountService.register(this.registrationForm.value).subscribe(() => this.router.navigate(['/home'])
    );
  }
}