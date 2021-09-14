import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import { PontoService } from 'src/app/_services/ponto.service';
import { ProjetoService } from 'src/app/_services/projeto.service';
import { DateAdapter, MatDialog, MatSnackBar } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { Ponto } from 'src/app/_models/ponto';
import { ConsultaprojetoComponent } from 'src/app/shared/consultaprojeto/consultaprojeto.component';
import { Projeto } from 'src/app/_models/Projeto';

@Component({
  selector: 'app-ponto-cadastro',
  templateUrl: './ponto-cadastro.component.html',
  styleUrls: ['./ponto-cadastro.component.scss']
})
export class PontoCadastroComponent implements OnInit {

  registerForm: FormGroup;
  modoSalvar = 'post';
  disableFields: boolean;
  ponto: Ponto;
  idUsers: any;
  codUser: number;
  codUserFlag: number;
  flagProjetoId: any;

  constructor(
    private fb: FormBuilder,
    private pontService: PontoService,
    private projetoService: ProjetoService,
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
    const idPE = +this.router.snapshot.paramMap.get('id');
    this.idUsers = localStorage.getItem('idUser');
    this.validation();

    if (idPE === 0) {
      this.modoSalvar = 'post';
    } else {
      this.modoSalvar = 'put';
      this.carregaPP(idPE);
    }
  }
  carregaPP(id: number) {
    this.pontService.getPontoId(id).subscribe(
      // tslint:disable-next-line: variable-name
      (_pp: Ponto) => {
        this.ponto = Object.assign({}, _pp);
        this.codUser = this.ponto.userId;
        this.registerForm.patchValue(_pp);
        this.getProjetoId(this.ponto.projetosId);
        this.disableFields = true;
      }
    );
  }
  getProjetoId(id: number) {
    this.projetoService.getProjetoId(id).subscribe(
      (_pj: Projeto) => {
        this.registerForm.get('projeto').setValue(_pj.codProjeto);
        let dataIni = new Date(_pj.dataInicio);
        this.registerForm.get('dataInicio').setValue(dataIni.toLocaleDateString());
      }
    );
  }

  salvarAlteracao() {
    if (this.registerForm.valid) {
      if (this.modoSalvar === 'post') {
        this.ponto = Object.assign({}, this.registerForm.value);
        this.ponto.userId = this.idUsers;
        this.pontService.postPonto(this.ponto).subscribe(
          (novo: Ponto) => {
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
        this.ponto = Object.assign({id: this.ponto.id}, this.registerForm.value);
        if (this.codUser === this.idUsers) {
          this.ponto.userId = this.idUsers;
        } else {
          this.ponto.userId = this.codUser;
        }
        this.pontService.putPonto(this.ponto).subscribe(
          () => {
            this.snackBar.open('Operação efetuada com sucesso', 'Fechar', {
              duration: 2000
            });
            this.route.navigate(['/', 'pontoc']);
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
          this.pontService.findLancamento(this.registerForm.get('data').value, this.codUserFlag, this.flagProjetoId).subscribe(
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
      entraFabrica: '',
      saidaAlmo: '',
      retorAlmo: '',
      saidaFabrica: '',
      atvDia: '',
      projetosId: '',
      projeto: '',
      dataInicio: ''
    });
  }
  cancelSave() {
    this.route.navigate(['/']);
  }
  carregarProjeto() {
    const dialoagRef = this.dialog.open(ConsultaprojetoComponent, {
      width: '920px',
      height: '560px'
    });

    dialoagRef.afterClosed().subscribe(result => {
      this.registerForm.get('projetosId').setValue(result.id);
      this.registerForm.get('projeto').setValue(result.codProjeto);
      let dataIni = new Date(result.dataInicio);
      this.registerForm.get('dataInicio').setValue(dataIni.toLocaleDateString());
    });
  }
  validation() {
    this.registerForm = this.fb.group({
      data: ['', [Validators.required]],
      entraFabrica: [''],
      saidaAlmo: [''],
      retorAlmo: [''],
      saidaFabrica: [''],
      atvDia: [''],
      projetosId: ['', [Validators.required]],
      projeto: [''],
      dataInicio: ['']
    });
  }
}
