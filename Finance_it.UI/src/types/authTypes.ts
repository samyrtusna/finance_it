export type RegisterRequest = {
  name: string;
  email: string;
  password: string;
};

export type LoginRequest = {
  email: string;
  password: string;
};

export type AuthResponse = {
  user: UserState;
};

export type UserState = {
  name: string | null;
  role: string | null;
  accessToken: string | null;
};

export type AuthInitialState = {
  loading: boolean;
  user: UserState | null;
  error: string | null;
};

export type RefreshTokenResponse = {
  accessToken: string | null;
};

export type RefreshTokenInitialState = {
  loading: boolean;
  accessToken: string | null;
  error: string | null;
};
