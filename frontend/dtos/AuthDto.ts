type AuthResDto = {
  id: number;
  userName: string;
}
type AuthResWithTokenDto = {
  id: number;
  userName: string;
  token: string;
}
type UserRegisterDto = {
  userName: string;
  firstName: string;
  lastName: string;
  email: string; // [ValidEmail]
  password: string;
}
type PreparedUserDto = {
  userName: string;
  firstName: string;
  lastName: string;
  email: string;
  hashedPassword: string;
  salt: string;
}
type UserLoginDto = {
  userName: string;
  password: string;
}
