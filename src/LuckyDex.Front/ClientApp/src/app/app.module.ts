import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { HomeComponent } from './shared/components/home/home.component';
import { NavMenuComponent } from './shared/components/nav-menu/nav-menu.component';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { SharedModule } from './shared/shared.module';
import { PokemonModule } from './features/pokemon/pokemon.module';
import { TrainerModule } from './features/trainer/trainer.module';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavMenuComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' }
    ]),

    SharedModule,
    PokemonModule,
    TrainerModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
