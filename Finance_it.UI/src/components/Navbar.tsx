import { Link } from "react-router-dom";
import { IoIosNotifications } from "react-icons/io";
import { FaUserCircle } from "react-icons/fa";
import { FiAlignRight } from "react-icons/fi";
import { useState } from "react";
import Sidebar from "./Sidebar";

function Navbar() {
  const [sidebarOpen, setSidebarOpen] = useState(false);

  const toggleSidebarOpen = () => setSidebarOpen(!sidebarOpen);
  const closeSidebar = () => setSidebarOpen(false);

  return (
    <>
      <div className="flex   items-center justify-end py-4">
        <div className="flex w-1/3 items-center justify-end">
          <Link to="#">
            <div className="relative group flex items-center">
              <IoIosNotifications className="size-5 text-gray-700 mr-4" />
              <span
                className=" absolute left-1/2 -translate-x-1/2 top-8 
                             px-2 py-1 text-sm rounded-md
                            bg-gray-800 text-white 
                            opacity-0 group-hover:opacity-100 
                            transition-opacity duration-200
                            pointer-events-none
                            z-50
                          "
              >
                notifications
              </span>
            </div>
          </Link>
          <Link to="login">
            <div className="relative group flex items-center">
              <FaUserCircle className="size-8 text-blue-700 mr-4 cursor-pointer" />

              <span
                className=" absolute left-1/2 -translate-x-1/2 top-10 
                            whitespace-nowrap px-2 py-1 text-sm rounded-md
                            bg-gray-800 text-white 
                            opacity-0 group-hover:opacity-100 
                            transition-opacity duration-200
                            pointer-events-none
                            z-50
                          "
              >
                Log in
              </span>
            </div>
          </Link>
          <button
            onClick={toggleSidebarOpen}
            className="md:hidden cursor-pointer"
          >
            <FiAlignRight className="size-5 text-gray-700 mr-4" />
          </button>
        </div>
      </div>
      <div
        className={`fixed top-0 left-0 w-1/2 sm:w-1/3 md:hidden h-full bg-white shadow-lg transform transition-transform duration-300 z-50 ${
          sidebarOpen ? "translate-x-0" : "-translate-x-full"
        }`}
      >
        <Sidebar setClick={closeSidebar} />
      </div>
    </>
  );
}

export default Navbar;
