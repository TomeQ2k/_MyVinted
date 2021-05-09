import { Message } from "src/app/models/domain/message";
import { BaseResponse } from "./base-response";

export class MessagesResponse extends BaseResponse {
  messages: Message[];
}
