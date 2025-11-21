export type CreateFinancialEntryRequest = {
  categoryId: number;
  amount: number;
  description: string;
};

export type CurrentWeekAgregates = {
  weekStartDate: string;
  weekEndDate: string;
  weekIncome: number;
  weekExpense: number;
  weekNetCashFlow: number;
};

export type CurrentMonthAgregates = {
  year: number;
  month: string;
  totalIncome: number;
  totalExpense: number;
  netCashFlow: number;
  totalSavings: number;
  netCashFlowRatio: number;
  savingsRate: number;
};

export type CurrentYearAgregates = {
  month: string;
  totalIncome: number;
  totalExpense: number;
  netCashFlow: number;
  totalSavings: number;
  fixedExpensesRatio: number;
  variableExpensesRatio: number;
  netCashFlowRatio: number;
  savingsRate: number;
  debtToIncomeRatio: number;
};

export type CreateFinancialEntryResponse = {
  currentWeekAgregates: CurrentWeekAgregates;
  currentMonthAgregates: CurrentMonthAgregates;
  currentYearAgregates: CurrentYearAgregates;
  netCashFlow: number;
};

export type FinancialEntryType = {
  id: number;
  userId: number;
  categoryId: number;
  category: CategoryType;
  amount: number;
  transactionDate: Date;
  description: string;
};

type CategoryType = {
  id: number;
  name: string;
  type: string;
  expenseType?: string;
};

export type FinancialEntryState = {
  loading: boolean;
  financialEntries: FinancialEntryType[];
  selectedEntry: FinancialEntryType | null;
  error: string;
};

export type NewFinancialEntryState = {
  loading: boolean;
  latestAggregates: CreateFinancialEntryResponse;
  error: string;
};

export type NewFinancialEntryForm = {
  category: string;
  amount: number;
  description: string;
};
