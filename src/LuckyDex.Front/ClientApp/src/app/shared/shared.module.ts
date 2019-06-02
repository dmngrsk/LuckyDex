import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from './material.module';
import { PokemonCardComponent } from './components/pokemon-card/pokemon-card.component';

const SHARED_MODULES: any[] = [
  FormsModule,
  ReactiveFormsModule,
  HttpClientModule,
  MaterialModule
];

const SHARED_DECLARATIONS: any[] = [
  PokemonCardComponent
];

@NgModule({
  imports: SHARED_MODULES,
  declarations: SHARED_DECLARATIONS,
  exports: SHARED_MODULES.concat(SHARED_DECLARATIONS)
})
export class SharedModule { }
