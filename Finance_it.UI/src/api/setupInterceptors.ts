import { setupRequestInterceptor } from "./interceptors/requestInterceptor";
import { setupResponseInterceptor } from "./interceptors/responseInterceptor";

export function setupAxiosInterceptors() {
  setupRequestInterceptor();
  setupResponseInterceptor();
}
