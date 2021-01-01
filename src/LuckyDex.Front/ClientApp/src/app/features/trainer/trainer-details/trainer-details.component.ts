import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainerRelationshipService } from 'src/app/shared/services/trainer-relationship.service';
import { filter } from 'rxjs/operators';
import { PokemonCardInfoService } from 'src/app/shared/services/pokemon-card-info.service';
import { PokemonCardInfo } from 'src/app/shared/models/pokemon-card-info';
import { TrainerRelationship, Trainer } from 'src/app/shared/models/trainer-relationship';
import { Pokemon } from 'src/app/shared/models/pokemon-relationship';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'ld-trainer-details',
  templateUrl: './trainer-details.component.html',
  styleUrls: ['./trainer-details.component.css'],
  providers: [TrainerRelationshipService, PokemonCardInfoService]
})
export class TrainerDetailsComponent implements OnInit {

  loaded: boolean;
  cards: PokemonCardInfo[];

  trainerName: string;
  lastModified: Date;
  trainerComment: string;
  commentDisplayed = false;
  displaySelected = true;
  displayUnselected = true;
  hideUnobtainable = false;
  hideRegional = false;
  displayedCards: PokemonCardInfo[];

  constructor(
    private activatedRoute: ActivatedRoute,
    private snackBar: MatSnackBar,
    private title: Title,
    private trainerService: TrainerRelationshipService,
    private cardInfoService: PokemonCardInfoService
  ) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      this.loaded = false;

      this.trainerService.getTrainerRelationship(params.name).pipe(
        filter(f => !!f),
      ).subscribe(
        r => {
          this.title.setTitle(`${r.trainer.name}'s details - LuckyDex`);

          const ids = r.pokémon.map(p => p.id);
          const cards = this.cardInfoService.getAllPokemonCardsInfo(ids);

          this.loaded = true;
          this.cards = cards;

          this.trainerName = r.trainer.name;
          this.lastModified = r.trainer.lastModified;
          this.trainerComment = r.trainer.comment ? r.trainer.comment : '';
          this.displayedCards = cards;

        },
        error => console.error(error)
      );
    });
  }

  refreshCards() {
    const selectedCards = this.displaySelected ? this.cards.filter(c => c.selected) : [];
    const unselectedCards = this.displayUnselected ? this.cards.filter(c => !c.selected) : [];

    this.displayedCards = selectedCards
      .concat(unselectedCards)
      .filter(c => !this.hideUnobtainable || c.obtainable)
      .filter(c => !this.hideRegional || !c.regional)
      .sort((a, b) => +a.id - +b.id);
  }

  displayComment() {
    this.commentDisplayed = true;
  }

  onClick() {
    const relationship = <TrainerRelationship> {
      trainer: <Trainer> { name: this.trainerName, comment: this.trainerComment },
      pokémon: this.cards.filter(c => c.selected).map(c => <Pokemon> { id: c.id })
    };

    this.trainerService.putTrainerRelationship(relationship).subscribe(
      _ => this.snackBar.open('LuckyDex saved successfully', 'Close', { duration: 5000 }),
      error => console.error(error)
    );
  }
}
