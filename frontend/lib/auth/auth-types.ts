import { authConfig } from "./auth-config";

export type Role = (typeof authConfig.roles)[number];

export type ValidateRBAC<T> = typeof authConfig.RBAC extends true ? T : never;
