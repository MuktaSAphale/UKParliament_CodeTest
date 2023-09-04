// src/app/person-form/person-form.component.ts

import { Component, OnInit } from '@angular/core';
import { Person } from '../person.model';
import { PersonService } from '../person.service';

@Component({
  selector: 'app-person-form',
  templateUrl: './person-form.component.html',
  styleUrls: ['./person-form.component.css'],
})
export class PersonFormComponent implements OnInit {
  newPerson: any = {};

  constructor(private personService: PersonService) {}

  addPerson() {
    this.personService.addPerson(this.newPerson).subscribe(
      () => {
        // Handle success or show a confirmation message.
        console.log('Person added successfully');
      },
      (error) => {
        // Handle errors.
        console.error('Error adding person:', error);
      }
    );
  }

  ngOnInit() {}
}