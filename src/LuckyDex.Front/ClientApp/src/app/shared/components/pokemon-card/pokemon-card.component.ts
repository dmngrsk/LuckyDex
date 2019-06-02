import { Component, Input } from '@angular/core';
import { PokemonCardInfo } from '../../models/pokemon-card-info';

@Component({
  selector: 'ld-pokemon-card',
  templateUrl: './pokemon-card.component.html',
  styleUrls: ['./pokemon-card.component.css']
})
export class PokemonCardComponent {

  @Input() data: PokemonCardInfo;

  toggleSelected(): void {
    this.data.selected = !this.data.selected;
  }
}
