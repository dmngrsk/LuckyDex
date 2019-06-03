import { Trainer } from './trainer-relationship';

export class Pokemon {
  id: string;
}

export class PokemonRelationship {
  pokémon: Pokemon;
  trainers: Trainer[];
}
