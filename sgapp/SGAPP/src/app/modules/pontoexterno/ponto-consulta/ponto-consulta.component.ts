import { Component, OnInit, ViewChild } from '@angular/core';
import {FormBuilder, FormControl, FormGroup} from '@angular/forms';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog, MatSnackBar } from '@angular/material';
import { Ponto } from 'src/app/_models/ponto';
import { Projeto } from 'src/app/_models/Projeto';
import { PontoService } from 'src/app/_services/ponto.service';
import { DialogMessageComponent } from 'src/app/shared/dialog-message/dialog-message.component';


@Component({
  selector: 'app-ponto-consulta',
  templateUrl: './ponto-consulta.component.html',
  styleUrls: ['./ponto-consulta.component.scss']
})
export class PontoConsultaComponent implements OnInit {

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  displayedColumns: string[] = [
    'id', 'data', 'entraFabrica', 'saidaAlmo',
    'retorAlmo', 'saidaFabrica',
    'atvDia', 'projetosId', 'projetoCodigo', 'idUser', 'userName', 'Opcoes'
  ];
  dataSource =  new MatTableDataSource();

  ponto: Ponto;
  pontos: Ponto[];

  projeto: Projeto;
  projetos: Projeto[];

  roleUSer: any;
  idUsers: any;

  constructor(
    private pontoService: PontoService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.idUsers = localStorage.getItem('idUser');
    this.roleUSer = localStorage.getItem('role');
    this.getDados();
  }

  deleteConfirm(dd: Ponto) {

    const dialogRef = this.dialog.open(DialogMessageComponent, {
      width: '500px',
      data: {
        message: 'Esta ação irá excluir os dados do ponto, deseja continuar? ',
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

        this.pontoService.deletePonto(dd).subscribe(
          () => {
            snack.dismiss();
            this.snackBar.open('Excluído com sucesso', 'Fechar', {
              duration: 2000,
            });
            this.getDados();
          }, erro => {
            snack.dismiss();
            this.snackBar.open('Erro ao excluir', 'Fechar', {
              duration: 2000,
            });
          }
        );
      } else {
        snack.dismiss();
      }
    });
  }

  getDados() {
    if (this.roleUSer === 'RUser' || this.roleUSer === 'Gestor') {
      this.pontoService.getPontoIdUser(this.idUsers).subscribe(
        (_pp: Ponto[]) => {
          this.pontos = _pp;
          this.dataSource = new MatTableDataSource(this.pontos);
          this.dataSource.sort = this.sort;
          this.dataSource.paginator = this.paginator;
        }, error => {
          console.log(error);
        }
      );
    } else if (this.roleUSer === 'Admin' || this.roleUSer === 'RH') {
      this.pontoService.getAllPonto().subscribe(
        (_pp: Ponto[]) => {
          this.pontos = _pp;
          this.dataSource = new MatTableDataSource(this.pontos);
          this.dataSource.sort = this.sort;
          this.dataSource.paginator = this.paginator;
        }, error => {
          console.log(error);
        }
      );
    }
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLocaleLowerCase();
  }
}
