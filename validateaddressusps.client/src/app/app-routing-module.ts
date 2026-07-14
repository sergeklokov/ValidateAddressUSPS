import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UspsAddressValidateComponent } from './usps-address-validate/usps-address-validate';

const routes: Routes = [
  { path: '', redirectTo: 'validate-address', pathMatch: 'full' },
  { path: 'validate-address', component: UspsAddressValidateComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
