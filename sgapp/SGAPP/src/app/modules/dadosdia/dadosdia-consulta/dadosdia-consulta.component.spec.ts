import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DadosdiaConsultaComponent } from './dadosdia-consulta.component';

describe('DadosdiaConsultaComponent', () => {
  let component: DadosdiaConsultaComponent;
  let fixture: ComponentFixture<DadosdiaConsultaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DadosdiaConsultaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DadosdiaConsultaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
