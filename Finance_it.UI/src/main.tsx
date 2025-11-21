import { createRoot } from "react-dom/client";
import { Provider } from "react-redux";
import "./index.css";
import App from "./App.tsx";
import store from "./state/store.ts";
import { setupAxiosInterceptors } from "./api/setupInterceptors.ts";

setupAxiosInterceptors();

createRoot(document.getElementById("root")!).render(
  <Provider store={store}>
    <App />
  </Provider>
);
