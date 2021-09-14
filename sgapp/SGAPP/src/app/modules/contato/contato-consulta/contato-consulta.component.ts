import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MatSnackBar, MatDialog } from '@angular/material';
import { Contato } from 'src/app/_models/Contato';
import { Cliente } from 'src/app/_models/Cliente';
import { ContatoService } from 'src/app/_services/contato.service';
import { DialogMessageComponent } from 'src/app/shared/dialog-message/dialog-message.component';


@Component({
  selector: 'app-contato-consulta',
  templateUrl: './contato-consulta.component.html',
  styleUrls: ['./contato-consulta.component.scss']
})


export class ContatoConsultaComponent implements OnInit {

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  displayedColumns: string[] = [
    'id', 'nome', 'emailPrinc', 'emailSec', 'fonePrinc', 'foneSecun', 'clienteId', 'clienteNome', 'Opcoes'
  ];
  dataSource =  new MatTableDataSource();

  cliente: Cliente;
  clientes: Cliente[];

  contatoCliente: Contato;
  contatoClientes: Contato[];

  constructor(
    private contatoService: ContatoService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {

    this.getContato();
  }

  deleteConfirm(contato: Contato) {

    const dialogRef = this.dialog.open(DialogMessageComponent, {
      width: '500px',
      data: {
        message: 'Deseja excluir o cliente ID ' + contato.id + ' nome ' + contato.nome + '?',
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

        this.contatoService.deleteContato(contato).subscribe(
          () => {
            snack.dismiss();
            this.snackBar.open('Excluído com sucesso', 'Fechar', {
              duration: 2000,
            });
            this.getContato();
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

  getContato() {
    this.contatoService.getAllContato().subscribe(
      (_contato: Contato[]) => {
        this.contatoClientes = _contato;
        this.dataSource = new MatTableDataSource(this.contatoClientes);
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
