import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectRequestCreationComponent } from './project-request-creation.component';

describe('ProjectRequestCreationComponent', () => {
  let component: ProjectRequestCreationComponent;
  let fixture: ComponentFixture<ProjectRequestCreationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectRequestCreationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectRequestCreationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
