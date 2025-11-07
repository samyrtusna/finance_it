import { Link } from "react-router-dom";
import { IoIosNotifications } from "react-icons/io";
import { FaUserCircle } from "react-icons/fa";
import { FiAlignLeft } from "react-icons/fi";

function Navbar() {
  return (
    <div className="flex justify-between items-center p-6">
      <div className="flex justify-start md:hidden">
        <button className="cursor-pointer">
          <FiAlignLeft className="size-5 text-gray-700" />
        </button>
      </div>
      <div className="flex items-center justify-end w-full p-6">
        <Link to="#">
          <IoIosNotifications className="size-5 text-gray-700 mr-5" />
        </Link>
        <Link to="login">
          <FaUserCircle className="size-8 text-blue-700" />
        </Link>
      </div>
    </div>
  );
}

export default Navbar;
