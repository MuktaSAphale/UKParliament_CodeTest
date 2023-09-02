// src/app/person-editor/person-editor.component.ts

import { Component, OnInit } from '@angular/core';
import { Person } from '../person.model';

@Component({
  selector: 'app-person-editor',
  templateUrl: './person-editor.component.html',
  styleUrls: ['./person-editor.component.css'],
})
export class PersonEditorComponent implements OnInit {
  selectedPerson: Person | null = null;

  savePerson() {
    // Implement save logic here (e.g., update the person in the list).
    console.log('Person saved:', this.selectedPerson);
  }

  ngOnInit() {}
}