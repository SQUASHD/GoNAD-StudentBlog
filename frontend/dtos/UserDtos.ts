type UserResDto = {
  id: number;
  userName: string;
  firstName: string;
  lastName: string;
  email: string;
  createdAt: string; // Represents a DateTime type in string format
  updatedAt: string; // Represents a DateTime type in string format
}
type UpdateUserInputReqDto = {
  firstName: string;
  lastName: string;
  oldPassword: string;
  newPassword: string;
}
