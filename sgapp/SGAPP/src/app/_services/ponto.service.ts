import { Injectable } from '@angular/core';
import { Ponto } from '../_models/ponto';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DatePipe } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class PontoService {

  ipAddress: any;
  baseURL = 'http://sgsg:9091/api/pe';

  constructor(private http: HttpClient, private datePipe: DatePipe) { }

  getAllPonto(): Observable<Ponto[]> {
    return this.http.get<Ponto[]>(this.baseURL);
  }
   getPontoId(id: number): Observable<Ponto> {
    return this.http.get<Ponto>(`${this.baseURL}/${id}`);//observable
  }

  getPontoIdUser(userId: number, flg: number = 0): Observable<Ponto[]> {
    return this.http.get<Ponto[]>(`${this.baseURL}/${userId}/${flg}`);//observable
  }
  postPonto(ponto: Ponto) {
    return this.http.post(this.baseURL, ponto);
  }

  putPonto(ponto: Ponto) {
    return this.http.put(`${this.baseURL}/${ponto.id}/${ponto.data}/${ponto.projetosId}` , ponto);
  }
  findLancamento(data: Date, userId: number, projetoId: number): Observable<any> {
    let dateFormatIni;
    dateFormatIni = this.datePipe.transform(data, 'yyyy-MM-dd');
    return this.http
    .get<any>(`${this.baseURL}/${dateFormatIni}/${userId}/${projetoId}`);
  }

  putPontoId(ponto: Ponto) {}

  deletePonto(ponto: Ponto) {
    return this.http.delete(`${this.baseURL}/${ponto.id}`);
  }
}
