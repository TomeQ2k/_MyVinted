export interface Opinion {
  id: string;
  text: string;
  rating: number;
  dateAdded: Date;
  userId: string;
  creatorId: string;
  creatorUserName: string;
}
