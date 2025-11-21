import React from "react";
import Sidebar from "../components/Sidebar";
import { Outlet } from "react-router-dom";
import Navbar from "../components/Navbar";
import Footer from "../components/Footer";

function AppLayout() {
  return (
    <div>
      <div className="grid grid-cols-1 md:grid-cols-4 grid-rows-10 gap-4">
        <div className="hidden md:block md:row-span-10">
          <Sidebar setClick={() => null} />
        </div>
        <div className="md:col-span-3">
          <Navbar />
        </div>
        <div className="md:col-span-3 row-span-9">
          <Outlet />
        </div>
      </div>
      <Footer />
    </div>
  );
}

export default AppLayout;
