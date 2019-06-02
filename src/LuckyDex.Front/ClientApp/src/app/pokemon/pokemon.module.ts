import { NgModule } from '@angular/core';
import { PokemonDetailsComponent } from './pokemon-details/pokemon-details.component';
import { PokemonSearchComponent } from './pokemon-search/pokemon-search.component';
import { PokemonRoutingModule } from './pokemon-routing.module';

@NgModule({
  declarations: [
    PokemonDetailsComponent,
    PokemonSearchComponent,
  ],
  imports: [
    PokemonRoutingModule
  ]
})
export class PokemonModule { }
