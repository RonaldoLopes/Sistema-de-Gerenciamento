import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatGridListModule } from '@angular/material/grid-list';
import {  MatPaginatorModule, MatTableModule, MatCardModule } from '@angular/material';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule, MatToolbarModule, MatIconModule,  MatMenuModule, MatListModule } from '@angular/material';

import { ContatoCadastroComponent } from './contato-cadastro/contato-cadastro.component';
import { ContatoConsultaComponent } from './contato-consulta/contato-consulta.component';

import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [ContatoCadastroComponent, ContatoConsultaComponent],
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
    MatIconModule,
    MatButtonModule,
    RouterModule,
    MatCardModule
  ],
  exports: [ContatoCadastroComponent, ContatoConsultaComponent]
})
export class ContatoModule { }
