import { ComponentFixture, TestBed, tick, fakeAsync } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { PersonListComponent } from './person-list.component';
import { HttpClient } from '@angular/common/http';
import { of } from 'rxjs';

describe('PersonListComponent', () => {
  let component: PersonListComponent;
  let fixture: ComponentFixture<PersonListComponent>;
  let httpClient: HttpClient;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PersonListComponent],
      imports: [HttpClientTestingModule],
    });

    fixture = TestBed.createComponent(PersonListComponent);
    component = fixture.componentInstance;
    httpClient = TestBed.inject(HttpClient);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch and display a list of people', fakeAsync(() => {
    const mockPeople = [
      { id: 1, firstName: 'John', lastName: 'Doe' },
      { id: 2, firstName: 'Jane', lastName: 'Smith' },
    ];

    spyOn(component, 'getPersons').and.callThrough();
    component.ngOnInit();

    const req = httpTestingController.expectOne('/api/Person');
    expect(req.request.method).toEqual('GET');
    req.flush(mockPeople);

    tick();

    expect(component.people).toEqual(mockPeople);
    expect(component.getPersons).toHaveBeenCalled();
  }));

  it('should select a person', () => {
    const personToSelect = { id: 1, firstName: 'John', lastName: 'Doe' };

    component.selectPerson(personToSelect);

    expect(component.selectedPerson).toEqual(personToSelect);
  });

  it('should update the list of people after adding or editing a person', fakeAsync(() => {
    spyOn(component, 'getPersons').and.callThrough();
    component.personAddedOrEdited();

    const req = httpTestingController.expectOne('/api/Person');
    expect(req.request.method).toEqual('GET');
    req.flush([]);

    tick();

    expect(component.getPersons).toHaveBeenCalled();
  }));

  it('should update the list of people after deleting a person', fakeAsync(() => {
    spyOn(component, 'getPersons').and.callThrough();
    component.personDeleted();

    const req = httpTestingController.expectOne('/api/Person');
    expect(req.request.method).toEqual('GET');
    req.flush([]);

    tick();

    expect(component.getPersons).toHaveBeenCalled();
  }));

  afterEach(() => {
    httpTestingController.verify();
  });
});