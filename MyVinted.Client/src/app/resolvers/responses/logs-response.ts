import { LogModel } from "src/app/models/domain/log-model";
import { BaseResponse } from "./base-response";

export class LogsResponse extends BaseResponse {
  logs: LogModel[];
}
