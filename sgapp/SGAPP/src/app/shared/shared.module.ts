import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { MatDividerModule, MatToolbarModule, MatIconModule,  MatMenuModule, MatListModule } from '@angular/material';
import { MatGridListModule, MatTableModule, MatPaginatorModule, MatInputModule } from '@angular/material';
import { MatButtonModule } from '@angular/material/button';
import { FlexLayoutModule } from '@angular/flex-layout';
import { RouterModule } from '@angular/router';
import { AreaComponent } from './widgets/area/area.component';
import { HighchartsChartModule } from 'highcharts-angular';
import { MatExpansionModule } from '@angular/material/expansion';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { DialogMessageComponent } from './dialog-message/dialog-message.component';
import { MatDialogModule } from '@angular/material/dialog';
import { ClienteconsultaComponent } from './clienteconsulta/clienteconsulta.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ConsultaprojetoComponent } from './consultaprojeto/consultaprojeto.component';
import { MatTabsModule } from '@angular/material/tabs';
import { RecursosComponent } from './widgets/card/recursos/recursos.component';
import { MobilizacoesComponent } from './widgets/card/mobilizacoes/mobilizacoes.component';
import { HorasdComponent } from './widgets/card/horasd/horasd.component';
import { HorasimpComponent } from './widgets/card/horasimp/horasimp.component';
import {MatSelectModule} from '@angular/material/select';
@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    SidebarComponent,
    AreaComponent,
    DialogMessageComponent,
    ClienteconsultaComponent,
    ConsultaprojetoComponent,
    RecursosComponent,
    MobilizacoesComponent,
    HorasdComponent,
    HorasimpComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BrowserAnimationsModule,
    MatDividerModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    FlexLayoutModule,
    MatSnackBarModule,
    MatMenuModule,
    MatListModule,
    MatDialogModule,
    RouterModule,
    HighchartsChartModule,
    FlexLayoutModule,
    MatGridListModule,
    MatTableModule,
    MatPaginatorModule,
    MatExpansionModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatPaginatorModule,
    MatTableModule,
    MatTabsModule,
    TooltipModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 5000,
      preventDuplicates: true
    })
  ],
  exports: [
    HeaderComponent,
    FooterComponent,
    SidebarComponent,
    AreaComponent,
    RecursosComponent,
    MobilizacoesComponent,
    HorasdComponent,
    HorasimpComponent
  ]
})
export class SharedModule { }
