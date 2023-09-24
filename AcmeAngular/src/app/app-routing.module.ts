import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {EmployeeGridComponent} from './employee-grid/employee-grid.component'
import { EmployeeAddEditComponent } from './employee-add-edit/employee-add-edit.component';

const routes: Routes = [
  { path: '', component: EmployeeGridComponent,  },    
  { path: 'Employee', component: EmployeeGridComponent  },  
  { path: 'Employee/add', component: EmployeeAddEditComponent },
  { path: 'Employee/edit/:id', component: EmployeeAddEditComponent },  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {  
 }
