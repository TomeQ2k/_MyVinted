export interface Message {
  id: string;
  text: string;
  dateSent: Date;
  senderId: string;
  recipientId: string;
  isRead: boolean;
  isLiked: boolean;
  senderName: string;
  senderAvatarUrl: string;
  recipientName: string;
  recipientAvatarUrl: string;
}
