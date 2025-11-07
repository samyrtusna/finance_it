import { Formik, Form, Field, ErrorMessage } from "formik";
import type { FormikHelpers } from "formik";
import { LoginSchema } from "../formik/loginSchema";
import { useNavigate } from "react-router-dom";
import logo from "../assets/logo.png";
import { MdLockOutline, MdOutlineEmail } from "react-icons/md";
import { useAppDispatch, useAppSelector } from "../state/hooks";
import { loginUser } from "../state/slices/authSlices/loginUserSlice";
import type { LoginRequest } from "../types/authTypes";

function Login() {
  const initialValues: LoginRequest = { email: "", password: "" };

  const loginState = useAppSelector((state) => state.loginUser);

  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const handleSubmit = async (
    values: LoginRequest,
    props: FormikHelpers<LoginRequest>
  ) => {
    const { email, password } = values;

    try {
      const response = await dispatch(loginUser({ email, password }));
      if (response.meta.requestStatus == "fulfilled") {
        props.setSubmitting(false);
        props.resetForm();
        navigate("/");
      }
    } catch (error) {
      console.error("signup error", error);
    }
  };

  if (loginState.loading) {
    return <h2 className="text-2xl font-bold text-blue-700">Loading...</h2>;
  }

  if (loginState.error) {
    return (
      <div>
        <p className="text-red-500">Error : {loginState.error} </p>
      </div>
    );
  }

  return (
    <div className="flex justify-center  h-dvh ">
      <div className="w-full max-w-lg h-full max-h-dvh bg-blue-700 rounded-lg shadow-lg">
        <div className="flex h-1/3 justify-center items-center  ">
          <img
            src={logo}
            alt="App Logo"
            className="w-40 h-40 filter invert brightness-0"
          />
        </div>
        <div className="w-full h-2/3 bg-gray-50 text-gray-700 rounded-b-lg rounded-tl-3xl">
          <div className="flex justify-center p-6">
            <h2 className="text-2xl font-bold">Log In</h2>
          </div>
          <Formik
            initialValues={initialValues}
            validationSchema={LoginSchema}
            onSubmit={handleSubmit}
            validateOnMount
          >
            {(formik) => {
              return (
                <div className="p-4">
                  <Form className="flex flex-col content-between">
                    <div>
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
                    <div>
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

                    <button
                      type="submit"
                      disabled={
                        !formik.dirty || !formik.isValid || formik.isSubmitting
                      }
                      className="w-full bg-blue-700 text-white font-bold rounded-lg text-center p-2 mt-8 hover:bg-blue-800 cursor-pointer disabled:bg-gray-400"
                    >
                      Log In
                    </button>
                  </Form>
                  <div className="flex justify-center">
                    <p className="text-xs">
                      Dont have an account?
                      <a
                        href="register"
                        className="text-blue-700"
                      >
                        Sign Up
                      </a>
                    </p>
                  </div>
                </div>
              );
            }}
          </Formik>
        </div>
      </div>
    </div>
  );
}

export default Login;
