import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SportsDashboardComponent } from './sports-dashboard.component';

describe('SportsDashboardComponent', () => {
  let component: SportsDashboardComponent;
  let fixture: ComponentFixture<SportsDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SportsDashboardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SportsDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
