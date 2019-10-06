import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupOwnedListComponent } from './group-owned-list.component';

describe('GroupOwnedListComponent', () => {
  let component: GroupOwnedListComponent;
  let fixture: ComponentFixture<GroupOwnedListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GroupOwnedListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupOwnedListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
