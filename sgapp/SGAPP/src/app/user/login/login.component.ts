import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { AuthService } from 'src/app/_services/auth.service';
import { VisitorsService } from 'src/app/_services/visitors.service';
import { Login } from 'src/app/_models/Login';
import { objectEach } from 'highcharts';
import { Router } from '@angular/router';
import { duration } from 'moment';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  titulo = 'Login';
  model: any = {};
  hide = true;
  logins: Login;
  spinnerLogin: false;

  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private snackBar: MatSnackBar,
    private ip: VisitorsService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.validation();

    if (localStorage.getItem('token') !== null) {
      this.ip.getIpAddress().subscribe((res: any) => {
        if (res.ip === '138.94.52.31') {
          localStorage.setItem('access', 'interno');
        } else {
          localStorage.setItem('access', 'externo');
        }
      });
    }
  }

  login() {
     if (this.registerForm.valid) {
      this.logins = Object.assign({}, this.registerForm.value);
      this.authService.login(this.logins).subscribe(
        () => {
          this.spinnerLogin = false;
          this.router.navigate(['/']);
          this.snackBar.open('Login com sucesso', 'Fechar', {
            duration: 2000
          });
        }, error => {
          this.spinnerLogin = false;
          this.snackBar.open('Algo está errado: ' + error, 'Fechar', {
            duration: 3000
          });
          console.log(error);
        }
      );
     }
  }
  spinnerShow(ativo) {
    this.spinnerLogin = ativo;
  }
  validation() {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }
}
