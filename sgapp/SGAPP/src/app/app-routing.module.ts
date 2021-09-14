import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DefaultComponent } from './layouts/default/default.component';
import { DashboardComponent } from './layouts/dashboard/dashboard.component';
import { ClientesComponent } from './modules/clientes/clientes.component';
import { ContatoCadastroComponent } from './modules/contato/contato-cadastro/contato-cadastro.component';
import { ContatoConsultaComponent } from './modules/contato/contato-consulta/contato-consulta.component';
import { ProjetoCadastroComponent } from './modules/projeto/projeto-cadastro/projeto-cadastro.component';
import { ProjetoConsultaComponent } from './modules/projeto/projeto-consulta/projeto-consulta.component';
import { CadernoCadastroComponent } from './modules/cadernohoras/caderno-cadastro/caderno-cadastro.component';
import { CadernoConsultaComponent } from './modules/cadernohoras/caderno-consulta/caderno-consulta.component';
import { DadosdiaCadastroComponent } from './modules/dadosdia/dadosdia-cadastro/dadosdia-cadastro.component';
import { DadosdiaConsultaComponent } from './modules/dadosdia/dadosdia-consulta/dadosdia-consulta.component';
import { PontoCadastroComponent } from './modules/pontoexterno/ponto-cadastro/ponto-cadastro.component';
import { PontoConsultaComponent } from './modules/pontoexterno/ponto-consulta/ponto-consulta.component';
import { UserComponent } from './user/user.component';
import { LoginComponent } from './user/login/login.component';
import { AuthGuard } from './auth/auth.guard';
import { RegisterComponent } from './modules/register/register.component';
import { RelatoriosComponent } from './modules/relatorios/relatorios.component';


const routes: Routes = [
  { path: 'user', component: UserComponent,
    children: [
      { path: 'login', component: LoginComponent}
    ]
  },
  {path: '',
  component: DefaultComponent,
  children: [{
    path: '', component: DashboardComponent
  }, {
    path: 'clientes', component: ClientesComponent, canActivate: [AuthGuard]
  }, {
    path: 'contatosc', component: ContatoCadastroComponent, canActivate: [AuthGuard]
  }, {
    path: 'contatosv', component: ContatoConsultaComponent, canActivate: [AuthGuard]
  }, {
    path: 'contatosc/:id/edit', component: ContatoCadastroComponent, canActivate: [AuthGuard]
  }, {
    path: 'projetosc', component: ProjetoCadastroComponent, canActivate: [AuthGuard]
  }, {
    path: 'projetosc/:id/edit', component: ProjetoCadastroComponent, canActivate: [AuthGuard]
  }, {
    path: 'projetosv', component: ProjetoConsultaComponent, canActivate: [AuthGuard]
  }, {
    path: 'cadernosc', component: CadernoCadastroComponent, canActivate: [AuthGuard]
  }, {
    path: 'cadernosc/:id/edit', component: CadernoCadastroComponent, canActivate: [AuthGuard]
  }, {
    path: 'cadernosv', component: CadernoConsultaComponent, canActivate: [AuthGuard]
  }, {
    path: 'dadosdiac', component: DadosdiaCadastroComponent, canActivate: [AuthGuard]
  }, {
    path: 'dadosdiac/:id/edit', component: DadosdiaCadastroComponent, canActivate: [AuthGuard]
  }, {
    path: 'dadosdiav', component: DadosdiaConsultaComponent, canActivate: [AuthGuard]
  }, {
    path: 'pontoc', component: PontoCadastroComponent, canActivate: [AuthGuard]
  },{
    path: 'pontoc/:id/edit', component: PontoCadastroComponent, canActivate: [AuthGuard]
  }, {
    path: 'pontov', component: PontoConsultaComponent, canActivate: [AuthGuard]
  }, {
    path: 'register', component: RegisterComponent, canActivate: [AuthGuard]
  }, {
    path: 'relatorios', component: RelatoriosComponent, canActivate: [AuthGuard]
  }],
 }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
