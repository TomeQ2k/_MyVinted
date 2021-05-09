export interface UserProfile {
  id: string;
  email: string;
  userName: string;
  phoneNumber: string;
  emailConfirmed: boolean;
  avatarUrl: string;
  isRegistered: boolean;
  isExternalUser: boolean;
  isVerified: boolean;
  balance: number;
}
