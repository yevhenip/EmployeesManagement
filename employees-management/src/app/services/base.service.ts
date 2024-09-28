import { HttpClient } from "@angular/common/http";

export class BaseService {
  protected _apiUrl = 'https://localhost:7282/api/';

  constructor(protected _httpClient: HttpClient, route: string) {
    this._apiUrl += route;
  }
}
