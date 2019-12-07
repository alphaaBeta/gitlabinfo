import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectEngagementPointsComponent } from './project-engagement-points.component';

describe('ProjectEngagementPointsComponent', () => {
  let component: ProjectEngagementPointsComponent;
  let fixture: ComponentFixture<ProjectEngagementPointsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectEngagementPointsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectEngagementPointsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
