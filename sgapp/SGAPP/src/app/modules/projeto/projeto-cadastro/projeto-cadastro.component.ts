import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import { DateAdapter, MatDialog, MatSnackBar } from '@angular/material';
import { ClienteService } from 'src/app/_services/cliente.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Cliente } from 'src/app/_models/Cliente';
import { ClienteconsultaComponent } from 'src/app/shared/clienteconsulta/clienteconsulta.component';
import { Projeto } from 'src/app/_models/Projeto';
import { ProjetoService } from 'src/app/_services/projeto.service';

@Component({
  selector: 'app-projeto-cadastro',
  templateUrl: './projeto-cadastro.component.html',
  styleUrls: ['./projeto-cadastro.component.scss']
})
export class ProjetoCadastroComponent implements OnInit {

  registerForm: FormGroup;

  clientes: Cliente[];
  clientesConsulta: Cliente;

  modoSalvar = 'post';

  dadosCliente = {
    localCliente: '',
    areaCliente: ''
  };

  projeto: Projeto;
  projetos: Projeto[];

  numberPattern = '@"^-?[0-9][0-9,\.]+$"';

  constructor(
    private fb: FormBuilder,
    private dateAdapter: DateAdapter<Date>,
    private clienteService: ClienteService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private router: ActivatedRoute,
    private route: Router,
    private projetoService: ProjetoService
  ) {
    this.dateAdapter.setLocale('pt-br');
    /*this.registerForm = fb.group({
      id: [''],
      cliente: [''],
      endereco: [''],
      area: ['']
    });*/
  }

  ngOnInit(): void {
    const idProjeto = +this.router.snapshot.paramMap.get('id');
    this.validation();
    this.getClientes();

    if (idProjeto === 0) {
      this.modoSalvar = 'post';
    } else {
      this.modoSalvar = 'put';
      this.carregarP(idProjeto);
    }
  }

  carregarP(id: number) {
    this.projetoService.getProjetoId(id).subscribe(
      (_cp: Projeto) => {
        this.projeto = Object.assign({}, _cp);
        this.registerForm.patchValue(_cp);
        this.getClientesId(this.projeto.clienteId);
      }
    );
  }

  getClientesId(id: number) {
    this.clienteService.getClienteId(id).subscribe(
      (_cli: Cliente) => {
        this.clientesConsulta = _cli;
        this.registerForm.get('clienteDesc').setValue(this.clientesConsulta.clientes);
        this.registerForm.get('endereco').setValue(this.clientesConsulta.endereco);
        this.registerForm.get('area').setValue(this.clientesConsulta.area);
      }, error => {
        console.log(error);
      }
    );
  }
  getClientes() {
    this.clienteService.getAllCliente().subscribe(
      (_clientes: Cliente[]) => {
        this.clientes = _clientes;
      }, error => {
        console.log(error);
      }
    );
  }

  salvarAlteracao() {
    if (this.registerForm.valid) {
      if (this.modoSalvar === 'post') {
        this.projeto = Object.assign({}, this.registerForm.value);
        this.projetoService.postProjeto(this.projeto).subscribe(
          (novo: Projeto) => {
            this.resetForm();
            this.snackBar.open('Operação efetuada com sucesso', 'Fechar', {
              duration: 2000
            });
          }, error => {
            this.snackBar.open('Algo está errado: ' + error, 'Fechar', {
              duration: 3000
            });
            console.log(error);
          }
        );
      } else {
        this.projeto = Object.assign({id: this.projeto.id}, this.registerForm.value);
        this.projetoService.putProjeto(this.projeto).subscribe(
          () => {
            this.snackBar.open('Operação efetuada com sucesso', 'Fechar', {
              duration: 2000
            });
            this.route.navigate(['/', 'projetosc']);
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

  carregaClienteDados() {
    const dialoagRef = this.dialog.open(ClienteconsultaComponent, {
      width: '920px',
      height: '560px'
    });

    dialoagRef.afterClosed().subscribe(result => {
      this.registerForm.get('clienteId').setValue(result.id);
      this.registerForm.get('clienteDesc').setValue(result.clientes);
      this.registerForm.get('endereco').setValue(result.endereco);
      this.registerForm.get('area').setValue(result.area);
    });
  }
  resetForm() {
    this.registerForm.setValue({
      recursosPrev: '',
      descricao: '',
      codProjeto: '',
      recursosUtil: '',
      mobilizaPrev: '',
      mobilizaUtili: '',
      dataInicio: '',
      horasPrevDesen: '',
      horasUtilDesenv: '',
      horasPrevImplement: '',
      horasUtilImplement: '',
      clienteId: '',
      clienteDesc: '',
      endereco: '',
      area: '',
      dataConclusao: '',
      concluido: false
    });
  }
  validation() {
    this.registerForm = this.fb.group({
      recursosPrev: ['', [Validators.required]],
      descricao: ['', [Validators.required, Validators.maxLength(100)]],
      codProjeto: ['', [Validators.required, Validators.maxLength(5)]],
      recursosUtil: ['', [Validators.required]],
      mobilizaPrev: ['', [Validators.required]],
      mobilizaUtili: ['', [Validators.required]],
      dataInicio: ['', [Validators.required]],
      horasPrevDesen: ['', [Validators.required]],
      horasUtilDesenv: ['', [Validators.required]],
      horasPrevImplement: ['', [Validators.required]],
      horasUtilImplement: ['', [Validators.required]],
      clienteId: ['', [Validators.required]],
      clienteDesc: [''],
      endereco: [''],
      area: [''],
      dataConclusao: [''],
      concluido: ['']
    });
  }
}
