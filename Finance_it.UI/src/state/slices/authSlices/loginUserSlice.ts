import {
  createSlice,
  createAsyncThunk,
  type PayloadAction,
} from "@reduxjs/toolkit";
import authService from "../../../services/authService";
import type {
  AuthInitialState,
  LoginRequest,
  UserState,
} from "../../../types/authTypes";

const initialState: AuthInitialState = {
  loading: false,
  user: null,
  error: null,
};

export const loginUser = createAsyncThunk(
  "auth/login",
  async (userData: LoginRequest, { rejectWithValue }) => {
    try {
      const response = await authService.login(userData);
      return response;
    } catch (error) {
      if (error instanceof Error) {
        return rejectWithValue(error.message);
      }
      return rejectWithValue("Login failed");
    }
  }
);

const loginUserSlice = createSlice({
  name: "loginUser",
  initialState,
  reducers: {
    logout: (state) => {
      state.user = null;
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(loginUser.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(
        loginUser.fulfilled,
        (state, action: PayloadAction<UserState>) => {
          state.loading = false;
          state.user = action.payload;
          state.error = null;
        }
      )
      .addCase(loginUser.rejected, (state, action) => {
        state.loading = false;
        state.user = null;
        state.error = action.error.message || "Login failed";
      });
  },
});

export const { logout } = loginUserSlice.actions;
export default loginUserSlice.reducer;
