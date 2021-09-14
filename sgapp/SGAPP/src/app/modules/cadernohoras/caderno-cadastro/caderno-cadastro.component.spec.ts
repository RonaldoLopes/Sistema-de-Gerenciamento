import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CadernoCadastroComponent } from './caderno-cadastro.component';

describe('CadernoCadastroComponent', () => {
  let component: CadernoCadastroComponent;
  let fixture: ComponentFixture<CadernoCadastroComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CadernoCadastroComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CadernoCadastroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
