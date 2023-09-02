// src/app/person-list/person-list.component.ts

import { Component, OnInit } from '@angular/core';
import { Person } from '../person.model';

@Component({
  selector: 'app-person-list',
  templateUrl: './person-list.component.html',
  styleUrls: ['./person-list.component.css'],
})
export class PersonListComponent implements OnInit {
  people: Person[] = [
    { id: 1, firstName: 'John', lastName: 'Doe', age: 30 },
    { id: 2, firstName: 'Jane', lastName: 'Smith', age: 25 },
  ];

  selectedPerson: Person | null = null;

  selectPerson(person: Person) {
    this.selectedPerson = { ...person };
  }

  ngOnInit() {}
}