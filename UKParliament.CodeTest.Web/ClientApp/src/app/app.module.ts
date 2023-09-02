import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';

import { PersonListComponent } from './persons/person-list/person-list.component';
import { PersonEditorComponent } from './persons/person-editor/person-editor.component';
import { PersonFormComponent } from './persons/person-form/person-form.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    PersonListComponent,
    PersonEditorComponent,
    PersonFormComponent,
  ],
  imports: [    
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
