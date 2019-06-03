import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PokemonRelationshipService } from 'src/app/shared/services/pokemon-relationship.service';
import { filter } from 'rxjs/operators';
import { PokemonCardInfoService } from 'src/app/shared/services/pokemon-card-info.service';

@Component({
  selector: 'ld-pokemon-details',
  templateUrl: './pokemon-details.component.html',
  styleUrls: ['./pokemon-details.component.css'],
  providers: [PokemonRelationshipService, PokemonCardInfoService]
})
export class PokemonDetailsComponent implements OnInit {

  loaded: boolean;
  pokemonName: string;
  trainerNames: string[];

  constructor(
    private activatedRoute: ActivatedRoute,
    private pokemonService: PokemonRelationshipService,
    private cardInfoService: PokemonCardInfoService
  ) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      this.loaded = false;

      this.pokemonService.getPokemonRelationship(params.id).pipe(
        filter(f => !!f),
      ).subscribe(
        r => {
          this.trainerNames = r.trainers.map(t => t.name);
          this.pokemonName = this.cardInfoService.getPokemonCardInfo(params.id).name;

          this.loaded = true;
        },
        error => console.error(error)
      );
    });
  }

}
