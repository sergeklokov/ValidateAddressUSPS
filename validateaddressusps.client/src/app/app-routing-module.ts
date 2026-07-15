import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UspsAddressValidateComponent } from './usps-address-validate/usps-address-validate';
import { WeatherComponent } from './weather/weather.component';

const routes: Routes = [
  { path: '', component: WeatherComponent },
  { path: 'validate-address', component: UspsAddressValidateComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
