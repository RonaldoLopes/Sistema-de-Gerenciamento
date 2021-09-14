import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PontoConsultaComponent } from './ponto-consulta.component';

describe('PontoConsultaComponent', () => {
  let component: PontoConsultaComponent;
  let fixture: ComponentFixture<PontoConsultaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PontoConsultaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PontoConsultaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
