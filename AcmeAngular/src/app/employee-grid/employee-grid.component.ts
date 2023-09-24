import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { MatSnackBar, MatSnackBarRef } from '@angular/material/snack-bar';
import { EmployeeModel } from '../model/employee.model';
import { EmployeeService } from '../service/employee-service.service';

@Component({
  selector: 'app-employee-grid',
  templateUrl: './employee-grid.component.html',
  styleUrls: ['./employee-grid.component.scss'],
})
export class EmployeeGridComponent implements OnInit {
  displayedColumns = ['firstName', 'lastName', 'employeeNumber', 'birthDate', 'employeeDate', 'terminationDate', 'edit/delete'];
  dataSource = new MatTableDataSource([]);
  isLoading = false;
  
  constructor(private employeeService: EmployeeService,
    private router: Router, private _snackBar: MatSnackBar) {
  }

  ngOnInit(): void {
    this.getAllList();
  }

  applyFilter(event: any) {
    let filterValue = event.target.value.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  getAllList() {
    this.isLoading = true;
    this.employeeService.getAll().subscribe(
      (data: any) => {
        this.isLoading = false;
        this.dataSource = new MatTableDataSource(data.result);
      },
      (error: any) => {
        this.isLoading = false;
        this._snackBar.open(error.message, 'x');
      }
    );
  }

  AddEmployee() {
    this.router.navigate(['/Employee/add']);
  }

  Edit(data: any) {
    this.router.navigate(['/Employee/edit', data.id]);
  }

  Delete(data: any) {
    this.isLoading = true;
    this.employeeService.delete(data.id).subscribe(
      (datas: any) => {
        this.isLoading = false;
        let index = this.dataSource.data.findIndex((obj: EmployeeModel) => obj.id == data.id);
        if (index !== -1) {
          this.dataSource.data.splice(index, 1);
          this.dataSource._updateChangeSubscription(); // Update the data source
        }
        this._snackBar.open("Employee Deleted Successfully!!", 'x');
      },
      (error: any) => {
        this.isLoading = false;
        this._snackBar.open(error.message, 'x');
      }
    );
  }
}