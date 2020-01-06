import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupClaimOwnershipComponent } from './group-add-current-user.component';

describe('GroupAddCurrentUserComponent', () => {
  let component: GroupClaimOwnershipComponent;
  let fixture: ComponentFixture<GroupClaimOwnershipComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GroupClaimOwnershipComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupClaimOwnershipComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
