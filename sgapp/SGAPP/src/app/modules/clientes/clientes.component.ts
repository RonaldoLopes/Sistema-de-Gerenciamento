import { Component, OnInit, ViewChild } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import { MatTableDataSource, MatPaginator, MatSnackBar, MatSort, MatDialog } from '@angular/material';
import { Cliente } from 'src/app/_models/Cliente';
import { ClienteService } from 'src/app/_services/cliente.service';

import { HttpClient } from '@angular/common/http';
import { CepService } from 'src/app/_services/cep.service';
import { EstadoBr } from 'src/app/_models/EstadoBr';
import { Cidade } from 'src/app/_models/Cidade';
import { DialogMessageComponent } from 'src/app/shared/dialog-message/dialog-message.component';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-clientes',
  templateUrl: './clientes.component.html',
  styleUrls: ['./clientes.component.scss']
})
export class ClientesComponent implements OnInit {

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  displayedColumns: string[] = [
    'id', 'clientes', 'cep', 'endereco', 'cidade', 'bairro', 'uf', 'numero', 'area', 'Opcoes'
  ];
  dataSource =  new MatTableDataSource();


  registerForm: FormGroup;

  modoSalvar = 'post';

  cliente: Cliente;
  clientes: Cliente[];

  estados: EstadoBr[];
  cidades: Cidade[];

  constructor(
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    private clienteService: ClienteService,
    private cepService: CepService,
    private http: HttpClient
  ) {}

  ngOnInit() {
    this.validation();
    this.getClientes();
  }

  getClientes() {
    this.clienteService.getAllCliente().subscribe(
      (_clientes: Cliente[]) => {
        this.clientes = _clientes;
        this.dataSource = new MatTableDataSource(this.clientes);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      }, error => {
        console.log(error);
      }
    );
  }

  salvarAlteracao() {
    if (this.registerForm.valid) {
      if (this.modoSalvar === 'post') {
        this.cliente = Object.assign({}, this.registerForm.value);
        this.clienteService.postCliente(this.cliente).subscribe(
          (novoCliente: Cliente) => {
            this.resetForm();
            this.getClientes();
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
        this.cliente = Object.assign({id: this.cliente.id}, this.registerForm.value);
        this.clienteService.putCliente(this.cliente).subscribe(
          (alterCliente: Cliente) => {
            this.snackBar.open('Operação efetuada com sucesso', 'Fechar', {
              duration: 2000
            });
            this.resetForm();
            this.getClientes();
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

  editarCliente(cliente: Cliente) {
    this.modoSalvar = 'put';
    this.cliente = cliente;
    this.registerForm.patchValue(cliente);
  }

  deleteConfirm(cliente: Cliente) {

    const dialogRef = this.dialog.open(DialogMessageComponent, {
      width: '500px',
      data: {
        message: 'Deseja excluir o cliente ID ' + cliente.id + ' nome ' + cliente.clientes + '?',
        buttonText: {
          delete: 'Sim',
          cancel: 'Não'
        }
      }
    });

    const snack = this.snackBar.open('Sim: exclui o item selecionado');
    dialogRef.afterClosed().subscribe(result => {
      if (result === 'delete') {
        snack.dismiss();
        this.snackBar.open('Excluindo...', 'Fechar', {
          duration: 2000
        });

        this.clienteService.deleteCliente(cliente).subscribe(
          () => {
            snack.dismiss();
            this.snackBar.open('Excluído com sucesso', 'Fechar', {
              duration: 2000,
            });
            this.resetForm();
            this.getClientes();
          }, erro => {
            snack.dismiss();
            this.snackBar.open('Erro ao excluir', 'Fechar', {
              duration: 2000,
            });
          }
        );
      }
    });
  }

  validation() {
    this.registerForm = this.fb.group({
      clientes: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(45)]],
      endereco: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(45)]],
      area: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(45)]],
      cep: ['', [Validators.maxLength(20)]],
      cidade: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(45)]],
      bairro: ['', [Validators.maxLength(45)]],
      estado: ['', [Validators.maxLength(2)]],
      numero: ['', [Validators.maxLength(10)]]
    });
  }

  resetForm() {
    this.registerForm.setValue({
      clientes: '',
      endereco: '',
      area: '',
      cep: '',
      cidade: '',
      bairro: '',
      estado: '',
      numero: ''
    });
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLocaleLowerCase();
  }

  getCep() {
    this.cepService.consultaCEP(this.registerForm.get('cep').value)
      .subscribe(dados => this.populaDadosForm(dados));
  }

  populaDadosForm(dados) {
    this.registerForm.patchValue({
      endereco: dados.logradouro,
      bairro: dados.bairro,
      cidade: dados.localidade,
      estado: dados.uf
    });
  }
}
