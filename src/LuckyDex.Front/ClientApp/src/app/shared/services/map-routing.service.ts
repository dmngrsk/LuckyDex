import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { APPCONFIG } from 'src/app/config';
import { Routing } from '../models/routing';

@Injectable({
  providedIn: 'root'
})
export class MapRoutingService {

  baseUrl = APPCONFIG.apiAddress;

  constructor(private http: HttpClient) { }

  public getMapRouting(): Observable<Routing> {
    const url = `${this.baseUrl}/routing/map`;
    return this.http.get<Routing>(url);
  }
}
