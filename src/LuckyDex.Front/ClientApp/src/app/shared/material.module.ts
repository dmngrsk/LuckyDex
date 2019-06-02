import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  MatInputModule,
  MatButtonModule,
  MatCardModule,
  MatGridListModule,
  MatProgressSpinnerModule,
  MatSnackBarModule
} from '@angular/material';

const MATERIAL_MODULES = [
  CommonModule,
  BrowserAnimationsModule,

  MatInputModule,
  MatButtonModule,
  MatCardModule,
  MatGridListModule,
  MatProgressSpinnerModule,
  MatSnackBarModule
];

@NgModule({
  imports: [ ...MATERIAL_MODULES ],
  exports: [ ...MATERIAL_MODULES ]
})
export class MaterialModule { }
