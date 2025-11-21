import {
  createSlice,
  createAsyncThunk,
  type PayloadAction,
} from "@reduxjs/toolkit";
import { categoryService } from "../../services/CategoryService";
import type {
  CategoryResponseType,
  CategoryState,
} from "../../types/categoryTypes";

const initialState: CategoryState = {
  loading: false,
  categories: [],
  error: "",
};

export const fetchAllCategories = createAsyncThunk(
  "categories/all",
  async (_, { rejectWithValue }) => {
    try {
      return await categoryService.getAll();
    } catch (error) {
      if (error instanceof Error) {
        return rejectWithValue(error.message);
      }
      return rejectWithValue("Fetching all categories failed");
    }
  }
);

const CategorySlice = createSlice({
  name: "categorySlice",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchAllCategories.pending, (state) => {
        state.loading = true;
      })
      .addCase(
        fetchAllCategories.fulfilled,
        (state, action: PayloadAction<CategoryResponseType[]>) => {
          state.loading = false;
          state.categories = action.payload;
        }
      )
      .addCase(fetchAllCategories.rejected, (state, action) => {
        state.loading = false;
        state.categories = [];
        state.error = action.error.message || "fetching all categories failed";
      });
  },
});

export default CategorySlice.reducer;
