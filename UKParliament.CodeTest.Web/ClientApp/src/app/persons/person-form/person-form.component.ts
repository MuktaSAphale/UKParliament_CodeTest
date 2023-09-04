// src/app/person-form/person-form.component.ts

import { Component, EventEmitter, OnInit, Input, Output, SimpleChanges } from '@angular/core';
import { Person } from '../person.model';
import { PersonService } from '../person.service';
import { PersonListComponent } from '../person-list/person-list.component';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-person-form',
  templateUrl: './person-form.component.html',
  styleUrls: ['./person-form.component.css'],
})
export class PersonFormComponent implements OnInit {
  @Output() personAddedOrEdited = new EventEmitter<any>();
  @Output() personDeleted = new EventEmitter<any>(); 
  @Input() selectedPerson: Person | null = null;
  
  newPerson: { id?: number, firstName?: string, lastName?: string, dateOfBirth?: Date } = {
    id: 0,
    firstName: '',
    lastName: '',
    dateOfBirth: undefined,
  };

  constructor(private personService: PersonService) {}

  ngOnChanges(changes: SimpleChanges) {
    if (changes.selectedPerson) {
      this.newPerson = { ...this.selectedPerson };
    }
  }

  resetPerson()
  {
    this.newPerson = {
      id: 0,
      firstName: '',
      lastName: '',
      dateOfBirth: undefined,
    };
  }

  addOrEditPerson() {
    if(this.newPerson.id == 0)
    {
      this.addPerson();    
    } else
    {
      this.editperson();
    }
  }  

  addPerson()
  {
    this.personService.addPerson(this.newPerson).subscribe(
      () => {
        // Handle success or show a confirmation message.
        console.log('Person added successfully');

        this.personAddedOrEdited.emit();
        this.resetPerson();

      },
      (error) => {
        // Handle errors.
        console.error('Error adding person:', error);
      }  

    );
  }

  editperson()
  {
    this.personService.editPerson(this.newPerson).subscribe(
      () => {
        // Handle success or show a confirmation message.
        console.log('Person edited successfully');

        this.personAddedOrEdited.emit();

        this.resetPerson();

      },
      (error) => {
        // Handle errors.
        console.error('Error editing person:', error);
      }  

    );
  } 

  deletePerson()
  {
    this.personService.deletePerson(this.newPerson).subscribe(
      () => {
        // Handle success or show a confirmation message.
        console.log('Person deleted successfully');

        this.personDeleted.emit();

        this.resetPerson();

      },
      (error) => {
        // Handle errors.
        console.error('Error deleting person:', error);
      }  

    );
  } 

  ngOnInit() {}
}