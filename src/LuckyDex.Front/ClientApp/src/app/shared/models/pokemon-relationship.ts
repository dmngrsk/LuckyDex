import { Trainer } from 'src/app/features/trainer/trainer';

export class Pokemon {
  id: string;
}

export class PokemonRelationship {
  pokemon: Pokemon;
  trainers: Trainer[];
}
