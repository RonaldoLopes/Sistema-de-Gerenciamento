import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DadosdiaCadastroComponent } from './dadosdia-cadastro.component';

describe('DadosdiaCadastroComponent', () => {
  let component: DadosdiaCadastroComponent;
  let fixture: ComponentFixture<DadosdiaCadastroComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DadosdiaCadastroComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DadosdiaCadastroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
