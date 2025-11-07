import { useAppDispatch, useAppSelector } from "../state/hooks";
import { registerUser } from "../state/slices/authSlices/registerUserSlice";
import { Formik, Form, Field, ErrorMessage } from "formik";
import type { FormikHelpers } from "formik";
import { FiUser } from "react-icons/fi";
import { MdOutlineEmail } from "react-icons/md";
import { MdLockOutline } from "react-icons/md";
import logo from "../assets/logo.png";
import { useNavigate } from "react-router-dom";
import { RegisterSchema } from "../formik/registerSchema";
import type { RegisterRequest } from "../types/authTypes";

type RegisterFormValues = RegisterRequest & {
  confirmPassword: string;
};

function Register() {
  const initialValues: RegisterFormValues = {
    name: "",
    email: "",
    password: "",
    confirmPassword: "",
  };

  const registerState = useAppSelector((state) => state.registerUser);
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const handleSubmit = async (
    values: RegisterRequest,
    props: FormikHelpers<RegisterFormValues>
  ) => {
    const { name, email, password } = values;

    try {
      const response = await dispatch(registerUser({ name, email, password }));
      if (response.meta.requestStatus === "fulfilled") {
        console.log(`${name} signed up successfully`);
        props.setSubmitting(false);
        props.resetForm();
        navigate("/");
      }
    } catch (error) {
      console.error("signup error", error);
    }
  };

  if (registerState.loading) {
    return <h2 className="text-2xl font-bold text-blue-700">Loading...</h2>;
  }
  if (registerState.error) {
    return (
      <div>
        <p className="text-red-500">Error : {registerState.error} </p>
      </div>
    );
  }

  return (
    <div className="flex justify-center items-center h-dvh ">
      <div className="w-full max-w-lg h-full max-h-dvh bg-blue-700 rounded-lg shadow-lg">
        <div className="flex h-1/5 justify-center items-center">
          <img
            src={logo}
            alt="App Logo"
            className="w-20 h-20 filter invert brightness-0"
          />
          <h2 className="text-2xl text-white">Create an Account</h2>
        </div>
        <div className="flex-row w-full h-4/5 bg-gray-50 text-gray-700 p-4 rounded-b-lg rounded-tl-3xl">
          <Formik
            initialValues={initialValues}
            validationSchema={RegisterSchema}
            onSubmit={handleSubmit}
            validateOnMount
          >
            {(formik) => {
              return (
                <Form className="flex flex-col">
                  <div className="flex flex-col content-between">
                    <label
                      htmlFor="name"
                      className="block text-sm font-semibold"
                    >
                      Username
                    </label>
                    <div className="flex items-center w-full rounded-lg shadow-lg p-2">
                      <FiUser className="size-6 text-gray-400" />

                      <Field
                        id="name"
                        name="name"
                        placeholder="Enter your Username"
                        className="w-full p-2 outline-gray-400 focus:placeholder-transparent"
                      />
                    </div>
                    <div className="h-4">
                      <ErrorMessage
                        name="name"
                        component="div"
                        className="text-xs text-red-500"
                      />
                    </div>
                  </div>
                  <div className="flex flex-col content-between">
                    <label
                      htmlFor="email"
                      className="block text-sm font-semibold"
                    >
                      Email
                    </label>
                    <div className="flex items-center w-full rounded-lg shadow-lg p-2">
                      <MdOutlineEmail className="size-6 text-gray-400" />
                      <Field
                        id="email"
                        name="email"
                        placeholder="Enter your Email Address"
                        className="w-full p-2 outline-gray-400 focus:placeholder-transparent"
                      />
                    </div>
                    <div className="h-4">
                      <ErrorMessage
                        name="email"
                        component="div"
                        className="text-xs text-red-500"
                      />
                    </div>
                  </div>
                  <div className="flex flex-col content-between">
                    <label
                      htmlFor="password"
                      className="block text-sm font-semibold"
                    >
                      Password
                    </label>
                    <div className="flex items-center w-full rounded-lg shadow-lg p-2">
                      <MdLockOutline className="size-6 text-gray-400" />

                      <Field
                        id="password"
                        name="password"
                        type="password"
                        placeholder="Enter your password"
                        className="w-full p-2 outline-gray-400 focus:placeholder-transparent"
                      />
                    </div>
                    <div className="h-4">
                      <ErrorMessage
                        name="password"
                        component="div"
                        className="text-xs text-red-500"
                      />
                    </div>
                  </div>
                  <div className="flex flex-col content-between">
                    <label
                      htmlFor="confirmPassword"
                      className="block text-sm font-semibold"
                    >
                      Confirm Password
                    </label>
                    <div className="flex items-center w-full rounded-lg shadow-lg p-2">
                      <MdLockOutline className="size-6 text-gray-400" />
                      <Field
                        id="confirmPassword"
                        name="confirmPassword"
                        type="password"
                        placeholder="Confirm your password"
                        className="w-full p-2 outline-gray-400 focus:placeholder-transparent"
                      />
                    </div>
                    <div className="h-4">
                      <ErrorMessage
                        name="confirmPassword"
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
                      Sign Up
                    </button>
                  </div>
                  <div className="flex justify-center">
                    <p className="text-xs">
                      Dont have an account?
                      <a
                        href="login"
                        className="text-blue-600"
                      >
                        Log In
                      </a>
                    </p>
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

export default Register;
