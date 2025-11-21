import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import type {
  CreateFinancialEntryRequest,
  CreateFinancialEntryResponse,
  NewFinancialEntryState,
} from "../../types/financialEntryTypes";
import { financialEntryService } from "../../services/financialEntryService";

const emptyAggregates = (): CreateFinancialEntryResponse => ({
  currentWeekAgregates: {
    weekStartDate: "",
    weekEndDate: "",
    weekIncome: 0,
    weekExpense: 0,
    weekNetCashFlow: 0,
  },
  currentMonthAgregates: {
    year: 0,
    month: "",
    totalIncome: 0,
    totalExpense: 0,
    netCashFlow: 0,
    totalSavings: 0,
    fixedExpenses: 0,
    variableExpenses: 0,
  },
  currentYearAgregates: {
    month: "",
    totalIncome: 0,
    totalExpense: 0,
    netCashFlow: 0,
    totalSavings: 0,
    fixedExpenses: 0,
    variableExpenses: 0,
  },
  netCashFlow: 0,
});

const initialState: NewFinancialEntryState = {
  loading: false,
  latestAggregates: emptyAggregates(),
  error: "",
};

export const addNewFinancialEntry = createAsyncThunk(
  "financialEntries/CreateNewEntry",
  async (newEntry: CreateFinancialEntryRequest, { rejectWithValue }) => {
    try {
      return await financialEntryService.create(newEntry);
    } catch (error) {
      if (error instanceof Error) {
        return rejectWithValue(error.message);
      }
      return rejectWithValue("Adding new entry failed");
    }
  }
);

const latestAggregatesSlice = createSlice({
  name: "latestAggregates",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(addNewFinancialEntry.pending, (state) => {
        state.loading = true;
      })
      .addCase(addNewFinancialEntry.fulfilled, (state, action) => {
        state.loading = false;
        state.latestAggregates = action.payload;
      })
      .addCase(addNewFinancialEntry.rejected, (state, action) => {
        state.loading = false;
        state.error =
          (action.payload as string) || "Adding new financial entry failed";
      });
  },
});

export default latestAggregatesSlice.reducer;
