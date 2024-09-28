import { Component } from '@angular/core';
import { EmployeesService } from "../../services/employees.service";
import { map, take, tap } from "rxjs";
import { EmployeeModel } from "../../models/employee-model";
import { MatButtonModule } from "@angular/material/button";
import { MatTableModule } from "@angular/material/table";
import { CurrencyPipe } from "@angular/common";
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-employees-table',
  standalone: true,
  imports: [MatButtonModule, MatTableModule, CurrencyPipe, RouterLink],
  templateUrl: './employees-table.component.html',
  styleUrl: './employees-table.component.css',
})
export class EmployeesTableComponent {
  public readonly displayedColumns: string[] = ['id', 'name', 'surname', 'salary', 'managerName', 'departmentName', 'update', 'remove'];
  public employees: EmployeeModel[] = [];

  constructor(private readonly _employeeService: EmployeesService) {
    this._employeeService.getEmployees().pipe(
      map(x => x.employees),
      take(1)
    ).subscribe(e => {
      this.employees = e.map(x => ({...x, canRemove: !!e.find(m => m.managerId == x.id)}))
    });
  }

  public removeEmployee(id: number): void {
    this._employeeService.deleteEmployee(id).pipe(
      tap(_ => this.employees = this.employees.filter((x) => x.id !== id)),
      take(1)
    ).subscribe();
  }
}
