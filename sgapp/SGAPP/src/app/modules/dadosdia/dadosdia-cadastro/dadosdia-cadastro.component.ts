import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import { DateAdapter, MatDialog, MatSnackBar } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjetoConsultaComponent } from '../../projeto/projeto-consulta/projeto-consulta.component';
import { ConsultaprojetoComponent } from 'src/app/shared/consultaprojeto/consultaprojeto.component';
import { DadosdiaService } from 'src/app/_services/dadosdia.service';
import { DadosDia } from 'src/app/_models/DadosDia';
import { PontoService } from 'src/app/_services/ponto.service';
import { Ponto } from 'src/app/_models/ponto';
import { ProjetoService } from 'src/app/_services/projeto.service';
import { Projeto } from 'src/app/_models/Projeto';
import * as moment from 'moment';
import { CadernoService } from 'src/app/_services/caderno.service';
import { CadernoHoras } from 'src/app/_models/CadernoHoras';
@Component({
  selector: 'app-dadosdia-cadastro',
  templateUrl: './dadosdia-cadastro.component.html',
  styleUrls: ['./dadosdia-cadastro.component.scss']
})
export class DadosdiaCadastroComponent implements OnInit {

  registerForm: FormGroup;
  modoSalvar = 'post';
  disableFields: boolean;
  dadosDia: DadosDia;
  ponto: Ponto;
  cadernoH: CadernoHoras;
  hd: any;
  ht: any;
  desloc: any;
  idUsers: any;
  horasInter: boolean;
  totalHorasInter: any;
  codUser: number;
  codUserFlag: number;
  retConsulData: any;
  flagProjetoId: any;

  constructor(
    private fb: FormBuilder,
    private dadosDiaService: DadosdiaService,
    private pontoService: PontoService,
    private cadernoService: CadernoService,
    private projetoService: ProjetoService,
    private dateAdapter: DateAdapter<Date>,
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private router: ActivatedRoute,
    private route: Router,
  ) {
    this.dateAdapter.setLocale('pt-br');
    this.route = route;
    this.disableFields = false;
    moment.locale('pt-br');
    moment().format('LT');
  }

  ngOnInit(): void {
    this.horasInter = false;
    const idDD = +this.router.snapshot.paramMap.get('id');
    this.validation();

    this.idUsers = localStorage.getItem('idUser');

    if (idDD === 0) {
      this.modoSalvar = 'post';
    } else {
      this.modoSalvar = 'put';
      this.carregarDD(idDD);
    }
  }

  caderno() {

    if  (!this.horasInter) {
      var saiAlmo = this.registerForm.get('saidaAlmo').value;
      var enterFabric = this.registerForm.get('entraFabrica').value;
      var retAlmo = this.registerForm.get('retorAlmo').value;
      var saiHotel = this.registerForm.get('saidaHotel').value;
      var chegaHote = this.registerForm.get('chegaHotel').value;
      var saiFabric =  this.registerForm.get('saidaFabrica').value;

      saiAlmo = saiAlmo + ':00';
      enterFabric = enterFabric + ':00';
      retAlmo = retAlmo + ':00';
      saiHotel = saiHotel + ':00';
      chegaHote = chegaHote + ':00';
      saiFabric = saiFabric + ':00';

      var dateSaiAlmo = new Date('01/01/2000 ' + saiAlmo ).getTime();
      var dateEnterFabric = new Date('01/01/2000 ' +  enterFabric).getTime();

      var dateRetAlmo = new Date('01/01/2000 ' + retAlmo ).getTime();
      var dateSaiHotel = new Date('01/01/2000 ' +  saiHotel).getTime();

      var dateChegaHote = new Date('01/01/2000 ' + chegaHote ).getTime();
      var dateSaiFabric = new Date('01/01/2000 ' +  saiFabric).getTime();

      var horasDia = (dateSaiAlmo - dateEnterFabric) + (dateSaiFabric - dateRetAlmo);
      var desloca = (dateEnterFabric - dateSaiHotel) + (dateChegaHote - dateSaiFabric);
      var horasTrab = horasDia - desloca;

      var minHorasDia = Math.floor(horasDia / 60000);
      var hrsHoraDia = Math.floor(minHorasDia / 60);
      minHorasDia = minHorasDia % 60;
      hrsHoraDia = hrsHoraDia % 24;
      this.hd = hrsHoraDia + ':' + minHorasDia;

      var minDesloc = Math.floor(desloca / 60000);
      var hrsDesloc = Math.floor(minDesloc / 60);
      minDesloc = minDesloc % 60;
      hrsDesloc = hrsDesloc % 24;
      this.desloc = hrsDesloc + ':' + minDesloc;

      var minHorasTrab = Math.floor(horasTrab / 60000);
      var hrsHorasTrab = Math.floor(minHorasTrab / 60);
      minHorasTrab = minHorasTrab % 60;
      hrsHorasTrab = hrsHorasTrab % 24;
      this.ht = hrsHorasTrab + ':' + minHorasTrab;
      //var msec = date1 - date2;
      //var mins = Math.floor(msec / 60000);
      //var hrs = Math.floor(mins / 60);
    // var days = Math.floor(hrs / 24);
      //var yrs = Math.floor(days / 365);
    //  mins = mins % 60;
    //  hrs = hrs % 24;
    } 
  }

  totInterno() {
    if (this.horasInter) {
      this.ht = '';
      this.ht = this.totalHorasInter;
    }

  }
  carregarDD(id: number) {
    this.dadosDiaService.getDadosDiaId(id).subscribe(
      // tslint:disable-next-line: variable-name
      (_dd: DadosDia) => {
        this.dadosDia = Object.assign({}, _dd);
        this.codUser = this.dadosDia.userId;
        this.registerForm.patchValue(_dd);
        this.getProjetoId(this.dadosDia.projetosId);
        this.disableFields = true;
        this.caderno();
      }
    );
  }
  getProjetoId(id: number) {
    this.projetoService.getProjetoId(id).subscribe(
      (_pj: Projeto) => {
        this.registerForm.get('projeto').setValue(_pj.codProjeto);
        const dataIni = new Date(_pj.dataInicio);
        this.registerForm.get('dataInicio').setValue(dataIni.toLocaleDateString());
      }
    );
  }

  salvarAlteracao() {

    if (this.registerForm.valid) {
      if (this.modoSalvar === 'post') {
        this.dadosDia = Object.assign({}, this.registerForm.value);
        this.dadosDia.userId = this.idUsers;
        this.dadosDiaService.postDadosDia(this.dadosDia).subscribe(
          (novo: DadosDia) => {
            if (!this.horasInter) {
              this.savePoint();
            } else {
              this.saveCH();
            }
          }, error => {
            this.snackBar.open('Algo está errado: ' + error, 'Fechar', {
              duration: 3000
            });
            console.log(error);
          }
        );
      } else {
        this.dadosDia = Object.assign({id: this.dadosDia.id}, this.registerForm.value);
        if (this.codUser === this.idUsers) {
          this.dadosDia.userId = this.idUsers;
        } else {
          this.dadosDia.userId = this.codUser;
        }
        this.dadosDiaService.putDadosDia(this.dadosDia).subscribe(
          () => {
            if (!this.horasInter) {
              this.savePoint();
            } else {
              this.saveCH();
            }
          }, error => {
            this.snackBar.open('Algo está errado: ' + error, 'Fechar', {
              duration: 3000
            });
            console.log(error);
          }
        );
      }
    } else {
      this.snackBar.open('Algo não está certo', 'Fechar', {
        duration: 2000
      });
    }
  }

  savePoint() {
    if (this.modoSalvar === 'post') {
     this.pontoService.postPonto(this.dadosDia).subscribe(
       () => {
        this.saveCH();
       }, error => {
        this.snackBar.open('Algo está errado: ' + error, 'Fechar', {
          duration: 3000
        });
        console.log(error);
      }
     );
    } else {
      this.ponto = Object.assign({id: this.dadosDia.id, data: this.dadosDia.data, pId: this.dadosDia.projetosId}, this.registerForm.value);
      if (this.codUser === this.idUsers) {
        this.ponto.userId = this.idUsers;
      } else {
        this.ponto.userId = this.codUser;
      }
      this.pontoService.putPonto(this.ponto).subscribe(
        () => {
          this.saveCH();
        }, error => {
          this.snackBar.open('Algo está errado: ' + error, 'Fechar', {
            duration: 3000
          });
          console.log(error);
        }
      );
    }
  }
  saveCH() {
    if (this.modoSalvar === 'post') {
      this.cadernoService.postoCadernoG(this.dadosDia).subscribe(
        () => {
          this.snackBar.open('Operação efetuada com sucesso', 'Fechar', {
            duration: 2000
          });
          this.formReset();
        }, error => {
          this.snackBar.open('Algo está errado: ' + error, 'Fechar', {
            duration: 3000
          });
          console.log(error);
        }
      );
    } else {
      this.dadosDia = Object.assign({id: this.dadosDia.projetosId, data: this.dadosDia.data}, this.registerForm.value);
      if (this.codUser === this.idUsers) {
        this.dadosDia.userId = this.idUsers;
      } else {
        this.dadosDia.userId = this.codUser;
      }
      this.cadernoService.putCadernoHorasG(this.dadosDia).subscribe(
        () => {
          this.snackBar.open('Operação efetuada com sucesso', 'Fechar', {
            duration: 2000
          });
          this.modoSalvar = 'post';
          this.route.navigate(['/', 'dadosdiac']);
        }, error => {
          this.snackBar.open('Algo está errado: ' + error, 'Fechar', {
            duration: 3000
          });
          console.log(error);
        }
      );
    }
  }

  findDataLancamento() {

    if (this.registerForm.valid) {

      this.flagProjetoId = this.registerForm.get('projetosId').value;
      if (this.modoSalvar === 'post') {
        if (this.registerForm.get('data').value !== '') {
          if (this.codUser === this.idUsers) {
            this.codUserFlag = this.codUser;
          } else {
            this.codUserFlag = this.idUsers;
          }
          this.dadosDiaService.findLancamento(this.registerForm.get('data').value, this.codUserFlag, this.flagProjetoId).subscribe(
            res => {
              if (res === null) {
                this.salvarAlteracao();
              } else {
                this.snackBar.open('Já existe um lançamento com esta data para este usuário neste projeto, por favor opte por alterar os dados', 'Fechar', {
                  duration: 8000
                });
                this.formReset();
                return;
              }
            },
            err => console.log('erro')
          );
        }
      } else {
        this.salvarAlteracao();
      }
    }
  }
  formReset() {
    this.hd = '';
    this.ht = '';
    this.desloc = '';
    this.registerForm.setValue({
      data: '',
      saidaHotel: '',
      entraFabrica: '',
      saidaAlmo: '',
      retorAlmo: '',
      saidaLanche: '',
      retorLanche: '',
      saidaFabrica: '',
      chegaHotel: '',
      atvDia: '',
      projetosId: '',
      projeto: '',
      dataInicio: '',
      interno: false,
      horasInterno: ''
    });
  }

  carregarProjeto() {
    const dialoagRef = this.dialog.open(ConsultaprojetoComponent, {
      width: '920px',
      height: '560px'
    });

    dialoagRef.afterClosed().subscribe(result => {
      this.registerForm.get('projetosId').setValue(result.id);
      this.registerForm.get('projeto').setValue(result.codProjeto);
      let dataIni = new Date(result.dataInicio);
      this.registerForm.get('dataInicio').setValue(dataIni.toLocaleDateString());
    });
  }
  cancelSave() {
    this.route.navigate(['/']);
  }
  validation() {
    this.registerForm = this.fb.group({
      data: ['', [Validators.required]],
      saidaHotel: [''],
      entraFabrica: [''],
      saidaAlmo: [''],
      retorAlmo: [''],
      saidaLanche: [''],
      retorLanche: [''],
      saidaFabrica: [''],
      chegaHotel: [''],
      atvDia: [''],
      projetosId: ['', [Validators.required]],
      projeto: [''],
      dataInicio: [''],
      interno: [''],
      horasInterno: ['']
    });
  }

}
