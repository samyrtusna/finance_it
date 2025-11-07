import { configureStore } from "@reduxjs/toolkit";
import registerUserReducer from "./slices/authSlices/registerUserSlice";
import loginUserReducer from "./slices/authSlices/loginUserSlice";
import refreshTokenReducer from "./slices/authSlices/refreshTokenSlice";

const store = configureStore({
  reducer: {
    registerUser: registerUserReducer,
    loginUser: loginUserReducer,
    refreshToken: refreshTokenReducer,
  },
  devTools: import.meta.env.MODE !== "production",
});

export default store;
export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
