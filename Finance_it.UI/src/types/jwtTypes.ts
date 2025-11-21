export type JwtPayload = {
  iss?: string;
  sub?: string;
  aud?: string | string[];
  exp?: number;
  nbf?: number;
  iat?: number;
  jti?: string;
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier": number;
};
