import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupProjectRequestListComponent } from './group-project-request-list.component';

describe('GroupProjectRequestListComponent', () => {
  let component: GroupProjectRequestListComponent;
  let fixture: ComponentFixture<GroupProjectRequestListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GroupProjectRequestListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupProjectRequestListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
