import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DadosDia } from '../_models/DadosDia';
import { Observable } from 'rxjs';
import { DatePipe } from '@angular/common';
import { map, catchError } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class DadosdiaService {

  ipAddress: any;
  baseURL = 'http://sgsg:9091/api/dd';

  constructor(private http: HttpClient, private datePipe: DatePipe) { }

  getAllDadosDia(): Observable<DadosDia[]> {
    return this.http.get<DadosDia[]>(this.baseURL);
  }
  getDadosDiaId(id: number): Observable<DadosDia> {
    return this.http.get<DadosDia>(`${this.baseURL}/${id}`);//observable
  }

  getDadosDiaUserId(userId: number, flg: number = 0): Observable<DadosDia[]> {
    return this.http.get<DadosDia[]>(`${this.baseURL}/${userId}/${flg}`);//observable
  }

  findLancamento(data: Date, userId: number, projetoId: number): Observable<any> {
    let dateFormatIni;
    dateFormatIni = this.datePipe.transform(data, 'yyyy-MM-dd');
    return this.http
    .get<any>(`${this.baseURL}/${dateFormatIni}/${userId}/${projetoId}`);
  }

  postDadosDia(dadosDia: DadosDia) {
    return this.http.post(this.baseURL, dadosDia);
  }

  putDadosDia(dadosDia: DadosDia) {
    return this.http.put(`${this.baseURL}/${dadosDia.id}` , dadosDia);
  }

  deleteDadosDia(dadosDia: DadosDia) {
    return this.http.delete(`${this.baseURL}/${dadosDia.id}`);
  }
}
