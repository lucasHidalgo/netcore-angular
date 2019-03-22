import { AuthService } from './../_services/auth.service';
import { Component, OnInit, Input , EventEmitter, Output } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import {FormGroup, FormControl, FormBuilder, Validators} from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};
  registerForm: FormGroup;
  bsConfig: BsDatepickerConfig;
  constructor(private authService: AuthService, private alertify: AlertifyService, private fb: FormBuilder) { }

  ngOnInit() {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      gender: ['male'],
      nombreUsuario: ['', Validators.required],
      knownAs: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4),Validators.maxLength(8)]],
      confirmPassword: ['', Validators.required]
    },{validator: this.passwordMatchValidity}
    )
  }

  passwordMatchValidity(g: FormGroup ) {
    return g.get('password').value === g.get('confirmPassword') .value ? null : {'missmatch': true};
  }
  register() {
    // this.authService.register(this.model).subscribe(() => {
    //   this.alertify.success('registration successful');
    // },
    // error => {
    //   this.alertify.error(error);
    // });

  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
