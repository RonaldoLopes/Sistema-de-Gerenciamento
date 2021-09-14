import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource, MatDialogRef } from '@angular/material';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Projeto } from 'src/app/_models/Projeto';
import { ProjetoService } from 'src/app/_services/projeto.service';

@Component({
  selector: 'app-consultaprojeto',
  templateUrl: './consultaprojeto.component.html',
  styleUrls: ['./consultaprojeto.component.scss']
})
export class ConsultaprojetoComponent implements OnInit {

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  displayedColumns: string[] = ['id', 'projeto', 'dataInicio', 'Opcoes'];

  dataSource =  new MatTableDataSource();

  //registerForm: FormGroup;

  projeto: Projeto;
  projetos: Projeto[];

  constructor(
    private projetoService: ProjetoService,
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<ConsultaprojetoComponent>
  ) { }

  ngOnInit(): void {
    this.getProjetos();
  }

  getProjetos() {
    this.projetoService.getAllProjetoC().subscribe(
      (_pj: Projeto[]) => {
        this.projetos = _pj;
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

  close(itemSelecionado: Projeto) {
    this.dialogRef.close(itemSelecionado);
  }

}
