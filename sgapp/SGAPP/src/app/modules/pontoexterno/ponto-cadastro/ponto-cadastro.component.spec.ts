import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PontoCadastroComponent } from './ponto-cadastro.component';

describe('PontoCadastroComponent', () => {
  let component: PontoCadastroComponent;
  let fixture: ComponentFixture<PontoCadastroComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PontoCadastroComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PontoCadastroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
