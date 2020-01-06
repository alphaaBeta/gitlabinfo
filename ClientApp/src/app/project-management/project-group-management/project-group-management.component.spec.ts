import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectGroupManagementComponent } from './project-group-management.component';

describe('ProjectGroupManagementComponent', () => {
  let component: ProjectGroupManagementComponent;
  let fixture: ComponentFixture<ProjectGroupManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectGroupManagementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectGroupManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
