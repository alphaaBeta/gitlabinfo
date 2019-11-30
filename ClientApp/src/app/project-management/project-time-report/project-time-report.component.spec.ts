import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectTimeReportComponent } from './project-time-report.component';

describe('ProjectTimeReportComponent', () => {
  let component: ProjectTimeReportComponent;
  let fixture: ComponentFixture<ProjectTimeReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectTimeReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectTimeReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
