import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AddressDto } from './models/address-dto';

@Injectable({
  providedIn: 'root'
})
export class UspsService {

  constructor(private http: HttpClient) { }

    validateAddress(address: AddressDto) {
    return this.http.post('/api/usps/validate', address, { responseType: 'text' });
  }

}
