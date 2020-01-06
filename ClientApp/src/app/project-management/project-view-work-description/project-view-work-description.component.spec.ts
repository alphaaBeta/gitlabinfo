import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectViewWorkDescriptionComponent } from './project-view-work-description.component';

describe('ProjectViewWorkDescriptionComponent', () => {
  let component: ProjectViewWorkDescriptionComponent;
  let fixture: ComponentFixture<ProjectViewWorkDescriptionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectViewWorkDescriptionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectViewWorkDescriptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
