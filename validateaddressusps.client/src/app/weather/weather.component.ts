import { ChangeDetectorRef, Component, OnInit, NgZone } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-weather',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './weather.component.html',
  styleUrls: ['./weather.component.css']
})
export class WeatherComponent implements OnInit {
  public forecasts: WeatherForecast[] = [];

  constructor(private http: HttpClient, private cd: ChangeDetectorRef, private zone: NgZone) {}

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.http.get<WeatherForecast[]>('/weatherforecast').subscribe(
      (result) => {
        console.log('Weather load result:', result);
        // Ensure assignment happens inside Angular zone and trigger change detection
        this.zone.run(() => {
          this.forecasts = Array.isArray(result) ? result : [];
          try { this.cd.detectChanges(); } catch {}
          console.log('forecasts after assign:', this.forecasts);
        });
      },
      (error) => {
        console.error(error);
      }
    );
  }

  get compactJson(): string {
    try {
      return JSON.stringify(this.forecasts);
    } catch {
      return '';
    }
  }
}
