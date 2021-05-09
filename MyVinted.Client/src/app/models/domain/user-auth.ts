import { UserRole } from "./user-role";

export interface UserAuth {
  id: string;
  email: string;
  userName: string;
  phoneNumber: string;
  emailConfirmed: boolean;
  avatarUrl: string;
  rating: number;
  isExternalUser: boolean;
  isVerified: boolean;

  userRoles: UserRole[];
}
