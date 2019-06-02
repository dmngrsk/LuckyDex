import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'ld-trainer-search',
  templateUrl: './trainer-search.component.html',
  styleUrls: ['./trainer-search.component.css']
})
export class TrainerSearchComponent implements OnInit {

  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router: Router
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      trainerName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(16)]]
    });
  }

  onSubmit() {
    const trainerName = this.form.get('trainerName').value;
    this.router.navigate([`trainer/${trainerName}`]);
  }

}
