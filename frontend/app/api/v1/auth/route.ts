import {
  getAccessToken,
  getRefreshToken,
  removeAccessToken,
  removeRefreshToken,
  setAccessToken,
} from "@/lib/auth";
import { AccessTokenResDto } from "@/types/converted-dtos/TokenDtos";

export async function HEAD() {
  const baseUrl = `${process.env.NEXT_PUBLIC_API_URL}`;

  const accessToken = getAccessToken();
  const refreshToken = getRefreshToken();

  if (!accessToken && !refreshToken) {
    return new Response("Unauthorized", {
      status: 401,
    });
  }

  if (!accessToken && refreshToken) {
    const res = await fetch(baseUrl + "/refresh", {
      cache: "no-cache",
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${refreshToken}`,
      },
    });
    if (!res.ok) {
      removeAccessToken();
      removeRefreshToken();

      return new Response("Unauthorized", {
        status: 401,
      });
    }

    if (res.ok) {
      const { token } = (await res.json()) as AccessTokenResDto;
      setAccessToken(token);
      return new Response("Ok", {
        status: 200,
      });
    }
  }

  if (accessToken && refreshToken) {
    const res = await fetch(baseUrl + "/auth", {
      cache: "no-cache",
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${refreshToken}`,
      },
    });
    if (!res.ok) {
      removeRefreshToken();
      removeAccessToken();

      return new Response("Unauthorized", {
        status: 401,
      });
    }
  }
}
