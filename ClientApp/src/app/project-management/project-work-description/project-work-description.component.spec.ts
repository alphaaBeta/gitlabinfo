import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectWorkDescriptionPostComponent } from './project-work-description.component';

describe('ProjectWorkDescriptionPostComponent', () => {
  let component: ProjectWorkDescriptionPostComponent;
  let fixture: ComponentFixture<ProjectWorkDescriptionPostComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectWorkDescriptionPostComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectWorkDescriptionPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
