import { Conversation } from "src/app/models/helpers/conversation";
import { BaseResponse } from "./base-response";

export class ConversationsResponse extends BaseResponse {
  conversations: Conversation[];
}
