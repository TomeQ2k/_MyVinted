import { StatsModel } from "src/app/models/helpers/stats-model";
import { BaseResponse } from "./base-response";

export class StatsResponse extends BaseResponse {
  statsModel: StatsModel;
}
