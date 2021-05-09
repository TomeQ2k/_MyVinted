import { LastMessage } from "./last-message";

export interface Conversation {
  lastMessage: LastMessage;
  recipientId: string
  recipientName: string;
  recipientAvatarUrl: string;
}
