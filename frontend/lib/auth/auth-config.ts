export const authConfig = {
  RBAC: true,
  roles: ["guest", "writer", "editor", "admin"],
  JWTAccessExpiration: 60 * 60 * 24, // 24 hours
  JWTRefreshExpiration: 60 * 60 * 24 * 30, // 30 days
} as const;

type JWTAccessTokenPayload = {
  user_id: number;
  exp: number;
  iss: string;
};