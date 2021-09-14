import { Component, OnInit, ViewChild } from '@angular/core';
import {FormBuilder, FormControl, FormGroup} from '@angular/forms';
import { MatTableDataSource, MatPaginator, MatSort, MatSnackBar, MatDialog } from '@angular/material';
import { CadernoHoras } from 'src/app/_models/CadernoHoras';
import { CadernoService } from 'src/app/_services/caderno.service';
import { DialogMessageComponent } from 'src/app/shared/dialog-message/dialog-message.component';
import { ProjetoURGlobal } from 'src/app/_models/ProjetoURGlobal';



@Component({
  selector: 'app-caderno-consulta',
  templateUrl: './caderno-consulta.component.html',
  styleUrls: ['./caderno-consulta.component.scss']
})
export class CadernoConsultaComponent implements OnInit {

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  displayedColumns: string[] = [
    'id', 'data', 'horasDia', 'deslocamento',
    'horasTrab', 'projetosId', 'codProjeto', 'idUser', 'userName',  'Opcoes'
  ];

  dataSource =  new MatTableDataSource();

  caderno: CadernoHoras;
  cadernos: CadernoHoras[];
  roleUSer: any;
  idUsers: any;

  constructor(
    private cadernoService: CadernoService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.idUsers = localStorage.getItem('idUser');
    this.roleUSer = localStorage.getItem('role');
    this.getDados();
  }

  deleteConfirm(dd: CadernoHoras) {

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

        this.cadernoService.deleteCadernoHoras(dd).subscribe(
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
    if (this.roleUSer === 'Admin' || this.roleUSer === 'Gestor') {
      this.cadernoService.getAllCadernoHoras().subscribe(
        (_ch: CadernoHoras[]) => {
          this.cadernos = _ch;
          this.dataSource = new MatTableDataSource(this.cadernos);
          this.dataSource.sort = this.sort;
          this.dataSource.paginator = this.paginator;
        }, error => {
          console.log(error);
        }
      );
    } else {
      this.cadernoService.getDadosDiaUserId(this.idUsers).subscribe(
        (_ch: CadernoHoras[]) => {
          this.cadernos = _ch;
          this.dataSource = new MatTableDataSource(this.cadernos);
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
