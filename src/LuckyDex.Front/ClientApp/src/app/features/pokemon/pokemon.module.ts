import { NgModule } from '@angular/core';
import { PokemonDetailsComponent } from './pokemon-details/pokemon-details.component';
import { PokemonSearchComponent } from './pokemon-search/pokemon-search.component';
import { PokemonRoutingModule } from './pokemon-routing.module';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from 'src/app/shared/material.module';
import { PokemonRelationshipService } from 'src/app/shared/services/pokemon-relationship.service';

@NgModule({
  declarations: [
    PokemonDetailsComponent,
    PokemonSearchComponent
  ],
  imports: [
    PokemonRoutingModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MaterialModule
  ],
  providers: [
    PokemonRelationshipService
  ]
})
export class PokemonModule { }
