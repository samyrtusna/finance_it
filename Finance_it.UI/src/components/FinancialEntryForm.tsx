import { useAppSelector, useAppDispatch } from "../state/hooks";
import { useNavigate } from "react-router-dom";
import { ErrorMessage, Field, Form, Formik, type FormikHelpers } from "formik";
import { FinancialEntryFormSchema } from "../formik/financialEntryFormSchema";
import { useEffect } from "react";
import { fetchAllCategories } from "../state/slices/categorySlice";
import { addNewFinancialEntry } from "../state/slices/latestAggregatesSlice";
import type { NewFinancialEntryForm } from "../types/financialEntryTypes";

function FinancialEntryForm() {
  const categories = useAppSelector((state) => state.categories);
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  useEffect(() => {
    dispatch(fetchAllCategories());
  }, [dispatch]);

  const initialValues: NewFinancialEntryForm = {
    category: "",
    amount: 0,
    description: "",
  };
  const handleSubmit = async (
    values: NewFinancialEntryForm,
    props: FormikHelpers<NewFinancialEntryForm>
  ) => {
    const { category, amount, description } = values;

    const response = await dispatch(
      addNewFinancialEntry({
        categoryId: Number(category),
        amount,
        description,
      })
    );
    if (response.meta.requestStatus == "fulfilled") {
      props.setSubmitting(false);
      props.resetForm();
      navigate("/");
    }
  };
  return (
    <div className="flex justify-center items-center h-dvh ">
      <div className="w-full max-w-lg h-full max-h-dvh bg-blue-700 rounded-lg shadow-lg">
        <div className="flex h-1/3 justify-center items-center">
          <img
            src="/logo.png"
            alt="App Logo"
            className="w-20 h-20 filter invert brightness-0"
          />
          <h2 className="text-2xl text-white">New Transaction</h2>
        </div>
        <div className="flex-row w-full h-2/3 bg-gray-50 text-gray-700 p-4 rounded-b-lg rounded-tl-3xl">
          <Formik
            initialValues={initialValues}
            validationSchema={FinancialEntryFormSchema}
            onSubmit={handleSubmit}
            validateOnMount
          >
            {(formik) => {
              return (
                <Form className="flex flex-col">
                  <div className="flex flex-col content-between">
                    <label
                      htmlFor="category"
                      className="block text-sm font-semibold"
                    >
                      Category
                    </label>
                    <div className="flex items-center w-full rounded-lg shadow-lg p-2">
                      <Field
                        as="select"
                        id="category"
                        name="category"
                        className="w-full p-2 outline-gray-400 focus:placeholder-transparent"
                      >
                        {categories &&
                          categories.categories.map((category) => {
                            return (
                              <option
                                key={category.id}
                                value={category.id}
                              >
                                {category.name}
                              </option>
                            );
                          })}
                      </Field>
                    </div>
                    <div className="h-4">
                      <ErrorMessage
                        name="category"
                        component="div"
                        className="text-xs text-red-500"
                      />
                    </div>
                  </div>
                  <div className="flex flex-col content-between">
                    <label
                      htmlFor="amount"
                      className="block text-sm font-semibold"
                    >
                      Amount
                    </label>
                    <div className="flex items-center w-full rounded-lg shadow-lg p-2">
                      <Field
                        id="amount"
                        name="amount"
                        type="number"
                        placeholder="Enter your Email Address"
                        className="w-full p-2 outline-gray-400 focus:placeholder-transparent"
                      />
                    </div>
                    <div className="h-4">
                      <ErrorMessage
                        name="amount"
                        component="div"
                        className="text-xs text-red-500"
                      />
                    </div>
                  </div>
                  <div className="flex flex-col content-between">
                    <label
                      htmlFor="description"
                      className="block text-sm font-semibold"
                    >
                      Description
                    </label>
                    <div className="flex items-center w-full rounded-lg shadow-lg p-2">
                      <Field
                        id="description"
                        name="description"
                        as="textarea"
                        placeholder="Enter your password"
                        className="w-full p-2 outline-gray-400 focus:placeholder-transparent"
                      />
                    </div>
                    <div className="h-4">
                      <ErrorMessage
                        name="description"
                        component="div"
                        className="text-xs text-red-500"
                      />
                    </div>
                  </div>

                  <div>
                    <button
                      type="submit"
                      disabled={
                        !formik.dirty || !formik.isValid || formik.isSubmitting
                      }
                      className="w-full bg-blue-700 text-white font-bold rounded-lg text-center p-2 mt-4 hover:bg-blue-800 cursor-pointer disabled:bg-gray-400"
                    >
                      Submit
                    </button>
                  </div>
                </Form>
              );
            }}
          </Formik>
        </div>
      </div>
    </div>
  );
}

export default FinancialEntryForm;
