import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { CategoryService } from "../services/category.service";
import { Notifier } from "../services/notifier.service";
import { CategoriesResponse } from "./responses/categories-response";

@Injectable()
export class CategoriesResolver implements Resolve<CategoriesResponse> {
  constructor(private router: Router, private categoryService: CategoryService,
    private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<CategoriesResponse> {
    return this.categoryService.fetchCategories().pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
