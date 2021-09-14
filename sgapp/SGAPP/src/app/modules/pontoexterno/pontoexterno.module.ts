import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatGridListModule } from '@angular/material/grid-list';
import {  MatDividerModule,  MatPaginatorModule, MatTableModule, MatButtonModule, MatIconModule } from '@angular/material';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatRadioModule } from '@angular/material/radio';

import { PontoCadastroComponent } from './ponto-cadastro/ponto-cadastro.component';
import { PontoConsultaComponent } from './ponto-consulta/ponto-consulta.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [PontoCadastroComponent, PontoConsultaComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatDividerModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    FlexLayoutModule,
    MatGridListModule,
    MatTableModule,
    MatPaginatorModule,
    MatDatepickerModule,
    MatRadioModule,
    MatButtonModule,
    MatIconModule,
    RouterModule
  ],
  exports: [
    PontoCadastroComponent, PontoConsultaComponent
  ]
})
export class PontoexternoModule { }
