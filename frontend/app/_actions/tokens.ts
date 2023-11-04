"use server";

import { accessTokenDuration } from "@/lib/auth/auth-jwt";
import { cookies } from "next/headers";

export async function setAccessTokenServerSide(token: string) {
  cookies().set({
    name: "access_token",
    value: token,
    httpOnly: true,
    expires: Date.now() + accessTokenDuration,
  });
}
