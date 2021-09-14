import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ToastrModule } from 'ngx-toastr';
import { TooltipModule } from 'ngx-bootstrap/tooltip';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DefaultModule } from './layouts/default/default.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule, MatCardModule, MatFormFieldModule, MatInputModule, MatDatepickerModule } from '@angular/material';
import { MatDividerModule, MatTableModule, MatPaginatorModule } from '@angular/material';
import { UserComponent } from './user/user.component';
import { LoginComponent } from './user/login/login.component';
import { AuthInterceptor } from './auth/AuthInterceptor';
import { RegisterComponent } from './modules/register/register.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import {MatSelectModule} from '@angular/material/select';
import { RelatoriosComponent } from './modules/relatorios/relatorios.component';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {DatePipe} from '@angular/common';
@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    LoginComponent,
    RegisterComponent,
    RelatoriosComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    DefaultModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatInputModule,
    MatSnackBarModule,
    MatFormFieldModule,
    MatDialogModule,
    MatCardModule,
    MatButtonModule,
    FlexLayoutModule,
    MatSelectModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatProgressSpinnerModule,
    MatTableModule,
    MatPaginatorModule,
    MatDividerModule,
    MatIconModule,
    TooltipModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 5000,
      preventDuplicates: true
    }),
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    MatDatepickerModule,
    DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
