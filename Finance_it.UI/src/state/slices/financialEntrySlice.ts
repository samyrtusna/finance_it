import {
  createSlice,
  createAsyncThunk,
  type PayloadAction,
} from "@reduxjs/toolkit";
import { financialEntryService } from "../../services/financialEntryService";
import type {
  CreateFinancialEntryRequest,
  FinancialEntryState,
  FinancialEntryType,
} from "../../types/financialEntryTypes";

const initialState: FinancialEntryState = {
  loading: false,
  financialEntries: [],
  selectedEntry: null,
  error: "",
};

const fetchAllEntries = createAsyncThunk(
  "financialEntries/fetchAll",
  async (_, { rejectWithValue }) => {
    try {
      return await financialEntryService.getAll();
    } catch (error) {
      if (error instanceof Error) {
        return rejectWithValue(error.message);
      }
      return rejectWithValue("Fetching all entries failed");
    }
  }
);

const fetchEntryById = createAsyncThunk(
  "financialEntries/fetchEntryById",
  async (id: number, { rejectWithValue }) => {
    try {
      return await financialEntryService.getById(id);
    } catch (error) {
      if (error instanceof Error) {
        return rejectWithValue(error.message);
      }
      return rejectWithValue("Fetching an entry by id failed");
    }
  }
);

const updateEntry = createAsyncThunk(
  "financialEntries/updateEntry",
  async (
    {
      id,
      updatedEntry,
    }: {
      id: number;
      updatedEntry: CreateFinancialEntryRequest;
    },
    { rejectWithValue }
  ) => {
    try {
      return financialEntryService.update(updatedEntry, id);
    } catch (error) {
      if (error instanceof Error) {
        return rejectWithValue(error.message);
      }
      return rejectWithValue("Fetching an entry by id failed");
    }
  }
);

const financialEntrySlice = createSlice({
  name: "financialEntrySlice",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchAllEntries.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchEntryById.pending, (state) => {
        state.loading = true;
      })
      .addCase(updateEntry.pending, (state) => {
        state.loading = true;
      })

      .addCase(
        fetchAllEntries.fulfilled,
        (state, action: PayloadAction<FinancialEntryType[]>) => {
          state.loading = false;
          state.financialEntries = action.payload;
        }
      )
      .addCase(
        fetchEntryById.fulfilled,
        (state, action: PayloadAction<FinancialEntryType>) => {
          state.loading = false;
          state.selectedEntry = action.payload;
        }
      )
      .addCase(
        updateEntry.fulfilled,
        (state, action: PayloadAction<FinancialEntryType>) => {
          state.loading = false;
          state.selectedEntry = action.payload;
        }
      )

      .addCase(fetchAllEntries.rejected, (state, action) => {
        state.loading = false;
        state.financialEntries = [];
        state.error =
          action.error.message || "fetching all financial entries failed";
      })
      .addCase(fetchEntryById.rejected, (state, action) => {
        state.loading = false;
        state.financialEntries = [];
        state.error =
          action.error.message || "fetching a financial entry by id failed";
      })
      .addCase(updateEntry.rejected, (state, action) => {
        state.loading = false;
        state.financialEntries = [];
        state.error =
          action.error.message || "updating a financial entry failed";
      });
  },
});

export default financialEntrySlice.reducer;
