import { Component, OnInit, ViewChild } from '@angular/core';
import {FormBuilder, FormControl, FormGroup} from '@angular/forms';
import { MatTableDataSource, MatPaginator, MatSort } from '@angular/material';
import { ClienteService } from 'src/app/_services/cliente.service';
import { Cliente } from 'src/app/_models/Cliente';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
  selector: 'app-clienteconsulta',
  templateUrl: './clienteconsulta.component.html',
  styleUrls: ['./clienteconsulta.component.scss']
})
export class ClienteconsultaComponent implements OnInit {

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  displayedColumns: string[] = ['id', 'clientes', 'endereco', 'area', 'Opcoes'];

  dataSource =  new MatTableDataSource();

  registerForm: FormGroup;

  cliente: Cliente;
  clientes: Cliente[];

  constructor(
    private clienteService: ClienteService,
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<ClienteconsultaComponent>
  ) {
    this.registerForm = this.fb.group({});
  }

  ngOnInit(): void {
    this.getClientes();
  }

  closeTeste(itemSelecionado: Cliente) {
    //this.registerForm.get('cli')
    this.dialogRef.close(itemSelecionado);
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
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLocaleLowerCase();
  }

}
