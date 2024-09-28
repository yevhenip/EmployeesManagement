import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { MatInputModule } from "@angular/material/input";
import { MatSelectModule } from "@angular/material/select";
import { MatFormFieldModule } from "@angular/material/form-field";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { EmployeesService } from "../../services/employees.service";
import { DepartmentsService } from "../../services/departments.service";
import { DepartmentModel } from "../../models/department-model";
import { EmployeeModel } from "../../models/employee-model";
import { MatButton } from "@angular/material/button";
import { catchError, map, Observable, ReplaySubject, take, throwError } from "rxjs";
import { MatCard, MatCardContent, MatCardTitle } from "@angular/material/card";
import { AsyncPipe } from "@angular/common";

@Component({
  selector: 'app-employees-form',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    ReactiveFormsModule,
    MatButton,
    MatCardContent,
    MatCard,
    MatCardTitle,
    AsyncPipe
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './employees-form.component.html',
  styleUrl: './employees-form.component.css'
})
export class EmployeesFormComponent implements OnInit {
  public employeeForm: FormGroup;
  public isEditing = false;
  public employeeId: number = 0;
  public departments$: Observable<DepartmentModel[]>;
  public employees$: Observable<Partial<EmployeeModel>[]>;
  private _errorSubject$: ReplaySubject<string> = new ReplaySubject<string>();

  public get error$(): Observable<string> {
    return this._errorSubject$.asObservable();
  }

  constructor(
    private _fb: FormBuilder,
    private _employeeService: EmployeesService,
    private _departmentService: DepartmentsService,
    private _route: ActivatedRoute,
    private _router: Router
  ) {
    this.departments$ = this._departmentService.getDepartments()
      .pipe(
        take(1),
        map(x => x.departments)
      );

    this.employees$ = this._employeeService.getEmployeesList().pipe(
      take(1),
      map(x => x.employees)
    );

    this.employeeForm = this._fb.group({
      name: ['', Validators.required],
      departmentId: ['', Validators.required],
      managerId: ['', Validators.required],
      salary: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    let id = this._route.snapshot.paramMap.get('id');
    if (!id)
      return;

    this.employeeId = +id;

    if (this.employeeId) {
      this.isEditing = true;
      this._employeeService.getEmployee(this.employeeId).pipe(take(1)).subscribe(x => {
        this.employeeForm.patchValue(x);
      });
    }
  }

  onSubmit(): void {
    let stream: Observable<any>;
    if (this.isEditing) {
      stream = this._employeeService.updateEmployee(this.employeeId, this.employeeForm.value)
    } else {
      stream = this._employeeService.createEmployee(this.employeeForm.value)
    }
    stream.pipe(
      catchError(e => {
        if (e.status === 400 && e.error.detail && e.error.title != "ValidationError") {
          this._errorSubject$.next(e.error.detail || 'An unknown error occurred!');
        } else {
          const validationErrors = e.error.parameters;
          Object.keys(validationErrors).forEach(field => {
            const message = validationErrors[field].join(' ');
            const name = field.toLowerCase();
            if (this.employeeForm.get(name)) {
              this.employeeForm.get(name)!.setErrors({serverError: message});
            }
          });
        }
        return throwError(() => e);
      }),
      take(1)
    )
      .subscribe(async () => {
        await this._router.navigate(['/']);
      });
  }

  onReset(): void {
    this.employeeForm.reset();
    this._errorSubject$.next('');
  }
}
