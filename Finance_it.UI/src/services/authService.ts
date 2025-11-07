import http from "../api/http";
import type {
  LoginRequest,
  RefreshTokenResponse,
  RegisterRequest,
  UserState,
} from "../types/authTypes";

const signup = async (newObject: RegisterRequest): Promise<UserState> => {
  const response = await http.post<UserState, RegisterRequest>(
    "/user/register/",
    newObject
  );
  return response.data;
};

const login = async (newObject: LoginRequest): Promise<UserState> => {
  const response = await http.post<UserState, LoginRequest>(
    "auth/login/",
    newObject
  );
  return response.data;
};

const logout = async (): Promise<void> => {
  await http.post("auth/logout/");
};

const refreshToken = async (): Promise<RefreshTokenResponse> => {
  const response = await http.post<RefreshTokenResponse>("auth/refresh-token");
  return response.data;
};

export default { signup, login, logout, refreshToken };
