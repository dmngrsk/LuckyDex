import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CookieReadingService {

  public hasCookie(name: string): boolean {
    return document.cookie.match(`^(?:.*;)?\\s*${name}\\s*=\\s*([^;]+)(?:.*)?$`) !== null;
  }
}
