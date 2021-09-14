import { Component, OnInit } from '@angular/core';
import { DateAdapter, MatSnackBar } from '@angular/material';
import { RelatoriosexcelService } from 'src/app/_services/relatoriosexcel.service';
import { ProjetoService } from 'src/app/_services/projeto.service';
import { Projeto } from 'src/app/_models/Projeto';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-relatorios',
  templateUrl: './relatorios.component.html',
  styleUrls: ['./relatorios.component.scss']
})
export class RelatoriosComponent implements OnInit {

  userId: number;
  projetos: Projeto[];
  registerForm: FormGroup;
  dateIni: Date;
  dateFim: Date;
  codProj: any;

  constructor(
    private fb: FormBuilder,
    private dateAdapter: DateAdapter<Date>,
    private reexcelService: RelatoriosexcelService,
    private projetoService: ProjetoService,
    private snackBar: MatSnackBar
  ) {
    this.dateAdapter.setLocale('pt-br');
  }

  ngOnInit(): void {
    this.validation();
    this.userId = Number(localStorage.getItem('idUser'));
    this.getProjeto();
  }

  generatePE(): void {
    this.dateIni = this.registerForm.get('dataIniPE').value;
    this.dateFim = this.registerForm.get('dataFimPE').value;
    this.reexcelService.generatePE(this.dateIni, this.dateFim, this.userId);
  }
  generateDD(): void {
    this.dateIni = this.registerForm.get('dataIniDD').value;
    this.dateFim = this.registerForm.get('dataFimDD').value;
    this.codProj = this.registerForm.get('projetoDD').value;
    this.reexcelService.generateDD(this.codProj, this.userId, this.dateIni, this.dateFim);
  }

  generateCH(): void {
    this.codProj = this.registerForm.get('projetoCH').value;
    this.reexcelService.generateCH(this.codProj, this.userId);
  }

  getProjeto() {
    this.projetoService.getAllProjeto().subscribe(
      (_projeto: Projeto[]) => {
        this.projetos = _projeto;
      }, error => {
        console.log(error);
      }
    );
  }
  validation() {
    this.registerForm = this.fb.group({
      dataIniPE: [''],
      dataFimPE: [''],

      dataIniDD: [''],
      dataFimDD: [''],
      projetoDD: [''],

      projetoCH: [''],
    });
  }
}
