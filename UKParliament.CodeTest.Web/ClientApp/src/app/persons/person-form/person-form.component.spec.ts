import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PersonFormComponent } from './person-form.component';
import { PersonService } from '../person.service';
import { of } from 'rxjs';

describe('PersonFormComponent', () => {
  let component: PersonFormComponent;
  let fixture: ComponentFixture<PersonFormComponent>;
  let personService: jasmine.SpyObj<PersonService>;

  beforeEach(() => {
    // Create a spy for the PersonService methods
    personService = jasmine.createSpyObj('PersonService', ['addPerson', 'editPerson', 'deletePerson']);

    TestBed.configureTestingModule({
      declarations: [PersonFormComponent],
      providers: [{ provide: PersonService, useValue: personService }],
    });

    fixture = TestBed.createComponent(PersonFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should add a new person', () => {
    const newPersonData = {
      id: 0,
      firstName: 'John',
      lastName: 'Doe',
      dateOfBirth: '1990-01-01',
    };

    // Configure the personService spy to return a successful response
    personService.addPerson.and.returnValue(of(newPersonData));

    component.newPerson = newPersonData;
    component.addOrEditPerson();

    // Verify that the addPerson method was called
    expect(personService.addPerson).toHaveBeenCalledWith(newPersonData);   
  });

  it('should edit an existing person', () => {
    const editedPersonData = {
      id: 1,
      firstName: 'Jane',
      lastName: 'Doe',
      dateOfBirth: '1995-02-15',
    };

    // Configure the personService spy to return a successful response
    personService.editPerson.and.returnValue(of(editedPersonData));

    component.newPerson = editedPersonData;
    component.addOrEditPerson();

    // Verify that the editPerson method was called
    expect(personService.editPerson).toHaveBeenCalledWith(editedPersonData);   
  });

  it('should delete a person', () => {
    const personToDelete = {
      id: 2,
      firstName: 'Alice',
      lastName: 'Smith',
      dateOfBirth: '1985-03-20',
    };

    // Configure the personService spy to return a successful response
    personService.deletePerson.and.returnValue(of({}));

    component.newPerson = personToDelete;
    component.deletePerson();

    // Verify that the deletePerson method was called
    expect(personService.deletePerson).toHaveBeenCalledWith(personToDelete);   
  });  
});