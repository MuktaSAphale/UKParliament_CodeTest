// src/app/person-form/person-form.component.ts

import { Component, OnInit } from '@angular/core';
import { Person } from '../person.model';

@Component({
  selector: 'app-person-form',
  templateUrl: './person-form.component.html',
  styleUrls: ['./person-form.component.css'],
})
export class PersonFormComponent implements OnInit {
  newPerson: Person = { id: 0, firstName: '', lastName: '', age: 0 };

  addPerson() {
    // Implement add logic here (e.g., add the person to the list).
    console.log('Person added:', this.newPerson);
  }

  ngOnInit() {}
}