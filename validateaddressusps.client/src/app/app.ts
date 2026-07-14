import { HttpClient } from '@angular/common/http';
import { Component, OnInit, signal, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrls: ['./app.css']
})
export class App implements OnInit {
  public forecasts: WeatherForecast[] = [];

  constructor(private http: HttpClient, private cd: ChangeDetectorRef) {}

  ngOnInit() {
    this.getForecasts();
    // Log router config to help diagnose routing issues
    try {
      // Router may not be injected in this component for all projects; inject lazily
    } catch {}
  }

  getForecasts() {
    this.http.get<WeatherForecast[]>('/weatherforecast').subscribe(
      (result) => {
        // Defensive handling: server may return an array or an object wrapper.
        // Log the raw result to help debugging in the browser console.
        console.log('getForecasts result:', result);

        if (Array.isArray(result)) {
          this.forecasts = result;
        } else if ((result as any)?.value && Array.isArray((result as any).value)) {
          this.forecasts = (result as any).value;
        } else if ((result as any)?.data && Array.isArray((result as any).data)) {
          this.forecasts = (result as any).data;
        } else {
          // Fallback: try to coerce to array if possible, otherwise empty
          this.forecasts = (result as any) || [];
        }
        // Ensure Angular runs change detection so the template updates
        try {
          this.cd.detectChanges();
        } catch (e) {
          // ignore - detectChanges can throw if called at certain times
        }
        console.log('forecasts after assign:', this.forecasts);
      },
      (error) => {
        console.error(error);
      }
    );
  }

  protected readonly title = signal('validateaddressusps.client');
}
