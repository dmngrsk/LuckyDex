import { NgModule } from '@angular/core';
import { TrainerDetailsComponent } from './trainer-details/trainer-details.component';
import { TrainerSearchComponent } from './trainer-search/trainer-search.component';
import { PokemonCardComponent } from './pokemon-card/pokemon-card.component';
import { TrainerRoutingModule } from './trainer-routing.module';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from 'src/app/shared/material.module';
import { TrainerRelationshipService } from '../../shared/services/trainer-relationship.service';

@NgModule({
  declarations: [
    TrainerDetailsComponent,
    TrainerSearchComponent,
    PokemonCardComponent
  ],
  imports: [
    TrainerRoutingModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MaterialModule
  ],
  providers: [
    TrainerRelationshipService
  ]
})
export class TrainerModule { }
