import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MatSnackBar, MatDialog, DateAdapter, MatDialogRef } from '@angular/material';
import { Projeto } from 'src/app/_models/Projeto';
import { ProjetoService } from 'src/app/_services/projeto.service';
import { DialogMessageComponent } from 'src/app/shared/dialog-message/dialog-message.component';
import { Router } from '@angular/router';



@Component({
  selector: 'app-projeto-consulta',
  templateUrl: './projeto-consulta.component.html',
  styleUrls: ['./projeto-consulta.component.scss']
})
export class ProjetoConsultaComponent implements OnInit {

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  displayedColumns: string[] = [
    'id', 'codProjeto', 'descricao', 'recursosPrev', 'recursosUtil', 'mobilizaPrev',
    'mobilizaUtili', 'dataInicio', 'horasPrevDesen', 'horasUtilDesenv', 'horasPrevImplement',
    'horasUtilImplement', 'clienteId', 'clienteNome', 'concluido', 'Opcoes'
  ];
  dataSource =  new MatTableDataSource();

  projeto: Projeto;
  projetos: Projeto[];

  constructor(
    private projetoService: ProjetoService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    private dateAdapter: DateAdapter<Date>,
    private route: Router,
  ) {
    this.dateAdapter.setLocale('pt-br');
  }

  ngOnInit(): void {
    this.getProjeto();
  }

  editar(projeto: Projeto) {
    if (projeto.concluido === true) {
      const dialogRef = this.dialog.open(DialogMessageComponent, {
        width: '500px',
        data: {
          // tslint:disable-next-line: max-line-length
          message: 'O projeto ' + projeto.codProjeto + ' já está concluído, para alterar, será necessário reabrir, deseja reabrir? ' ,
          buttonText: {
            delete: 'Sim',
            cancel: 'Não'
          }
        }
      });

      dialogRef.afterClosed().subscribe(result => {
        if (result === 'delete') {
          this.projetoService.putProjetoA(projeto).subscribe(
            () => {
              this.route.navigate(['/projetosc', projeto.id, 'edit']);
            }, error => {
              this.snackBar.open('Algo está errado: ' + error, 'Fechar', {
                duration: 3000
              });
              console.log(error);
            }
          );
        }
      });
    } else {
      this.route.navigate(['/projetosc', projeto.id, 'edit']);
    }
  }

  deleteConfirm(projeto: Projeto) {

    const dialogRef = this.dialog.open(DialogMessageComponent, {
      width: '500px',
      data: {
        message: 'Deseja excluir o projeto ID ' + projeto.id + ' Código ' + projeto.codProjeto + '?',
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

        this.projetoService.deleteProjeto(projeto).subscribe(
          () => {
            snack.dismiss();
            this.snackBar.open('Excluído com sucesso', 'Fechar', {
              duration: 2000,
            });
            this.getProjeto();
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

  getProjeto() {
    this.projetoService.getAllProjeto().subscribe(
      (_projeto: Projeto[]) => {
        this.projetos = _projeto;
        this.dataSource = new MatTableDataSource(this.projetos);
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
  highlight(element): boolean {
    // tslint:disable-next-line: no-conditional-assignment
    if (element === true) {
      return true;
    } else {
      return false;
    }
  }

}
