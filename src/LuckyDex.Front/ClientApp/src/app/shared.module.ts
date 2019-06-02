import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './material.module';

const SHARED_MODULES = [
  FormsModule,
  ReactiveFormsModule,
  MaterialModule
];

@NgModule({
  imports: [ ...SHARED_MODULES ],
  exports: [ ...SHARED_MODULES ]
})
export class SharedModule { }
