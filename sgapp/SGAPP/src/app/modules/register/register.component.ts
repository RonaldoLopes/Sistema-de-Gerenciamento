import { Component, OnInit, ViewChild } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import { MatTableDataSource, MatPaginator, MatSnackBar, MatSort, MatDialog } from '@angular/material';

import { HttpClient } from '@angular/common/http';
import { User } from 'src/app/_models/User';
import { AuthService } from 'src/app/_services/auth.service';
import { Role } from 'src/app/_models/Role';
import { delay } from 'rxjs/operators';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  displayedColumns: string[] = [
    'id', 'userName', 'fullName', 'email', 'role', 'funcao', 'setor', 'Opcoes'
  ];
  dataSource =  new MatTableDataSource();

  registerForm: FormGroup;

  modoSalvar = 'post';
  hide = true;
  hideConf = true;
  user: any;
  users: User[];
  role: Role = new Role();
  userId: number;
  oldRole: any;

  constructor(
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    private http: HttpClient,
    private authService: AuthService,
  ) {}

  ngOnInit() {
    this.validation();
    this.getAllUser();
  }

  getAllUser() {
    this.authService.getAllUser().subscribe(
      (_usr: User[]) => {
        this.users = _usr;
        this.dataSource = new MatTableDataSource(this.users);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      }, error => {
        console.log(error);
      }
    );
  }
  editar(usuario: User) {
    this.modoSalvar = 'put';
    this.user = usuario;
    //this.registerForm.patchValue(usuario);
    this.registerForm.get('userName').setValue(this.user.user.userName);
    this.registerForm.get('fullName').setValue(this.user.user.fullName);
    this.registerForm.get('email').setValue(this.user.user.email);
    this.registerForm.get('role').setValue(this.user.role.name);
    this.registerForm.get('funcao').setValue(this.user.user.funcao);
    this.registerForm.get('setor').setValue(this.user.user.setor);

    this.oldRole = this.user.role.name;
    this.userId = this.user.user.id;
  }

  salvarAlteracoes() {
    if (this.registerForm.valid) {
      if (this.modoSalvar === 'post') {
        this.user = Object.assign({}, this.registerForm.value);
        this.authService.register(this.user).subscribe(
          () => {
            this.role.email = this.user.email;
            this.role.delete = 'false';
            this.role.role = this.registerForm.get('role').value;
            this.updateUser();
          }, error => {
            const erro = error.error;
            if (erro === 'Email duplicado') {
              this.snackBar.open('Email duplicado', 'Fechar', {
                duration: 2000
              });
            } else if (erro[0].code === 'DuplicateUserName') {
               this.snackBar.open('Nome de usuário duplicado', 'Fechar', {
                duration: 2000
              });
            }
          }
        );
      } else {
        this.user = Object.assign({id: this.userId}, this.registerForm.value);
        this.authService.putUser(this.user).subscribe(
          () => {
            this.role.email = this.user.email;
            this.role.delete = 'true';
            this.role.roleNew = this.registerForm.get('role').value;
            this.role.role = this.oldRole;
            this.updateUser();
          }
        );
      }
    }
  }
  updateUser() {
    delay(500);
    this.authService.putUserRole(this.role).subscribe(
      () => {
        this.snackBar.open('Operação efetuada com sucesso', 'Fechar', {
          duration: 2000
        });
        this.getAllUser();
        this.resetForm();
      },
    );
  }

  comparePass() {
    if (this.registerForm.get('password').value === this.registerForm.get('confirmPassword').value) {
    } else {
      this.snackBar.open('Senhas não conferem:', 'Fechar', {
        duration: 3000
      });
    }
  }

  resetForm() {
    this.registerForm.setValue({
      userName: '',
      fullName: '',
      email: '',
      password: '',
      confirmPassword: '',
      funcao: '',
      setor: '',
      role: ''
    });
  }
  validation() {
    this.registerForm = this.fb.group({
      userName: ['', [Validators.required]],
      fullName: ['', [Validators.required]],
      email: ['', [Validators.required]],
      password: ['', [ Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [ Validators.required, Validators.minLength(6)]],
      role: ['', [Validators.required]],
      funcao: [''],
      setor: ['']
    });
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLocaleLowerCase();
  }
}
