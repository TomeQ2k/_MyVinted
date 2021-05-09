import { logsPageSize } from "src/environments/environment";
import { PaginationRequest } from "./pagination-request";

export class LogsRequest extends PaginationRequest {
  pageSize = logsPageSize;

  date: Date;

  message: string;
  level: string;
  requestMethod: string;
  requestPath: string;
  statusCode?: number;
  exception: string;
  minTime?: Date;
  maxTime?: Date;
  sortType: number = 0;

  constructor() {
    super();
    this.date = new Date(new Date());
    this.date.setDate(this.date.getDate() - 1);
  }
}
