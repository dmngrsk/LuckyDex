import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule } from '@angular/material/snack-bar';

const MATERIAL_MODULES = [
  CommonModule,
  BrowserAnimationsModule,

  MatInputModule,
  MatButtonModule,
  MatCardModule,
  MatGridListModule,
  MatSelectModule,
  MatAutocompleteModule,
  MatProgressSpinnerModule,
  MatSnackBarModule,
  MatCheckboxModule
];

@NgModule({
  imports: [ ...MATERIAL_MODULES ],
  exports: [ ...MATERIAL_MODULES ]
})
export class MaterialModule { }
