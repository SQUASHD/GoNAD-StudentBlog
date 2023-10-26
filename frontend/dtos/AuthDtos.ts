type AuthResDto = {
  id: number;
  userName: string;
}
type AuthWithTokenResDto = {
  id: number;
  userName: string;
  accessToken: string;
  refreshToken: string;
}
type UserRegisterReqDto = {
  userName: string;
  firstName: string;
  lastName: string;
  email: string; // [ValidEmail]
  password: string;
}
type UserLoginReqDto = {
  userName: string;
  password: string;
}
