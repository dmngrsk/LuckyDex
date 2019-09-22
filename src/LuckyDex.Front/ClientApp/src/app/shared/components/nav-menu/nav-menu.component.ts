import { Component, Inject } from '@angular/core';
import { CookieReadingService } from '../../services/cookie-reading.service';
import { MapRoutingService } from '../../services/map-routing.service';
import { DOCUMENT } from '@angular/common';
import { APPCONFIG } from 'src/app/config';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'ld-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  mapCookieName = APPCONFIG.mapCookieName;

  constructor(
    @Inject(DOCUMENT) private document: Document,
    public cookieReadingService: CookieReadingService,
    private snackBar: MatSnackBar,
    private mapRoutingService: MapRoutingService
  ) { }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  redirectToMap() {
    this.snackBar.open('Redirecting, please wait...', 'Close', { duration: 60000 });

    this.mapRoutingService
      .getMapRouting()
      .subscribe(route => this.document.location.href = route.route);
  }
}
