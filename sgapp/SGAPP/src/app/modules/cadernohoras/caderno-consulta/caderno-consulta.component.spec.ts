import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CadernoConsultaComponent } from './caderno-consulta.component';

describe('CadernoConsultaComponent', () => {
  let component: CadernoConsultaComponent;
  let fixture: ComponentFixture<CadernoConsultaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CadernoConsultaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CadernoConsultaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
