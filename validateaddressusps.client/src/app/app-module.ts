import { HttpClientModule } from '@angular/common/http';
import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { UspsAddressValidateComponent } from './usps-address-validate/usps-address-validate';
import { WeatherComponent } from './weather/weather.component';

@NgModule({
  declarations: [App, UspsAddressValidateComponent],
  imports: [BrowserModule, HttpClientModule, AppRoutingModule, ReactiveFormsModule, WeatherComponent],
  providers: [provideBrowserGlobalErrorListeners()],
  bootstrap: [App],
})
export class AppModule {}
