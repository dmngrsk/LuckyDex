import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { PokemonCardInfoService } from 'src/app/shared/services/pokemon-card-info.service';
import { PokemonCardInfo } from 'src/app/shared/models/pokemon-card-info';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'ld-pokemon-search',
  templateUrl: './pokemon-search.component.html',
  styleUrls: ['./pokemon-search.component.css'],
  providers: [PokemonCardInfoService]
})
export class PokemonSearchComponent implements OnInit {

  form: FormGroup;

  pokemon: PokemonCardInfo[];
  filteredPokemon$: Observable<PokemonCardInfo[]>;

  constructor(
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private router: Router,
    private title: Title,
    private cardInfoService: PokemonCardInfoService
  ) { }

  ngOnInit() {
    this.title.setTitle('Pokémon list - LuckyDex');

    this.form = this.fb.group({
      pokemonName: ['', Validators.required]
    });

    this.pokemon = this.cardInfoService.getAllPokemonCardsInfo([]);
    this.filteredPokemon$ = this.form.get('pokemonName').valueChanges.pipe(
      startWith(''),
      map(value => typeof value === 'string' ? value : value.name),
      map(name => name ? this._filter(name) : this.pokemon.slice())
    );
  }

  onSubmit() {
    const value = this.form.get('pokemonName').value;

    if (typeof value === 'object') {
      this.router.navigate([`pokemon/${value.id}`]);
    } else {
      const loweredValue = value.toLowerCase();
      const pokemon = this.pokemon.find(p => p.name.toLowerCase() === loweredValue);

      if (pokemon) {
        this.router.navigate([`pokemon/${pokemon.id}`]);
      } else {
        this.snackBar.open('This Pokémon does not exist.', 'Close', { duration: 5000 });
      }
    }
  }

  displayFn(info: PokemonCardInfo) {
    return info ? info.name : undefined;
  }

  private _filter(name: string) {
    const filterValue = name.toLowerCase();

    return this.pokemon.filter(p => p.name.toLowerCase().indexOf(filterValue) === 0);
  }
}
