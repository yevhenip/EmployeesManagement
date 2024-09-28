import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { DepartmentModel } from "../models/department-model";
import { BaseService } from "./base.service";

@Injectable({
  providedIn: 'root'
})
export class DepartmentsService extends BaseService{
  constructor(httpClient: HttpClient) {
    super(httpClient, "departments")
  }

  getDepartments(): Observable<{ departments: DepartmentModel[] }> {
    return this._httpClient.get<{ departments: DepartmentModel[] }>(`${this._apiUrl}`);
  }
}
