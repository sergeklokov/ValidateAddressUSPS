import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UspsAddressValidate } from './usps-address-validate';

describe('UspsAddressValidate', () => {
  let component: UspsAddressValidate;
  let fixture: ComponentFixture<UspsAddressValidate>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UspsAddressValidate],
    }).compileComponents();

    fixture = TestBed.createComponent(UspsAddressValidate);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
