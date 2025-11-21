import * as Yup from "yup";

export const FinancialEntryFormSchema = Yup.object().shape({
  category: Yup.string().required("Category is required"),
  amount: Yup.number().required("Amount is required"),
  description: Yup.string(),
});
