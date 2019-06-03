import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { APPCONFIG } from 'src/app/config';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {
  constructor(private http: HttpClient) { }

  getApplicationSettings(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http
        .get<ApplicationSettings>('/api/settings')
        .subscribe(response => {
          APPCONFIG.apiAddress = response.apiAddress;

          resolve(true);
        },
        error => reject(false)
      );
    });
  }
}

export class ApplicationSettings {
  apiAddress: string;
}
