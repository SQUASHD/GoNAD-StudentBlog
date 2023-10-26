import { cookies } from "next/headers";

type FetchOptions = {
  method?: "GET" | "POST" | "PUT" | "DELETE";
  headers?: Record<string, string>;
  body?: any;
};

async function fetchWithAccessToken(endpoint: string, options?: FetchOptions) {
  const url = `${process.env.NEXT_PUBLIC_API_URL}${endpoint}`;

  const cookieStore = cookies();
  const accessToken = cookieStore.get("access_token")?.value;

  if (!accessToken) {
    throw new Error("No access token found");
  }

  console.log(accessToken);

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
  const response = await fetch(url, options);

  if (!response.ok) {
    throw new Error("Failed to fetch");
  }

  return response.json() as Promise<T>;
}

export async function typedFetchWithAccessToken<T>(
  endpoint: string,
  options?: FetchOptions
): Promise<T> {
  const response = await fetchWithAccessToken(endpoint, options);

  if (!response.ok) {
    throw new Error("Failed to fetch");
  }
  return response.json() as Promise<T>;
}
