import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { EmployeeModel } from "../models/employee-model";
import { UpdateEmployeeModel } from "../models/update-employee-model";
import { BaseService } from "./base.service";

@Injectable({
  providedIn: 'root'
})
export class EmployeesService extends BaseService {
  constructor(httpClient: HttpClient) {
    super(httpClient, "employees")
  }

  getEmployees(): Observable<{ employees: EmployeeModel[] }> {
    return this._httpClient.get<{ employees: EmployeeModel[] }>(`${this._apiUrl}`);
  }

  getEmployeesList(): Observable<{ employees: Partial<EmployeeModel>[] }> {
    return this._httpClient.get<{ employees: Partial<EmployeeModel>[] }>(`${this._apiUrl}`);
  }

  getEmployee(id: number): Observable<EmployeeModel> {
    return this._httpClient.get<EmployeeModel>(`${this._apiUrl}/${id}`);
  }

  createEmployee(employee: UpdateEmployeeModel): Observable<number> {
    return this._httpClient.post<number>(`${this._apiUrl}`, employee);
  }

  updateEmployee(id: number, employee: UpdateEmployeeModel): Observable<void> {
    return this._httpClient.put<void>(`${this._apiUrl}/${id}`, employee);
  }

  deleteEmployee(id: number): Observable<void> {
    return this._httpClient.delete<void>(`${this._apiUrl}/${id}`);
  }
}
