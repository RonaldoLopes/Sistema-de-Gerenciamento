import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultaprojetoComponent } from './consultaprojeto.component';

describe('ConsultaprojetoComponent', () => {
  let component: ConsultaprojetoComponent;
  let fixture: ComponentFixture<ConsultaprojetoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConsultaprojetoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConsultaprojetoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
