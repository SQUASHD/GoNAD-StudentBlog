import { cookies } from "next/headers";

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
