import { cookies } from "next/headers";
import { authConfig } from "./auth-config";

const {
  JWTSettings: { IdClaim: userIdClaim, accessIssuer, refreshIssuer },
} = authConfig;

type JWTAccessTokenPayload = {
  user_id: string;
  exp: number;
  iss: typeof accessIssuer;
};

type JWTRefreshTokenPayload = {
  user_id: string;
  exp: number;
  iss: typeof refreshIssuer;
};

// These values must match the values in the API

export const accessTokenDuration = 1000 * 60 * 60 * 3; // 3 hours in milliseconds
export const refreshTokenDuration = 1000 * 60 * 60 * 24; // 24 hours in milliseconds

export function parseJwt(
  token: string,
): JWTAccessTokenPayload | JWTRefreshTokenPayload {
  const toBuffer: string = token.split(".")[1] ?? "";
  return JSON.parse(Buffer.from(toBuffer, "base64").toString());
}

export function getUserIdFromToken(token: string) {
  return parseJwt(token).user_id;
}

export function getUserId() {
  const refreshToken = getRefreshToken();

  if (!refreshToken) {
    return null;
  }
  return getUserIdFromToken(refreshToken);
}

// Utility functions for getting and removing the access and refresh tokens
export function setAccessToken(token: string) {
  cookies().set({
    name: "access_token",
    value: token,
    httpOnly: true,
    expires: Date.now() + accessTokenDuration,
  });
}

export function getAccessToken() {
  return cookies().get("access_token")?.value;
}

export function removeAccessToken() {
  return cookies().delete("access_token");
}

export function getRefreshToken() {
  return cookies().get("refresh_token")?.value;
}

export function setRefreshToken(token: string) {
  cookies().set({
    name: "refresh_token",
    value: token,
    httpOnly: true,
    expires: Date.now() + refreshTokenDuration,
  });
}

export function removeRefreshToken() {
  return cookies().delete("refresh_token");
}
