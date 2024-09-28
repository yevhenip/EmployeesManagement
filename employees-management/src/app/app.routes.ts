import { Routes } from '@angular/router';
import { EmployeesTableComponent } from "./components/employees-table/employees-table.component";
import { EmployeesFormComponent } from "./components/employees-form/employees-form.component";

export const routes: Routes = [
  { path: '', component: EmployeesTableComponent },
  { path: 'employee/:id', component: EmployeesFormComponent },
  { path: 'employee', component: EmployeesFormComponent }
];
