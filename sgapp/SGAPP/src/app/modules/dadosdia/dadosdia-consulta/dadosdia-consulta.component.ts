import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MatSnackBar, MatDialog } from '@angular/material';
import { DadosDia } from 'src/app/_models/DadosDia';
import { Projeto } from 'src/app/_models/Projeto';
import { DadosdiaService } from 'src/app/_services/dadosdia.service';
import { DialogMessageComponent } from 'src/app/shared/dialog-message/dialog-message.component';
import { ProjetoURGlobal } from 'src/app/_models/ProjetoURGlobal';


@Component({
  selector: 'app-dadosdia-consulta',
  templateUrl: './dadosdia-consulta.component.html',
  styleUrls: ['./dadosdia-consulta.component.scss']
})
export class DadosdiaConsultaComponent implements OnInit {

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  displayedColumns: string[] = [
    'id', 'data', 'saidaHotel', 'entraFabrica', 'saidaAlmo',
    'retorAlmo', 'saidaLanche', 'retorLanche', 'saidaFabrica', 'chegaHotel',
    'atvDia', 'projetosId', 'projetoCodigo', 'idUser', 'userName', 'Opcoes'
  ];
  dataSource =  new MatTableDataSource();

  dadoDia: DadosDia;
  dadosDia: DadosDia[];

  projeto: Projeto;
  projetos: Projeto[];
  roleUSer: any;
  idUsers: any;

  constructor(
    private dadosDiaService: DadosdiaService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.idUsers = localStorage.getItem('idUser');
    this.roleUSer = localStorage.getItem('role');
    this.getDadosDia();
  }
  deleteConfirm(dd: DadosDia) {

    const dialogRef = this.dialog.open(DialogMessageComponent, {
      width: '500px',
      data: {
        message: 'Esta ação irá excluir apenas os dados do dia sem alterar demais tabelas, deseja continuar? ',
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

        this.dadosDiaService.deleteDadosDia(dd).subscribe(
          () => {
            snack.dismiss();
            this.snackBar.open('Excluído com sucesso', 'Fechar', {
              duration: 2000,
            });
            this.getDadosDia();
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

  getDadosDia() {
    if (this.roleUSer === 'Admin' || this.roleUSer === 'Gestor') {
      this.dadosDiaService.getAllDadosDia().subscribe(
        (_dd: DadosDia[]) => {
          this.dadosDia = _dd;
          this.dataSource = new MatTableDataSource(this.dadosDia);
          this.dataSource.sort = this.sort;
          this.dataSource.paginator = this.paginator;
        }, error => {
          console.log(error);
        }
      );
    } else {
      this.dadosDiaService.getDadosDiaUserId(this.idUsers).subscribe(
        (_dd: DadosDia[]) => {
          this.dadosDia = _dd;
          this.dataSource = new MatTableDataSource(this.dadosDia);
          this.dataSource.sort = this.sort;
          this.dataSource.paginator = this.paginator;
        }, error => {
          console.log(error);
        }
      );
    }
  }
  setUserId(id: any){
    ProjetoURGlobal.idUser = id;
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLocaleLowerCase();
  }

}
