import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class VisitorsService {

  ipAddress: any;

  constructor(private http: HttpClient) { }

  public getIpAddress() {
    return this.http.get('http://api.ipify.org/?format=json');
  }
}
