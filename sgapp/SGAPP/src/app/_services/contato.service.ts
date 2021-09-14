import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Contato } from '../_models/Contato';

@Injectable({
  providedIn: 'root'
})
export class ContatoService {

  ipAddress: any;
  baseURL = 'http://sgsg:9091/api/ct';

  constructor(private http: HttpClient) { }

  getAllContato(): Observable<Contato[]> {
    return this.http.get<Contato[]>(this.baseURL);
  }
  getContatoId(id: number): Observable<Contato> {
    return this.http.get<Contato>(`${this.baseURL}/${id}`);//observable
  }

  postContato(contato: Contato) {
    return this.http.post(this.baseURL, contato);
  }

  putContato(contato: Contato) {
    return this.http.put(`${this.baseURL}/${contato.id}` , contato);
  }

  deleteContato(contato: Contato) {
    return this.http.delete(`${this.baseURL}/${contato.id}`);
  }
}
