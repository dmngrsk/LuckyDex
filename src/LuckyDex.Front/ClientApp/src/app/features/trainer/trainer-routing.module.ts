import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TrainerSearchComponent } from './trainer-search/trainer-search.component';
import { TrainerDetailsComponent } from './trainer-details/trainer-details.component';

const routes: Routes = [
  { path: 'trainer', component: TrainerSearchComponent },
  { path: 'trainer/:name', component: TrainerDetailsComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TrainerRoutingModule { }
