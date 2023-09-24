import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { EmployeeModel } from '../model/employee.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(public httpModule: HttpClient) { }

  getAll(): Observable<any> {
    return this.httpModule.get(environment.url +'/Employee/getAll');
  }

  getById(id: string): Observable<any> {
    return this.httpModule.get(environment.url +'/Employee/getById/' + id);
  }

  create(emp: EmployeeModel): Observable<any> {    
    return this.httpModule.post(environment.url +'/Employee/add', emp);
  }

  update(emp: EmployeeModel): Observable<any> {
    return this.httpModule.put(environment.url +'/Employee/update/' + emp.id, emp);
  }

  delete(id: string): Observable<any> {
    return this.httpModule.delete(environment.url +'/Employee/delete/' + id);
  }
}
