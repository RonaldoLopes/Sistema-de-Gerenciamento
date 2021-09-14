import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatGridListModule } from '@angular/material/grid-list';
import {  MatDividerModule,  MatPaginatorModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatTabsModule, MatCheckboxModule } from '@angular/material';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatRadioModule } from '@angular/material/radio';

import { DadosdiaCadastroComponent } from './dadosdia-cadastro/dadosdia-cadastro.component';
import { DadosdiaConsultaComponent } from './dadosdia-consulta/dadosdia-consulta.component';
import { RouterModule } from '@angular/router';


@NgModule({
  declarations: [DadosdiaCadastroComponent, DadosdiaConsultaComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatDividerModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatSelectModule,
    FlexLayoutModule,
    MatGridListModule,
    MatTableModule,
    MatPaginatorModule,
    MatDatepickerModule,
    MatRadioModule,
    MatButtonModule,
    MatIconModule,
    RouterModule,
    MatTabsModule,
    MatCheckboxModule
  ],
  exports: [DadosdiaCadastroComponent, DadosdiaConsultaComponent]
})
export class DadosdiaModule { }
