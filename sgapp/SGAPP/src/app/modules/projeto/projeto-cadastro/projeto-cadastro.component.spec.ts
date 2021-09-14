import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjetoCadastroComponent } from './projeto-cadastro.component';

describe('ProjetoCadastroComponent', () => {
  let component: ProjetoCadastroComponent;
  let fixture: ComponentFixture<ProjetoCadastroComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjetoCadastroComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjetoCadastroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
