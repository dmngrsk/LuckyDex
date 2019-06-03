import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { PokemonCardInfoService } from 'src/app/shared/services/pokemon-card-info.service';

@Component({
  selector: 'ld-pokemon-search',
  templateUrl: './pokemon-search.component.html',
  styleUrls: ['./pokemon-search.component.css'],
  providers: [PokemonCardInfoService]
})
export class PokemonSearchComponent implements OnInit {

  pokemon: any[];
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private cardInfoService: PokemonCardInfoService
  ) { }

  ngOnInit() {
    this.pokemon = this.cardInfoService
      .getAllPokemonCardsInfo([])
      .map(c => ({ value: c.id, display: `(#${c.id}) ${c.name}` }));

    this.form = this.fb.group({
      pokemonId: ['', Validators.required]
    });
  }

  onSubmit() {
    const pokemonId = this.form.get('pokemonId').value;
    this.router.navigate([`pokemon/${pokemonId}`]);
  }
}
