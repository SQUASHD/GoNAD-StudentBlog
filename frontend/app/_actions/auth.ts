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
import { typedFetch } from "@/lib/fetch";

export async function loginUser(req: UserLoginReqDto) {
  let loginRes: AuthWithTokenResDto;
  try {
    loginRes = await typedFetch<AuthWithTokenResDto>(
      `${env.NEXT_PUBLIC_API_URL}/Auth/login`,
      {
        cache: "no-cache",
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          userName: req.userName,
          password: req.password,
        } satisfies UserLoginReqDto),
      }
    );
    setAccessToken(loginRes.accessToken);
    setRefreshToken(loginRes.refreshToken);

    console.log(loginRes);
    return loginRes;
  } catch (error) {
    logError({ message: "Error logging in user" }, "critical");
    throw error;
  }
}

export async function registerUser(authReq: UserRegisterReqDto) {
  let authRes: AuthWithTokenResDto;
  try {
    authRes = await typedFetch<AuthWithTokenResDto>(
      `${env.NEXT_PUBLIC_API_URL}/auth/register`,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(authReq),
      }
    );
    // Make sure to remove any existing tokens before setting the new ones
    removeAccessToken();
    removeRefreshToken();

    setAccessToken(authRes.accessToken);
    setRefreshToken(authRes.refreshToken);

    return authRes;
  } catch (error) {
    logError({ message: "Error registering user" }, "critical");
    throw error;
  }
}
