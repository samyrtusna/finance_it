import { configureStore } from "@reduxjs/toolkit";
import authUserReducer from "./slices/authSlice";
import categoryReducer from "./slices/categorySlice";
import financialEntryReducer from "./slices/financialEntrySlice";
import latestAggregatesReducer from "./slices/latestAggregatesSlice";

const store = configureStore({
  reducer: {
    authUser: authUserReducer,
    categories: categoryReducer,
    financialEntries: financialEntryReducer,
    latestAggregates: latestAggregatesReducer,
  },
  devTools: import.meta.env.MODE !== "production",
});

export default store;
export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
