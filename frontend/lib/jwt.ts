import { cookies } from "next/headers";

// This is the payload of the JWT access token
// Useful for getting identifying information about the user
// Must be kept in sync with the auth server
type JWTAccessTokenPayload = {
  user_id: number;
  exp: number;
  iss: string;
};

// Since a JWT is just a base64 encoded JSON object, we can parse it
// The auth server will take care of validating the JWT and the determining the user's authorization
// Do not use the JWT itself for authorization
export function parseJwt(token: string): JWTAccessTokenPayload {
  return JSON.parse(Buffer.from(token.split(".")[1], "base64").toString());
}

export function setAccessToken(token: string) {
  cookies().set({
    name: "access_token",
    value: token,
    httpOnly: true,
  });
}


// Utility functions for getting and removing the access and refresh tokens
export function getAccessToken() {
  return cookies().get("access_token")?.value;
}

export function removeAccessToken() {
  cookies().delete("access_token");
}

export function getRefreshToken() {
  return cookies().get("refresh_token")?.value;
}

export function setRefreshToken(token: string) {
  cookies().set({
    name: "refresh_token",
    value: token,
    httpOnly: true,
  });
}

export function removeRefreshToken() {
  cookies().delete("refresh_token");
}
