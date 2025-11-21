import {
  createSlice,
  createAsyncThunk,
  type PayloadAction,
} from "@reduxjs/toolkit";
import authService from "../../services/authService";
import type {
  AuthInitialState,
  LoginRequest,
  RegisterRequest,
  UserState,
} from "../../types/authTypes";

const initialState: AuthInitialState = {
  loading: false,
  user: null,
  error: "",
};

export const registerUser = createAsyncThunk(
  "auth/register",
  async (userData: RegisterRequest, { rejectWithValue }) => {
    try {
      return await authService.signup(userData);
    } catch (error) {
      if (error instanceof Error) {
        return rejectWithValue(error.message);
      }
      return rejectWithValue("Registration failed");
    }
  }
);

export const loginUser = createAsyncThunk(
  "auth/login",
  async (userData: LoginRequest, { rejectWithValue }) => {
    try {
      return await authService.login(userData);
    } catch (error) {
      if (error instanceof Error) {
        return rejectWithValue(error.message);
      }
      return rejectWithValue("Login failed");
    }
  }
);

export const refreshToken = createAsyncThunk(
  "auth/refresh-token",
  async (_, { rejectWithValue }) => {
    try {
      const response = await authService.refreshToken();
      return response.accessToken;
    } catch (error) {
      if (error instanceof Error) {
        return rejectWithValue(error.message);
      }
      return rejectWithValue("Token refresh failed");
    }
  }
);

const authUserSlice = createSlice({
  name: "authUser",
  initialState,
  reducers: {
    logout: (state) => {
      state.user = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(registerUser.pending, (state) => {
        state.loading = true;
      })
      .addCase(loginUser.pending, (state) => {
        state.loading = true;
      })
      .addCase(refreshToken.pending, (state) => {
        state.loading = true;
      })
      .addCase(
        registerUser.fulfilled,
        (state, action: PayloadAction<UserState>) => {
          state.loading = false;
          state.user = action.payload;
        }
      )
      .addCase(
        loginUser.fulfilled,
        (state, action: PayloadAction<UserState>) => {
          state.loading = false;
          state.user = action.payload;
        }
      )
      .addCase(
        refreshToken.fulfilled,
        (state, action: PayloadAction<string>) => {
          state.loading = false;
          if (state.user) {
            state.user.accessToken = action.payload;
          }
        }
      )
      .addCase(registerUser.rejected, (state, action) => {
        state.loading = false;
        state.user = null;
        state.error = (action.payload as string) || "Registration failed";
      })
      .addCase(loginUser.rejected, (state, action) => {
        state.loading = false;
        state.user = null;
        state.error = (action.payload as string) || "Login failed";
      })
      .addCase(refreshToken.rejected, (state, action) => {
        state.loading = false;
        state.user = null;
        state.error = (action.payload as string) || "Token refresh failed";
      });
  },
});

export const { logout } = authUserSlice.actions;
export default authUserSlice.reducer;
