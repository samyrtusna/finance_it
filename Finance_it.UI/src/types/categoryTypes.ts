export type CategoryResponseType = {
  id: number;
  name: string;
  type: string;
  parentCategoryId: number;
  subCategories: CategoryResponseType[];
};

export type CategoryState = {
  loading: boolean;
  categories: CategoryResponseType[];
  error: string;
};
