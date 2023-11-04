export type UserResDto = {
  id: number;
  userName: string;
  firstName: string;
  lastName: string;
  email: string;
  createdAt: string; // Represents a DateTime type in string format
  updatedAt: string; // Represents a DateTime type in string format
};
export type UpdateUserInfoReqDto = {
  firstName: string;
  lastName: string;
};
export type UpdatePasswordReqDto = {
  oldPassword: string; // [Required]
  newPassword: string; // [Required]
};
