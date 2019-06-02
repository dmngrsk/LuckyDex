import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TrainerRelationship } from '../models/trainer-relationship';

@Injectable({
  providedIn: 'root'
})
export class TrainerRelationshipService {

  baseUrl = 'http://localhost:50765';

  constructor(
    private http: HttpClient
  ) { }

  public getTrainerRelationship(name: string): Observable<TrainerRelationship> {
    const url = `${this.baseUrl}/trainers/${name}`;
    return this.http.get<TrainerRelationship>(url);
  }

  public putTrainerRelationship(relationship: TrainerRelationship): Observable<TrainerRelationship> {
    const url = `${this.baseUrl}/trainers/${relationship.trainer.name}`;
    return this.http.put<TrainerRelationship>(url, relationship);
  }
}
