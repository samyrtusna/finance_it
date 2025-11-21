import http from "../api/http";
import type {
  LoginRequest,
  RefreshTokenResponse,
  RegisterRequest,
  UserState,
} from "../types/authTypes";

const signup = async (newObject: RegisterRequest): Promise<UserState> => {
  try {
    const response = await http.post<UserState, RegisterRequest>(
      "/user/register/",
      newObject
    );
    return response;
  } catch (error: any) {
    if (error.response.status === 400) {
      throw new Error("Email is already in use");
    }
    throw new Error("Signup failed");
  }
};

const login = async (newObject: LoginRequest): Promise<UserState> => {
  try {
    const response = await http.post<UserState, LoginRequest>(
      "auth/login/",
      newObject
    );
    return response;
  } catch (error: any) {
    if (error.response?.status === 404) {
      throw new Error("User not registered yet");
    }
    if (error.response?.status === 401) {
      throw new Error("Invalid email or password");
    }
    throw new Error("Login failed");
  }
};

const logout = async (): Promise<void> => {
  await http.post("auth/logout/");
};

const refreshToken = async (): Promise<RefreshTokenResponse> => {
  return await http.post<RefreshTokenResponse>("auth/refresh-token");
};

export default { signup, login, logout, refreshToken };
