
// This is the payload of the JWT access token
// Useful for getting identifying information about the user
// Must be kept in sync with the auth server
type JWTAccessTokenPayload = {
  user_id: number;
  exp: number;
  iss: string;
};

// Since a JWT is just a base64 encoded JSON object, we can parse it
// The auth server will take care of validating the JWT and the determining the user's authorization
// Do not use the JWT itself for authorization
export function parseJwt(token: string): JWTAccessTokenPayload {
  return JSON.parse(Buffer.from(token.split(".")[1], "base64").toString());
}


