import {
  createSlice,
  createAsyncThunk,
  type PayloadAction,
} from "@reduxjs/toolkit";
import authService from "../../../services/authService";
import type { RefreshTokenInitialState } from "../../../types/authTypes";

const initialState: RefreshTokenInitialState = {
  loading: false,
  accessToken: null,
  error: null,
};

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

const refreshTokenSlice = createSlice({
  name: "refreshToken",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(refreshToken.pending, (state) => {
        state.loading = true;
      })
      .addCase(
        refreshToken.fulfilled,
        (state, action: PayloadAction<string | null>) => {
          state.loading = false;
          state.accessToken = action.payload;
          state.error = null;
        }
      )
      .addCase(refreshToken.rejected, (state, action) => {
        state.loading = false;
        state.accessToken = null;
        state.error = action.error.message || "Token refresh failed";
      });
  },
});

export default refreshTokenSlice.reducer;
