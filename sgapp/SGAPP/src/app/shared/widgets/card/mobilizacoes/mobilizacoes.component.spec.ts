import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MobilizacoesComponent } from './mobilizacoes.component';

describe('MobilizacoesComponent', () => {
  let component: MobilizacoesComponent;
  let fixture: ComponentFixture<MobilizacoesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MobilizacoesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MobilizacoesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
