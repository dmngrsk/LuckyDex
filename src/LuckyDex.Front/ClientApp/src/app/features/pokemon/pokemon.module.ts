import { NgModule } from '@angular/core';
import { PokemonDetailsComponent } from './pokemon-details/pokemon-details.component';
import { PokemonSearchComponent } from './pokemon-search/pokemon-search.component';
import { PokemonRoutingModule } from './pokemon-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { PokemonRelationshipService } from 'src/app/shared/services/pokemon-relationship.service';

@NgModule({
  declarations: [
    PokemonDetailsComponent,
    PokemonSearchComponent,
  ],
  imports: [
    PokemonRoutingModule,
    SharedModule
  ],
  providers: [
    PokemonRelationshipService
  ]
})
export class PokemonModule { }
