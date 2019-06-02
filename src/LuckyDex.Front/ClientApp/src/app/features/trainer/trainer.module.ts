import { NgModule } from '@angular/core';
import { TrainerDetailsComponent } from './trainer-details/trainer-details.component';
import { TrainerSearchComponent } from './trainer-search/trainer-search.component';
import { TrainerRoutingModule } from './trainer-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { TrainerRelationshipService } from '../../shared/services/trainer-relationship.service';

@NgModule({
  declarations: [
    TrainerDetailsComponent,
    TrainerSearchComponent
  ],
  imports: [
    TrainerRoutingModule,
    SharedModule
  ],
  providers: [
    TrainerRelationshipService
  ]
})
export class TrainerModule { }
