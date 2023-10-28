import logError from "@/app/_actions/logger";
import { env } from "@/env.mjs";
import { getAccessToken } from "@/lib/auth/auth-jwt";
import { redirect } from "next/navigation";
type FetchOptions = {
  method?: "GET" | "POST" | "PUT" | "DELETE";
  cache?: "no-cache" | "force-cache" | "no-store";
  headers?: Record<string, string>;
  revalidate?: false | 0 | number;
  body?: any;
};

export type ApiErrorResponse = {
  StatusCode: number;
  Message: string;
};

async function fetchWithAccessToken(endpoint: string, options?: FetchOptions) {
  const url = `${env.NEXT_PUBLIC_API_URL}${endpoint}`;
  const authUrl = `${env.NEXT_PUBLIC_SITE_URL}/api/v1/auth`;

  const accessToken = getAccessToken();

  if (!accessToken) {
    const refreshed = await fetch(authUrl, {
      method: "HEAD",
    });
    if (!refreshed.ok) {
      redirect("/auth/login");
    }
  }

  const headers = {
    ...options?.headers,
    Authorization: `Bearer ${accessToken}`,
    "Content-Type": "application/json",
  };

  return fetch(url, { ...options, headers });
}

export async function typedFetch<T>(
  url: string,
  options?: FetchOptions
): Promise<T> {
  const headers = {
    ...options?.headers,
  };
  const response = await fetch(url, { ...options, headers });

  if (!response.ok) {
    throw new Error("Failed to fetch");
  }

  return response.json() as Promise<T>;
}
export async function typedFetchWithoutAccessToken<T>(
  endpoint: string,
  options?: FetchOptions
): Promise<T | ApiErrorResponse> {
  try {
    const headers = {
      ...options?.headers,
    };
    const res = await fetch(endpoint, { ...options, headers });

    if (!res.ok) {
      const err: ApiErrorResponse = await res.json();
      return err;
    }
    return res.json() as Promise<T>;
  } catch (error) {
    logError({ message: "Error fetching data" }, "critical");
    throw error;
  }
}

export async function typedFetchWithAccessToken<T>(
  endpoint: string,
  options?: FetchOptions
): Promise<T | ApiErrorResponse> {
  try {
    const res = await fetchWithAccessToken(endpoint, options);

    console.log(res);

    if (!res.ok) {
      const err: ApiErrorResponse = await res.json();
      return err;
    }
    return res.json() as Promise<T>;
  } catch (error) {
    logError({ message: "Error fetching data" }, "critical");
    throw error;
  }
}
