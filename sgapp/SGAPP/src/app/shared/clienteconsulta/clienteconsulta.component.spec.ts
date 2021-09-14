import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClienteconsultaComponent } from './clienteconsulta.component';

describe('ClienteconsultaComponent', () => {
  let component: ClienteconsultaComponent;
  let fixture: ComponentFixture<ClienteconsultaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClienteconsultaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClienteconsultaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
