export interface LastMessage {
  senderId: string;
  senderName: string;
  text: string;
  dateSent: Date;
  isRead: boolean;
  isCurrentUserSender: boolean;
}
