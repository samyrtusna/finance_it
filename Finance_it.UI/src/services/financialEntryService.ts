import http from "../api/http";
import type {
  CreateFinancialEntryRequest,
  CreateFinancialEntryResponse,
  FinancialEntryType,
} from "../types/financialEntryTypes";

const addNewEntry = async (
  newObject: CreateFinancialEntryRequest
): Promise<CreateFinancialEntryResponse> => {
  return http.post<CreateFinancialEntryResponse, CreateFinancialEntryRequest>(
    "financialEntry/new",
    newObject
  );
};

const getAllEntries = async (): Promise<FinancialEntryType[]> => {
  return http.get<FinancialEntryType[]>("financialEntry/all");
};

const getEntry = async (id: number): Promise<FinancialEntryType> => {
  return http.get<FinancialEntryType>(`financialEntry/${id}`);
};

const updateEntry = async (
  body: CreateFinancialEntryRequest,
  id: number
): Promise<FinancialEntryType> => {
  return http.put<FinancialEntryType, CreateFinancialEntryRequest>(
    `financialEntry/${id}`,
    body
  );
};

const deleteEntry = async (id: number): Promise<void> => {
  return http.delete<void>(`financialEntry/${id}`);
};

export const financialEntryService = {
  create: addNewEntry,
  getAll: getAllEntries,
  getById: getEntry,
  update: updateEntry,
  remove: deleteEntry,
};
