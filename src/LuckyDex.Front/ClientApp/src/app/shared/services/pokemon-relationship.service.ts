import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PokemonRelationship } from '../models/pokemon-relationship';
import { APPCONFIG } from 'src/app/config';

@Injectable({
  providedIn: 'root'
})
export class PokemonRelationshipService {

  baseUrl = APPCONFIG.apiAddress;

  constructor(private http: HttpClient) { }

  public getPokemonRelationship(name: string): Observable<PokemonRelationship> {
    const url = `${this.baseUrl}/pokemon/${name}`;
    return this.http.get<PokemonRelationship>(url);
  }
}
