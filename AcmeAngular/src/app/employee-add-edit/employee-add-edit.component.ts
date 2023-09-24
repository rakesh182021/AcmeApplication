import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EmployeeService } from '../service/employee-service.service';

@Component({
  selector: 'app-employee-add-edit',
  templateUrl: './employee-add-edit.component.html',
  styleUrls: ['./employee-add-edit.component.scss']
})
export class EmployeeAddEditComponent implements OnInit {

  employeeForm: FormGroup;
  EmployeeId: string = '';
  isLoading = false;

  constructor(private fb: FormBuilder,
    private employeeService: EmployeeService,
    private router: Router, private activatedRoute: ActivatedRoute,
    private _snackBar: MatSnackBar) {

    this.employeeForm = this.fb.group({
      id: [null],
      firstName: [''],
      lastName: [''],
      employeeNumber: [''],
      birthDate: [''],
      employeeDate: [''],
      terminatedDate: ['']
    });
  }

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe((parm: any) => {
      this.EmployeeId = parm.get('id');
    });
    if(this.EmployeeId)
      this.getById(this.EmployeeId);
  }

  getById(EmployeeId: string) {
    this.isLoading = true;
    this.employeeService.getById(EmployeeId).subscribe(
      (data: any) => {
        this.employeeForm.patchValue(data.result);
        this.isLoading = false;

      },
      (error: any) => {
        this.isLoading = false;
        this._snackBar.open(error.message, 'x');
      }
    );
  }

  onSubmit(): void {
    if (this.employeeForm.valid) {
      if (this.employeeForm.value.id !== null &&
        this.employeeForm.value.id !== undefined &&
        this.employeeForm.value.id !== '' &&
        this.employeeForm.value.id !== "") {
        this.isLoading = true;
        this.employeeService.update(this.employeeForm.value).subscribe(
          (data: any) => {
            this.employeeForm.patchValue(data);
            this.isLoading = false;
            this._snackBar.open("Employee Updated Successfully!!", 'x');
            this.router.navigate(['/Employee']);            
          },
          (error: any) => {
            this.isLoading = false;
            this._snackBar.open(error.message, 'x');
          }
        );
      } else {
        this.isLoading = true;
        this.employeeService.create(this.employeeForm.value).subscribe(
          (data: any) => {
            this.isLoading = false;
            this._snackBar.open("Employee Added Successfully!!", 'x');
            this.router.navigate(['/Employee']);            
          },
          (error: any) => {
            this.isLoading = false;
            this._snackBar.open(error.message, 'x');
          }
        );
      }
    } else {
      this.employeeForm.markAllAsTouched();
      this._snackBar.open('Please fill all required details', 'x');      
    }
  }
  
}
