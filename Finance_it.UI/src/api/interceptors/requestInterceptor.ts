import AxiosService from "../axiosInstance";
import type { AxiosError } from "axios";
import store from "../../state/store";

AxiosService.interceptors.request.use(
  (config) => {
    const token = store.getState().registerUser.accessToken;
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error: AxiosError) => {
    return Promise.reject(error);
  }
);
