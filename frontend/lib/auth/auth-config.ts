export const authConfig = {
  RBAC: true,
  roles: ["guest", "writer", "editor", "admin"],
  JWT: true,
  JWTSettings: {
    IdClaim: "user_id",
    accessIssuer: "StudentBlogAPI-Access",
    JWTAccessExpiration: 1000 * 60 * 60 * 24, // 24 hours in milliseconds
    refreshIssuer: "StudentBlogAPI-Refresh",
    JWTRefreshExpiration: 1000 * 60 * 60 * 24 * 30, // 30 days in milliseconds
  },
} as const;

type JWTAccessTokenPayload = {
  user_id: number;
  exp: number;
  iss: string;
};
