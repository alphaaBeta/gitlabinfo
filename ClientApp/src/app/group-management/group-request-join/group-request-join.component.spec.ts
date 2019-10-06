import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupRequestJoinComponent } from './group-request-join.component';

describe('GroupRequestJoinComponent', () => {
  let component: GroupRequestJoinComponent;
  let fixture: ComponentFixture<GroupRequestJoinComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GroupRequestJoinComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupRequestJoinComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
