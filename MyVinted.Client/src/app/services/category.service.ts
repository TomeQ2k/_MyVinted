import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private readonly categoryApiUrl = environment.apiUrl + 'category/';

  constructor(private httpClient: HttpClient) { }

  public fetchCategories() {
    return this.httpClient.get<any>(this.categoryApiUrl + 'all');
  }
}
