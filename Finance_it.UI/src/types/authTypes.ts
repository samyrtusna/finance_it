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
  name: string;
  role: string;
  accessToken: string;
};

export type AuthInitialState = {
  loading: boolean;
  user: UserState | null;
  error: string;
};

export type RefreshTokenResponse = {
  accessToken: string;
};
