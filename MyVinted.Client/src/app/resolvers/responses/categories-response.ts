import { Category } from "src/app/models/domain/category";
import { BaseResponse } from "./base-response";

export class CategoriesResponse extends BaseResponse {
  categories: Category[];
}
