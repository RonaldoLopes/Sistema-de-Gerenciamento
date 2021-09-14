import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContatoCadastroComponent } from './contato-cadastro.component';

describe('ContatoCadastroComponent', () => {
  let component: ContatoCadastroComponent;
  let fixture: ComponentFixture<ContatoCadastroComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContatoCadastroComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContatoCadastroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
