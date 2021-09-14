import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Projeto } from '../_models/Projeto';

@Injectable({
  providedIn: 'root'
})
export class ProjetoService {

  ipAddress: any;
  baseURL = 'http://sgsg:9091/api/projetos';
  baseURLC = 'http://sgsg:9091/api/projetos/c';
  baseURLA = 'http://sgsg:9091/api/projetos/al/con';

  constructor(private http: HttpClient) { }

  getAllProjeto(): Observable<Projeto[]> {
    return this.http.get<Projeto[]>(this.baseURL);
  }
  getAllProjetoC(): Observable<Projeto[]> {
    return this.http.get<Projeto[]>(this.baseURLC);
  }
  getProjetoId(id: number): Observable<Projeto> {
    return this.http.get<Projeto>(`${this.baseURL}/${id}`);//observable
  }

  postProjeto(projeto: Projeto) {
    return this.http.post(this.baseURL, projeto);
  }

  putProjeto(projeto: Projeto) {
    return this.http.put(`${this.baseURL}/${projeto.id}` , projeto);
  }

  putProjetoA(projeto: Projeto) {
    return this.http.put(`${this.baseURLA}/${projeto.id}` , projeto);
  }

  deleteProjeto(projeto: Projeto) {
    return this.http.delete(`${this.baseURL}/${projeto.id}`);
  }
}
