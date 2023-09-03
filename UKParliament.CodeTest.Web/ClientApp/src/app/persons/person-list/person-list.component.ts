// src/app/person-list/person-list.component.ts

import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Person } from '../person.model';

@Component({
  selector: 'app-person-list',
  templateUrl: './person-list.component.html',
  styleUrls: ['./person-list.component.css'],
})
export class PersonListComponent implements OnInit {
  people: Person[] = [];

  constructor(private http: HttpClient) { }

  selectedPerson: Person | null = null;

  selectPerson(person: Person) {
    this.selectedPerson = { ...person };
  }

  ngOnInit() {
    this.getPersons();
  }

  getPersons() {    
    this.http.get<any[]>('/api/Person') 
      .subscribe(response => {
        this.people = response;
      });
  }
}