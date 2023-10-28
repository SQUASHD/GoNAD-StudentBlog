"use server";

import { env } from "@/env.mjs";
import {
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
import logError from "./logger";
import {
  ApiErrorResponse,
  typedFetch,
  typedFetchWithoutAccessToken,
} from "@/lib/fetch";
import * as z from "zod";
import { loginFormSchema } from "@/components/auth/auth-forms";
import { redirect } from "next/navigation";

export async function loginUser(values: z.infer<typeof loginFormSchema>) {
  const loginRes = await typedFetchWithoutAccessToken<AuthWithTokenResDto>(
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

  if ("StatusCode" in loginRes) {
    return loginRes as ApiErrorResponse;
  }
  // Remove any existing tokens before setting the new ones
  removeAccessToken();
  removeRefreshToken();

  setAccessToken(loginRes.accessToken);
  setRefreshToken(loginRes.refreshToken);
  return loginRes;
}

export async function registerUser(authReq: UserRegisterReqDto) {
  const registrationRes =
    await typedFetchWithoutAccessToken<AuthWithTokenResDto>(
      `${env.NEXT_PUBLIC_API_URL}/auth/register`,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(authReq),
      }
    );

  // Remove any existing tokens before setting the new ones
  removeAccessToken();
  removeRefreshToken();

  if ("StatusCode" in registrationRes) {
    return registrationRes as ApiErrorResponse;
  }

  setAccessToken(registrationRes.accessToken);
  setRefreshToken(registrationRes.refreshToken);

  return registrationRes;
}

export async function signOutUser() {
  removeAccessToken();
  removeRefreshToken();
}
