import http from "../api/http";
import type { CategoryResponseType } from "../types/categoryTypes";

const getAllCategories = async (): Promise<CategoryResponseType[]> => {
  return http.get<CategoryResponseType[]>("category/all");
};

export const categoryService = {
  getAll: getAllCategories,
};
