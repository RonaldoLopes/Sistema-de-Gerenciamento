import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import { ClienteService } from 'src/app/_services/cliente.service';
import { Cliente } from 'src/app/_models/Cliente';
import { ContatoService } from 'src/app/_services/contato.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { ClienteconsultaComponent } from 'src/app/shared/clienteconsulta/clienteconsulta.component';
import { Contato } from 'src/app/_models/Contato';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-contato-cadastro',
  templateUrl: './contato-cadastro.component.html',
  styleUrls: ['./contato-cadastro.component.scss']
})
export class ContatoCadastroComponent implements OnInit {

  registerForm: FormGroup;
  clientes: Cliente[];
  clientesConsulta: Cliente;

  contatoCliente: Contato;
  contatoClientes: Contato[];

  modoSalvar = 'post';

  dadosCliente = {
    localCliente: '',
    areaCliente: ''
  };

  constructor(
    private fb: FormBuilder,
    private clienteService: ClienteService,
    private contatoService: ContatoService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private router: ActivatedRoute,
    private route: Router
  ) {
    this.route = route;
   }

  ngOnInit(): void {

    const idCC = +this.router.snapshot.paramMap.get('id');
    this.validation();
    this.getClientes();

    if (idCC === 0) {
      this.modoSalvar = 'post';
    } else {
      this.modoSalvar = 'put';
      this.carregarCC(idCC);
    }
  }

  carregarCC(id: number) {
    this.contatoService.getContatoId(id).subscribe(
      // tslint:disable-next-line: variable-name
      (_cc: Contato) => {
        this.contatoCliente = Object.assign({}, _cc);
        this.registerForm.patchValue(_cc);
        this.getClienteId(this.contatoCliente.clienteId);
      }
    );
  }
  getClientes() {
    this.clienteService.getAllCliente().subscribe(
      // tslint:disable-next-line: variable-name
      (_clientes: Cliente[]) => {
        this.clientes = _clientes;
      }, error => {
        console.log(error);
      }
    );
  }
  getClienteId(id: number) {
    this.clienteService.getClienteId(id).subscribe(
      // tslint:disable-next-line: variable-name
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

  salvarAlteracao() {
    if (this.registerForm.valid) {
      if (this.modoSalvar === 'post') {
        this.contatoCliente = Object.assign({}, this.registerForm.value);
        this.contatoService.postContato(this.contatoCliente).subscribe(
          (novo: Contato) => {
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
        this.contatoCliente = Object.assign({id: this.contatoCliente.id}, this.registerForm.value);
        this.contatoService.putContato(this.contatoCliente).subscribe(
          (alterCliente: Cliente) => {
            this.snackBar.open('Operação efetuada com sucesso', 'Fechar', {
              duration: 2000
            });
            this.route.navigate(['/', 'contatosc']);
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

  resetForm() {
    this.registerForm.setValue({
      nome: '',
      emailPrinc: '',
      emailSec: '',
      fonePrinc: '',
      foneSecun: '',
      clienteId: '',
      clienteDesc: '',
      endereco: '',
      area: ''
    });
  }
  validation() {
    this.registerForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(45)]],
      emailPrinc: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(65), Validators.email]],
      emailSec: [''],
      fonePrinc:  ['', [Validators.required, Validators.maxLength(16)]],
      foneSecun: [''],
      clienteId: ['', [Validators.required]],
      clienteDesc: [''],
      endereco: [''],
      area: ['']
    });
  }

}
