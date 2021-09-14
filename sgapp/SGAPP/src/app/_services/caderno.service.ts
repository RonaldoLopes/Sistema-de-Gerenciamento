import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CadernoHoras } from '../_models/CadernoHoras';
import { Observable } from 'rxjs';
import { DadosDia } from '../_models/DadosDia';
import { DatePipe } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class CadernoService {

  ipAddress: any;
  baseURL = 'http://sgsg:9091/api/ch';
  baseURL2 = 'http://sgsg:9091/api/ch/dd';

  constructor(private http: HttpClient, private datePipe: DatePipe) { }

  getAllCadernoHoras(): Observable<CadernoHoras[]> {
    return this.http.get<CadernoHoras[]>(this.baseURL);
  }
  getCadernoHorasId(id: number): Observable<CadernoHoras> {
    return this.http.get<CadernoHoras>(`${this.baseURL}/${id}`);//observable
  }
  getDadosDiaUserId(userId: number, flg: number = 0): Observable<CadernoHoras[]> {
    return this.http.get<CadernoHoras[]>(`${this.baseURL}/${userId}/${flg}`);//observable
  }

  postCadernoHoras(cadernoHoras: CadernoHoras) {
    return this.http.post(this.baseURL, cadernoHoras);
  }

  postoCadernoG(dadosDiario: DadosDia) {
    return this.http.post(this.baseURL2, dadosDiario);
  }
  putCadernoHoras(cadernoHoras: CadernoHoras) {
    return this.http.put(`${this.baseURL}/${cadernoHoras.id}` , cadernoHoras);
  }

  putCadernoHorasG(dadosDiario: DadosDia) {
    return this.http.put(`${this.baseURL}/${dadosDiario.projetosId}/${dadosDiario.data}` , dadosDiario);
  }
  findLancamento(data: Date, userId: number, projetoId: number): Observable<any> {
    let dateFormatIni;
    dateFormatIni = this.datePipe.transform(data, 'yyyy-MM-dd');
    return this.http
    .get<any>(`${this.baseURL}/${dateFormatIni}/${userId}/${projetoId}`);
  }

  deleteCadernoHoras(cadernoHoras: CadernoHoras) {
    return this.http.delete(`${this.baseURL}/${cadernoHoras.id}`);
  }
}
