import { Component, Inject, OnInit } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { CookieReadingService } from '../../services/cookie-reading.service';
import { MatSnackBar } from '@angular/material';
import { MapRoutingService } from '../../services/map-routing.service';
import { APPCONFIG } from 'src/app/config';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'ld-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  mapCookieName = APPCONFIG.mapCookieName;

  constructor(
    @Inject(DOCUMENT) private document: Document,
    public cookieReadingService: CookieReadingService,
    private snackBar: MatSnackBar,
    private title: Title,
    private mapRoutingService: MapRoutingService,
  ) { }

  ngOnInit() {
    this.title.setTitle('LuckyDex');
  }

  redirectToMap() {
    this.snackBar.open('Redirecting, please wait...', 'Close', { duration: 60000 });

    this.mapRoutingService
      .getMapRouting()
      .subscribe(route => this.document.location.href = route.route);
  }
}
