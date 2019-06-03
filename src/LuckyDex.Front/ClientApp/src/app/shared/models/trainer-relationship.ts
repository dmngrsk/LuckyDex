import { Pokemon } from './pokemon-relationship';

export class Trainer {
  name: string;
  comment: string;
}

export class TrainerRelationship {
  trainer: Trainer;
  pokémon: Pokemon[];
}
