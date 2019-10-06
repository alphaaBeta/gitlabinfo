import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupAddCurrentUserComponent } from './group-add-current-user.component';

describe('GroupAddCurrentUserComponent', () => {
  let component: GroupAddCurrentUserComponent;
  let fixture: ComponentFixture<GroupAddCurrentUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GroupAddCurrentUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupAddCurrentUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
