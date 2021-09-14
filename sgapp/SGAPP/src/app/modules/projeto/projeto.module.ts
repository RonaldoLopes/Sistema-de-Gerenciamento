import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatGridListModule } from '@angular/material/grid-list';
import {  MatDividerModule,  MatPaginatorModule } from '@angular/material';
import { MatTableModule, MatNativeDateModule, MatIconModule, MatButtonModule } from '@angular/material';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatCheckboxModule } from '@angular/material/checkbox';

import { ProjetoCadastroComponent } from './projeto-cadastro/projeto-cadastro.component';
import { ProjetoConsultaComponent } from './projeto-consulta/projeto-consulta.component';

import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [ProjetoCadastroComponent, ProjetoConsultaComponent],
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
    MatNativeDateModule,
    MatIconModule,
    MatButtonModule,
    RouterModule,
    MatCheckboxModule
  ],
  providers: [
    MatDatepickerModule
  ],
  exports: [ProjetoCadastroComponent, ProjetoConsultaComponent]
})
export class ProjetoModule { }
