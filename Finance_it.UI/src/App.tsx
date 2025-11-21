import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Toaster } from "sonner";
import Register from "./pages/Register";
import Login from "./pages/Login";
import AppLayout from "./layout/AppLayout";
import FinancialEntryForm from "./components/FinancialEntryForm";

function App() {
  return (
    <BrowserRouter>
      <Toaster position="top-right" />
      <Routes>
        <Route
          path="/"
          element={<AppLayout />}
        >
          <Route
            index
            element={<FinancialEntryForm />}
          />
        </Route>
        <Route
          path="register"
          element={<Register />}
        />
        <Route
          path="login"
          element={<Login />}
        />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
