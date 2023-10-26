type UserResDto = {
  id: number;
  userName: string;
  firstName: string;
  lastName: string;
  email: string;
  createdAt: string; // Represents a DateTime. Format: ISO string
  updatedAt: string; // Represents a DateTime. Format: ISO string
}
type UpdateUserInput = {
  firstName: string;
  lastName: string;
  oldPassword: string;
  newPassword: string;
}
type UpdateUserData = {
  userId: number;
  firstName: string;
  lastName: string;
  hashedNewPassword: string;
  newSalt: string;
}
