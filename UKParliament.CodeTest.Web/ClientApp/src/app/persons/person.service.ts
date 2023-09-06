import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Person } from './person.model';
import { PersonViewModel } from '../../models/person-view-model';

@Injectable({
  providedIn: 'root'
})

export class PersonService {
  private apiUrl = '/api/person/'; 

  constructor(private http: HttpClient) {}

  getAllPeople(): Observable<Person[]> {
    return this.http
      .get<PersonViewModel[]>('/api/person/')
      .pipe(
        map((data) =>
          data.map((viewModel) =>
            this.mapPersonViewModelToPerson(viewModel)
          )
        )
      );
  }

  private mapPersonViewModelToPerson(viewModel: PersonViewModel): Person {
    const person = new Person();
    person.id = viewModel.id;    
    person.firstName = viewModel.firstName;
    person.lastName = viewModel.lastName;
    if (viewModel.dateOfBirth) {
      person.dateOfBirth = new Date(viewModel.dateOfBirth);
    }
    return person;
  }

  addPerson(person: any): Observable<any> {
    return this.http.post(this.apiUrl, person);
  }

  editPerson(person: any): Observable<any> {
    return this.http.put(this.apiUrl +  person.id, person);
  }

  deletePerson(person: any): Observable<any> {
    return this.http.delete(this.apiUrl +  person.id, person);
  }
}