import { cookies } from "next/headers";

type JWTAccessTokenPayload = {
  user_id: typeof userIdClaim;
  exp: number;
  iss: typeof accessIssuer;
};

type JWTRefreshTokenPayload = {
  user_id: typeof userIdClaim;
  exp: number;
  iss: typeof refreshIssuer;
};

const accessTokenDuration = 60 * 60 * 24 * 7; // 7 days in seconds
const refreshTokenDuration = 60 * 60 * 24; // 24 hours in seconds
const userIdClaim = "user_id";
const accessIssuer = "StudentBlogAPI-Access";
const refreshIssuer = "StudentBlogAPI-Refresh";

export function parseJwt(
  token: string
): JWTAccessTokenPayload | JWTRefreshTokenPayload {
  return JSON.parse(Buffer.from(token.split(".")[1], "base64").toString());
}

export function getUserIdFromToken(token: string) {
  return parseJwt(token).user_id;
}

export function getUserId() {
  const accessToken = getAccessToken();
  
  if (!accessToken) {
    return null;
  }
  return getUserIdFromToken(accessToken);
}

export function setAccessToken(token: string) {
  cookies().set({
    name: "access_token",
    value: token,
    httpOnly: true,
    expires: accessTokenDuration,
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
    expires: refreshTokenDuration,
  });
}

export function removeRefreshToken() {
  cookies().delete("refresh_token");
}
