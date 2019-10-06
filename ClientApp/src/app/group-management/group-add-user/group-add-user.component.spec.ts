import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupAddUserComponent } from './group-add-user.component';

describe('GroupAddUserComponent', () => {
  let component: GroupAddUserComponent;
  let fixture: ComponentFixture<GroupAddUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GroupAddUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupAddUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
