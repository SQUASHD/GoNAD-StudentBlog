export type AuthWithTokenResDto = {
  id: number;
  userName: string;
  accessToken: string;
  refreshToken: string;
};
export type UserRegisterReqDto = {
  userName: string;
  firstName: string;
  lastName: string;
  email: string; // [ValidEmail]
  password: string;
};
export type UserLoginReqDto = {
  userName: string;
  password: string;
};
