import AxiosService from "../axiosInstance";
import type { AxiosError } from "axios";
import store from "../../state/store";

export function setupRequestInterceptor() {
  AxiosService.interceptors.request.use(
    (config) => {
      const token = store.getState().authUser.user?.accessToken;
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    },
    (error: AxiosError) => {
      return Promise.reject(error);
    }
  );
}
