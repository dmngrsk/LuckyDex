import { NgModule } from '@angular/core';
import { TrainerDetailsComponent } from './trainer-details/trainer-details.component';
import { TrainerSearchComponent } from './trainer-search/trainer-search.component';
import { TrainerRoutingModule } from './trainer-routing.module';
import { SharedModule } from '../shared.module';

@NgModule({
  declarations: [
    TrainerDetailsComponent,
    TrainerSearchComponent
  ],
  imports: [
    TrainerRoutingModule,
    SharedModule
  ]
})
export class TrainerModule { }
