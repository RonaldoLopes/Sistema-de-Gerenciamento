import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProjetoPie } from '../_models/ProjetoPie';
import { ProjetoUR } from '../_models/ProjetoUR';

@Injectable({
  providedIn: 'root'
})
export class ChartsService {

  teste: any;
  ipAddress: any;
  baseURLPIE = 'http://sgsg:9091/api/projetos/dashPIE';
  baseURLUR = 'http://sgsg:9091/api/projetos/dashUR';

  constructor(private http: HttpClient) {  }


  getAllProjPie(): Observable<ProjetoPie[]> {
    return this.http.get<ProjetoPie[]>(this.baseURLPIE);
  }

  getAllProjUR(): Observable<ProjetoUR[]> {
    return this.http.get<ProjetoUR[]>(this.baseURLUR);
  }
}
