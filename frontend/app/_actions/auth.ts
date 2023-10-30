"use server";

import { env } from "@/env.mjs";
import {
  getRefreshToken,
  removeAccessToken,
  removeRefreshToken,
  setAccessToken,
  setRefreshToken,
} from "@/lib/auth";
import {
  AuthWithTokenResDto,
  UserLoginReqDto,
  UserRegisterReqDto,
} from "@/types/converted-dtos/AuthDtos";

import * as z from "zod";
import { loginFormSchema } from "@/components/auth/auth-forms";
import { redirect } from "next/navigation";
import { typedFetch } from "@/lib/fetch";
import { AccessTokenResDto } from "@/types/converted-dtos/TokenDtos";
import { ApiErrorResponse } from "@/lib/errors";

export async function loginUser(values: z.infer<typeof loginFormSchema>) {
  let loginRes = await typedFetch<AuthWithTokenResDto | ApiErrorResponse>(
    `${env.NEXT_PUBLIC_API_URL}/Auth/login`,
    {
      cache: "no-cache",
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        userName: values.username,
        password: values.password,
      } satisfies UserLoginReqDto),
    }
  );

  // TODO: Handle errors

  if ("StatusCode" in loginRes) {
    return loginRes;
  }

  setAccessToken(loginRes.accessToken);
  setRefreshToken(loginRes.refreshToken);

  return loginRes;
}

export async function registerUser(authReq: UserRegisterReqDto) {
  const registrationRes = await typedFetch<AuthWithTokenResDto | ApiErrorResponse>(
    `${env.NEXT_PUBLIC_API_URL}/auth/register`,
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(authReq),
    }
  );

  if ("StatusCode" in registrationRes) {
    return registrationRes;
  }


  setAccessToken(registrationRes.accessToken);
  setRefreshToken(registrationRes.refreshToken);

  return registrationRes;
}

export async function signOutUser() {
  removeAccessToken();
  removeRefreshToken();
}

export async function refreshAccessToken() {
  const refreshToken = getRefreshToken();

  const refreshRes = await typedFetch<AccessTokenResDto | ApiErrorResponse>(
    `${env.NEXT_PUBLIC_API_URL}/refresh`,
    {
      method: "POST",
      cache: "no-cache",
      headers: {
        Authorization: `Bearer ${refreshToken}`,
      },
    }
  );

  if ("StatusCode" in refreshRes) {
    return refreshRes as ApiErrorResponse;
  }

  setAccessToken(refreshRes.token);
  return refreshRes;
}
