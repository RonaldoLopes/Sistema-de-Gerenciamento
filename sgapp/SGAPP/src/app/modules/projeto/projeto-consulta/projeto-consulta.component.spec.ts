import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjetoConsultaComponent } from './projeto-consulta.component';

describe('ProjetoConsultaComponent', () => {
  let component: ProjetoConsultaComponent;
  let fixture: ComponentFixture<ProjetoConsultaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjetoConsultaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjetoConsultaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
