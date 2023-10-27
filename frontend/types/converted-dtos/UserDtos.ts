type UserResDto = {
  id: number;
  userName: string;
  firstName: string;
  lastName: string;
  email: string;
  createdAt: string; // Represents a DateTime type in string format
  updatedAt: string; // Represents a DateTime type in string format
}
type UpdateUserInfoReqDto = {
  firstName: string;
  lastName: string;
}
type UpdatePasswordReqDto = {
  oldPassword: string; // [Required]
  newPassword: string; // [Required]
}
