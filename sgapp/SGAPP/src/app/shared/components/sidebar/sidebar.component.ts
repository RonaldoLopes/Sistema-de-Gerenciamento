import { Component, OnInit } from '@angular/core';
import { ProjetoUR } from 'src/app/_models/ProjetoUR';
import { ProjetoURGlobal } from 'src/app/_models/ProjetoURGlobal';
import { ProjetoPie } from 'src/app/_models/ProjetoPie';
import { ProjetoPieGlobal } from 'src/app/_models/ProjetoPieGlobal';
import { ChartsService } from 'src/app/_services/charts.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

  nameUser: string;

  constructor(
    private chartService: ChartsService
  ) { }

  ngOnInit() {
    this.nameUser = localStorage.getItem('username');
    //sessionStorage.getItem('username');
  }

  reloadData() {
    this.chartService.getAllProjUR().subscribe(
      (_pj: ProjetoUR[]) => {
        ProjetoURGlobal.nome = _pj[0].name;
        ProjetoURGlobal.rPrevistos = _pj[0].rPrevistos;
        ProjetoURGlobal.rUtilizados = _pj[0].rUtilizados;
        ProjetoURGlobal.hPrevistasD = _pj[0].hPrevistasD;
        ProjetoURGlobal.hPrevistasI = _pj[0].hPrevistasI;
        ProjetoURGlobal.hUtilizadasD = _pj[0].hUtilizadasD;
        ProjetoURGlobal.hUtilizadasI = _pj[0].hUtilizadasI;
        ProjetoURGlobal.mPrevistos = _pj[0].mPrevistos;
        ProjetoURGlobal.mUtilizados = _pj[0].mUtilizados;
      }
    );

    this.chartService.getAllProjPie().subscribe(
      (_pj: ProjetoPie[]) => {
        ProjetoPieGlobal.data = _pj;
      }
    );
  }

}
