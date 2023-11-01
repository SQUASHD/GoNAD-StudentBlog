import { Role, ValidateRBAC } from "./auth-types";
import { authConfig } from "./auth-config";
import { getAccessToken, getRefreshToken, getUserId } from "./auth-jwt";
import { redirect } from "next/navigation";

/**
 * Checks if the user has the specified role.
 * @param role - The role to check against. Must be one of the roles defined in the system.
 * @returns {boolean} Returns `true` if the user has the specified role, `false` otherwise.
 * @throws {Error} Will throw an error if RBAC is disabled in the configuration.
 * @example
 *
 * try {
 *   const authorized = userHasRole('admin');
 * } catch (error) {
 *   console.error(error);
 * }
 *
 * Note: This function requires RBAC to be enabled. If RBAC is disabled, calling this function
 * will result in an error.
 */
export async function userHasRole(role: ValidateRBAC<Role>): Promise<{
  authenticated: boolean;
  authorized: boolean;
}> {
  if (!authConfig.RBAC) {
    throw new Error("RBAC must be enabled to use this function.");
  }
  if (!authConfig.roles.includes(role)) {
    throw new Error(`Role "${role}" is not defined in the system.`);
  }

  // Simulate a delay in the request.
  await new Promise((resolve) => setTimeout(resolve, 1000));

  return { authenticated: true, authorized: true };
}

export function isUserSignedIn(): boolean {
  const accessToken = getAccessToken();
  const refreshToken = getRefreshToken();

  return !!accessToken && !!refreshToken;
}

type AuthRes = {
  userId: string;
};

export async function auth(redirectTo: string): Promise<AuthRes> {
  const accessToken = getAccessToken();
  const refreshToken = getRefreshToken();

  if (!accessToken && !refreshToken) {
    redirect(`/landing-page`);
  }
  if (refreshToken && !accessToken) {
    redirect(`/auth/refresh?redirect=${redirectTo}`);
  }

  const userId = getUserId();

  if (!userId) {
    redirect(`/auth/login?redirect=${redirectTo}`);
  }

  return { userId };
}
