import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class PersonService {
  private apiUrl = '/api/person/'; 

  constructor(private http: HttpClient) {}

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