import { Component, OnInit } from '@angular/core';
import { FormControl, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit {

  constructor(private router: Router, private service: AuthService, private fb: FormBuilder) { }

  ngOnInit(): void {
  }

  onSignInGoogle(): void {
    this.service.loginGoogle();
  }

}
