import { env } from "@/env.mjs";
import { getAccessToken } from "./auth";
import { ApiError, ApiErrorResponse } from "./errors";

export async function typedFetch<T>(
  url: string,
  options?: RequestInit
): Promise<T> {
  return fetch(url, options).then((res) => res.json());
}

export async function typedFetchWithAuth<T>(
  endpoint: string,
  options?: RequestInit
): Promise<T | ApiErrorResponse> {
  const baseURL = env.NEXT_PUBLIC_API_URL;
  const requestURL = `${baseURL}${endpoint}`;

  const accessToken = getAccessToken();

  if (!accessToken) {
    throw new ApiError(401, "Access token is missing.");
  }

  const authHeaders = {
    Authorization: `Bearer ${accessToken}`,
  };
  const res = await fetch(requestURL, {
    ...options,
    headers: { ...options?.headers, ...authHeaders },
  });

  if (res.ok) {
    return (await res.json()) as T;
  }

  return (await res.json()) as ApiErrorResponse;
}
