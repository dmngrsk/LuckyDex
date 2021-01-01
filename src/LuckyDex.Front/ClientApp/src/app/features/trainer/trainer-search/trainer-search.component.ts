import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TrainerRelationshipService } from 'src/app/shared/services/trainer-relationship.service';
import { filter } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'ld-trainer-search',
  templateUrl: './trainer-search.component.html',
  styleUrls: ['./trainer-search.component.css'],
  providers: [TrainerRelationshipService]
})
export class TrainerSearchComponent implements OnInit {

  trainerNames: string[];

  loaded: boolean;
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private router: Router,
    private title: Title,
    private trainerService: TrainerRelationshipService,
  ) { }

  ngOnInit() {
    this.title.setTitle('Trainer list - LuckyDex');

    this.trainerService.getTrainers().pipe(
      filter(f => !!f),
    ).subscribe(
      ts => {
        this.trainerNames = ts.map(t => t.name);
        this.loaded = true;
      },
      error => console.error(error)
    );

    this.form = this.fb.group({
      trainerName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(16)]]
    });
  }

  onSubmit() {
    const trainerName = this.form.get('trainerName').value;

    if (!this.trainerNames.find(tn => tn.toLowerCase() === trainerName.toLowerCase())) {
      this.router.navigate([`trainer/${trainerName}`]);
    } else {
      this.snackBar.open('A trainer with provided name already exists.', 'Close', { duration: 5000 });
    }
  }

}
