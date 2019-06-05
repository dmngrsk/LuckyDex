import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TrainerRelationship, Trainer } from '../models/trainer-relationship';
import { APPCONFIG } from 'src/app/config';

@Injectable({
  providedIn: 'root'
})
export class TrainerRelationshipService {

  baseUrl = APPCONFIG.apiAddress;

  constructor(private http: HttpClient) { }

  public getTrainers(): Observable<Trainer[]> {
    const url = `${this.baseUrl}/trainers`;
    return this.http.get<Trainer[]>(url);
  }

  public getTrainerRelationship(name: string): Observable<TrainerRelationship> {
    const url = `${this.baseUrl}/trainers/${name}`;
    return this.http.get<TrainerRelationship>(url);
  }

  public putTrainerRelationship(relationship: TrainerRelationship): Observable<TrainerRelationship> {
    const url = `${this.baseUrl}/trainers/${relationship.trainer.name}`;
    return this.http.put<TrainerRelationship>(url, relationship);
  }
}
