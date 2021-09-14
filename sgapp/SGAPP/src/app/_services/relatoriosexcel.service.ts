import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class RelatoriosexcelService {

  ipAddress: any;
  dateFormatIni: any;
  dateFormatFim: any;
  //codProj: any;
  baseURLDD = 'http://sgsg:9091/api/reldd';
  baseURLCH = 'http://sgsg:9091/api/relch';
  baseURLPE = 'http://sgsg:9091/api/relpe';

  constructor(
    private http: HttpClient,
    private datePipe: DatePipe
    ) { }

   generatePE(dataIni: Date, dataFim: Date, userId: number): void {
     this.dateFormatIni = this.datePipe.transform(dataIni, 'yyyy-MM-dd');
     this.dateFormatFim = this.datePipe.transform(dataFim, 'yyyy-MM-dd');
     const url = `${this.baseURLPE}/${this.dateFormatIni}/${this.dateFormatFim}/${userId}`;
     window.open(url, '_blank');
  }
  generateDD(codP: number, userId: number, dataIni: Date, dataFim: Date): void {
    this.dateFormatIni = this.datePipe.transform(dataIni, 'yyyy-MM-dd');
    this.dateFormatFim = this.datePipe.transform(dataFim, 'yyyy-MM-dd');
    const url = `${this.baseURLDD}/${codP}/${userId}/${this.dateFormatIni}/${this.dateFormatFim}`;
    window.open(url, '_blank');
  }

  generateCH(codP: number, userId: number): void {
    const url = `${this.baseURLCH}/${codP}/${userId}`;
    window.open(url, '_blank');
  }
}
