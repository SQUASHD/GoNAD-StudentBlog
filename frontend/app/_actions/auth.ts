"use server";

import { env } from "@/env.mjs";
import { typedFetch } from "@/lib/fetch";
import { cookies } from "next/headers";

export type LoginData = {
  username: string;
  password: string;
};

export type LoginResponse = {
  id: number;
  username: string;
  token: string;
  refreshToken: string;
};

export async function loginUser({ username, password }: LoginData) {
  const res = await typedFetch<LoginResponse>(
    `${env.NEXT_PUBLIC_API_URL}/auth/login`,
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        username: username,
        password: password,
      }),
    }
  );

  cookies().set({
    name: "access_token",
    value: res.token,
    httpOnly: true,
  });

  cookies().set({
    name: "refresh_token",
    value: res.refreshToken,
    httpOnly: true,
  });

  return res;
}

export async function logoutUser() {
  const response = await fetch(`${env.NEXT_PUBLIC_API_URL}/auth/logout`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      username: "hjartland",
      password: "testing123",
    }),
  });

  if (!response.ok) {
    console.log(response);
  }
  cookies().delete("access_token");
}
