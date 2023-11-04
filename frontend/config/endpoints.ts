type AllowedMethods = "GET" | "POST" | "PUT" | "DELETE" | "HEAD";
type Endpoint = {
  method: AllowedMethods[];
  path: string;
};

const endpoints: Endpoint[] = [
  {
    method: ["HEAD"],
    path: "/health",
  },
  {
    method: ["POST"],
    path: "/auth/login",
  },
  {
    method: ["POST"],
    path: "/auth/logout",
  },
];
