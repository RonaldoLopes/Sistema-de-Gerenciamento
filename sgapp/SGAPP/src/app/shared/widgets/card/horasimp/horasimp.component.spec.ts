import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HorasimpComponent } from './horasimp.component';

describe('HorasimpComponent', () => {
  let component: HorasimpComponent;
  let fixture: ComponentFixture<HorasimpComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HorasimpComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HorasimpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
