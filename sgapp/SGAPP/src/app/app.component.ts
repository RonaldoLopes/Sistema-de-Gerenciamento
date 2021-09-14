import { Component, OnInit, HostListener, OnDestroy  } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ChartsService } from './_services/charts.service';
import { ProjetoUR } from './_models/ProjetoUR';
import { ProjetoURGlobal } from './_models/ProjetoURGlobal';
import { ProjetoPie } from './_models/ProjetoPie';
import { ProjetoPieGlobal } from './_models/ProjetoPieGlobal';
import { Observable, interval, Subscription } from 'rxjs';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})


export class AppComponent implements OnInit {

  title = 'Sistema de gerenciamento de projetos';
  private updateSubscription: Subscription;

  constructor(
    private http: HttpClient,
    private router: Router,
    private chartService: ChartsService
  ) {
    if (localStorage.getItem('idUser') === null) {
      localStorage.removeItem('token');
      localStorage.removeItem('access');
      localStorage.removeItem('token');
      localStorage.removeItem('access');
      localStorage.removeItem('username');
      localStorage.removeItem('idUser');
      localStorage.removeItem('role');
    }
  }

  ngOnInit(): void {
    
    this.getDadosChart();

    this.updateSubscription = interval(35000).subscribe(
      () => {
        this.getDadosChart();
      }
    );

    if (localStorage.getItem('token') === null) {
      this.router.navigate(['/user/login']);
    }
  }

  getDadosChart(): void {
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
