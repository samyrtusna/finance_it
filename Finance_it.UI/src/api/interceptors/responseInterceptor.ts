import type { AxiosError, AxiosRequestConfig } from "axios";
import AxiosService from "../axiosInstance";
import store from "../../state/store";
import { refreshToken } from "../../state/slices/authSlice";

interface CustomAxiosRequestConfig extends AxiosRequestConfig {
  _retry?: boolean;
}
export function setupResponseInterceptor() {
  AxiosService.interceptors.response.use(
    (response) => response,
    async (error: AxiosError) => {
      const originalRequest = error.config as CustomAxiosRequestConfig;

      if (error.response?.status === 401 && !originalRequest._retry) {
        originalRequest._retry = true;

        try {
          await store.dispatch(refreshToken()).unwrap();
          return AxiosService(originalRequest);
        } catch (refreshTokenError) {
          console.error("Failed to refresh token", refreshTokenError);
          window.location.href = "/login";
          return Promise.reject(refreshTokenError);
        }
      }
      return Promise.reject(error);
    }
  );
}
