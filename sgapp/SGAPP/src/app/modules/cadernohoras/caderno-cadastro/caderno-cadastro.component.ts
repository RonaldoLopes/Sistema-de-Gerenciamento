import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import { CadernoHoras } from 'src/app/_models/CadernoHoras';
import { ProjetoService } from 'src/app/_services/projeto.service';
import { CadernoService } from 'src/app/_services/caderno.service';
import { DateAdapter, MatDialog, MatSnackBar, TransitionCheckState } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { ConsultaprojetoComponent } from 'src/app/shared/consultaprojeto/consultaprojeto.component';
import { Projeto } from 'src/app/_models/Projeto';

@Component({
  selector: 'app-caderno-cadastro',
  templateUrl: './caderno-cadastro.component.html',
  styleUrls: ['./caderno-cadastro.component.scss']
})
export class CadernoCadastroComponent implements OnInit {

  registerForm: FormGroup;
  modoSalvar = 'post';
  disableFields: boolean;
  cadernoHoras: CadernoHoras;
  roleUSer: any;
  idUsers: any;
  projeto: Projeto;
  codUser: number;
  flagProjetoId: any;
  codUserFlag: number;

  constructor(
    private fb: FormBuilder,
    private projetoService: ProjetoService,
    private cadernoService: CadernoService,
    private dateAdapter: DateAdapter<Date>,
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private router: ActivatedRoute,
    private route: Router,
  ) {
    this.dateAdapter.setLocale('pt-br');
    this.route = route;
    this.disableFields = false;
  }

  ngOnInit(): void {

    this.idUsers = localStorage.getItem('idUser');
    this.roleUSer = localStorage.getItem('role');
    const idCH = + this.router.snapshot.paramMap.get('id');
    this.validation();

    if (idCH === 0) {
      this.modoSalvar = 'post';
    } else {
      this.modoSalvar = 'put';
      this.carregarCH(idCH);
    }
  }

  carregarCH(id: number) {
    this.cadernoService.getCadernoHorasId(id).subscribe(
      (_ch: CadernoHoras) => {
        this.cadernoHoras = Object.assign({} , _ch);
        this.codUser = this.cadernoHoras.userId;
        this.registerForm.patchValue(_ch);
        this.getProjetoId(this.cadernoHoras.projetosId);
      }
    );
  }

  getProjetoId(id: number) {
    this.projetoService.getProjetoId(id).subscribe(
      (_pj: Projeto) => {
        this.projeto = _pj;
        this.registerForm.get('projetosId').setValue(id);
        this.registerForm.get('projeto').setValue(this.projeto.codProjeto);
        const dataIni = new Date(_pj.dataInicio);
        this.registerForm.get('dataInicio').setValue(dataIni.toLocaleDateString());
      }, error => {
        console.log(error);
      }
    );
  }

  salvarAlteracao() {
    if (this.registerForm.valid) {
      if (this.modoSalvar === 'post') {
        this.cadernoHoras = Object.assign({}, this.registerForm.value);
        this.cadernoHoras.userId = this.idUsers;
        this.cadernoService.postCadernoHoras(this.cadernoHoras).subscribe(
          () => {
            this.snackBar.open('Operação efetuada com sucesso', 'Fechar', {
              duration: 2000
            });
            this.resetForm();
          }, error => {
            this.snackBar.open('Algo está errado: ' + error, 'Fechar', {
              duration: 3000
            });
            console.log(error);
          }
        );
      } else {
        this.cadernoHoras = Object.assign({id: this.cadernoHoras.id}, this.registerForm.value);
        if (this.codUser === this.idUsers) {
          this.cadernoHoras.userId = this.idUsers;
        } else {
          this.cadernoHoras.userId = this.codUser;
        }
        this.cadernoService.putCadernoHoras(this.cadernoHoras).subscribe(
          () => {
            this.snackBar.open('Operação efetuada com sucesso', 'Fechar', {
              duration: 2000
            });
            this.route.navigate(['/', 'cadernosc']);
          }, error => {
            this.snackBar.open('Algo está errado: ' + error, 'Fechar', {
              duration: 3000
            });
            console.log(error);
          }
        );
      }
    }
  }

  findDataLancamento() {

    if (this.registerForm.valid) {

      this.flagProjetoId = this.registerForm.get('projetosId').value;
      if (this.modoSalvar === 'post') {
        if (this.registerForm.get('data').value !== '') {
          if (this.codUser === this.idUsers) {
            this.codUserFlag = this.codUser;
          } else {
            this.codUserFlag = this.idUsers;
          }
          this.cadernoService.findLancamento(this.registerForm.get('data').value, this.codUserFlag, this.flagProjetoId).subscribe(
            res => {
              if (res === null) {
                this.salvarAlteracao();
              } else {
                this.snackBar.open('Já existe um lançamento com esta data para este usuário neste projeto, por favor opte por alterar os dados', 'Fechar', {
                  duration: 8000
                });
                this.resetForm();
                return;
              }
            },
            err => console.log('erro')
          );
        }
      } else {
        this.salvarAlteracao();
      }
    }
  }
  resetForm() {
    this.registerForm.setValue({
      data: '',
      horasDia: '',
      deslocamento: '',
      horasTrab: '',
      projetosId: '',
      projeto: '',
      dataInicio: ''
    });
  }

  carregarProjeto() {
    const dialoagRef = this.dialog.open(ConsultaprojetoComponent, {
      width: '920px',
      height: '560px'
    });

    dialoagRef.afterClosed().subscribe(result => {
      this.registerForm.get('projetosId').setValue(result.id);
      this.registerForm.get('projeto').setValue(result.codProjeto);
      const dataIni = new Date(result.dataInicio);
      this.registerForm.get('dataInicio').setValue(dataIni.toLocaleDateString());
    });
  }

  validation() {
    this.registerForm = this.fb.group({
      data: ['', [Validators.required]],
      horasDia: [''],
      deslocamento: [''],
      horasTrab: [''],
      projetosId: ['', [Validators.required]],
      projeto: [''],
      dataInicio: ['']
    });
  }

}
