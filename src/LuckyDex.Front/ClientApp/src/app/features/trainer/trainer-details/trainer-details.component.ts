import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainerRelationshipService } from 'src/app/shared/services/trainer-relationship.service';
import { Observable } from 'rxjs';
import { filter, map, tap } from 'rxjs/operators';
import { PokemonCardInfoService } from 'src/app/shared/services/pokemon-card-info.service';
import { PokemonCardInfo } from 'src/app/shared/models/pokemon-card-info';
import { TrainerRelationship, Trainer } from 'src/app/shared/models/trainer-relationship';
import { Pokemon } from 'src/app/shared/models/pokemon-relationship';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'ld-trainer-details',
  templateUrl: './trainer-details.component.html',
  styleUrls: ['./trainer-details.component.css'],
  providers: [TrainerRelationshipService, PokemonCardInfoService]
})
export class TrainerDetailsComponent implements OnInit {

  trainerName: string;
  cards: PokemonCardInfo[];

  constructor(
    private activatedRoute: ActivatedRoute,
    private snackBar: MatSnackBar,
    private relationshipService: TrainerRelationshipService,
    private cardInfoService: PokemonCardInfoService
  ) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      this.trainerName = params.name;

      this.relationshipService.getTrainerRelationship(this.trainerName).pipe(
        filter(f => !!f),
        map(r => r.pokémon.map(p => p.id)),
        map(ids => this.cardInfoService.getAllPokemonCardsInfo(ids))
      ).subscribe(
        cs => this.cards = cs,
        error => console.error(error)
      );
    });
  }

  onClick() {
    const relationship = <TrainerRelationship> {
      trainer: <Trainer> { name: this.trainerName },
      pokémon: this.cards.filter(c => c.selected).map(c => <Pokemon> { id: c.id })
    };

    console.log(relationship);

    this.relationshipService.putTrainerRelationship(relationship).subscribe(
      _ => this.snackBar.open('LuckyDex saved successfully', 'Close', { duration: 5000 }),
      error => console.error(error)
    );
  }
}
