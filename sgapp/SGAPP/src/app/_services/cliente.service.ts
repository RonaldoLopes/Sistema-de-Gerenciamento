import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cliente } from '../_models/Cliente';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  ipAddress: any;
  baseURL = 'http://sgsg:9091/api/clientes';

  constructor(private http: HttpClient) { }

  getAllCliente(): Observable<Cliente[]> {
    return this.http.get<Cliente[]>(this.baseURL);
  }

  getClienteId(id: number): Observable<Cliente> {
    return this.http.get<Cliente>(`${this.baseURL}/${id}`);//observable
  }

  postCliente(cliente: Cliente) {
    return this.http.post(this.baseURL, cliente);
  }

  putCliente(cliente: Cliente) {
    return this.http.put(`${this.baseURL}/${cliente.id}` , cliente);
  }

  deleteCliente(cliente: Cliente) {
    return this.http.delete(`${this.baseURL}/${cliente.id}`);
  }
}
