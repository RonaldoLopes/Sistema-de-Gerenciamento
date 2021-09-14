import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HorasdComponent } from './horasd.component';

describe('HorasdComponent', () => {
  let component: HorasdComponent;
  let fixture: ComponentFixture<HorasdComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HorasdComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HorasdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
