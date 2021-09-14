import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DefaultComponent } from './default.component';
import { DashboardComponent } from '../dashboard/dashboard.component';
import { RouterModule } from '@angular/router';

import { ClientesComponent } from 'src/app/modules/clientes/clientes.component';

import { SharedModule } from 'src/app/shared/shared.module';
import { MatSidenavModule, MatDividerModule, MatCardModule, MatPaginatorModule, MatTableModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatFormFieldModule, MatInputModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';

import { ContatoModule } from 'src/app/modules/contato/contato.module';
import { ProjetoModule } from 'src/app/modules/projeto/projeto.module';
import { CadernohorasModule } from 'src/app/modules/cadernohoras/cadernohoras.module';
import { DadosdiaModule } from 'src/app/modules/dadosdia/dadosdia.module';
import { PontoexternoModule } from 'src/app/modules/pontoexterno/pontoexterno.module';
import { ChartsService } from 'src/app/_services/charts.service';


@NgModule({
  declarations: [
    DefaultComponent,
    DashboardComponent,
    ClientesComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    MatSidenavModule,
    MatDividerModule,
    MatCardModule,
    FlexLayoutModule,
    MatGridListModule,
    MatInputModule,
    MatFormFieldModule,
    MatPaginatorModule,
    MatTableModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatIconModule,

    ContatoModule,
    ProjetoModule,
    CadernohorasModule,
    DadosdiaModule,
    PontoexternoModule
  ],
  providers: [
    ChartsService
  ]
})
export class DefaultModule { }
