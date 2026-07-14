import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { AddressDto } from '../models/address-dto';
import { UspsService } from '../usps.service';

@Component({
  selector: 'app-usps-address-validate',
  standalone: false,
  templateUrl: './usps-address-validate.html',
  styleUrls: ['./usps-address-validate.css'],
})
export class UspsAddressValidateComponent {
  form: FormGroup;
  result: any;

  constructor(private fb: FormBuilder, private uspsService: UspsService) {
    this.form = this.fb.group({
      address1: ['', Validators.required],
      address2: [''],
      city: ['', Validators.required],
      state: ['', Validators.required],
      zip5: ['', Validators.required]
    });
  }

  validate() {
    const address: AddressDto = this.form.value;

    this.uspsService.validateAddress(address).subscribe(response => {
      this.result = response;
    });
  }
}

