import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class PersonService {
  private apiUrl = 'http://api/person'; // Replace with your API URL

  constructor(private http: HttpClient) {}

  addPerson(person: any): Observable<any> {
    return this.http.post(this.apiUrl, person);
  }
}