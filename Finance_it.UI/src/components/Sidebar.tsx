import { FaUserCircle } from "react-icons/fa";
import { MdSpaceDashboard } from "react-icons/md";
import { MdRecommend } from "react-icons/md";
import { GiTargetShot } from "react-icons/gi";
import { MdAssessment } from "react-icons/md";
import { IoMdSettings } from "react-icons/io";
import { useAppSelector } from "../state/hooks";
import { Link } from "react-router-dom";

function Sidebar() {
  const userState = useAppSelector((state) => state.loginUser);

  return (
    <div className="max-w-1/5 h-dvh pt-4 lg:ml-[5%] lg:border-l border-r border-gray-200 hidden md:block">
      <div className="flex items-center my-6 pl-4">
        <FaUserCircle className="md:size-6 lg:size-10 text-blue-700 " />
        <h3 className=" font-semibold ml-3">Hello, {userState.user?.name}</h3>
      </div>

      <Link
        to=""
        className="flex w-full rounded-lg p-2 my-2 items-center text-gray-700 hover:text-white hover:bg-blue-700"
      >
        <MdSpaceDashboard className="md:size-4 lg:size-5" />
        <h3 className="text-sm lg:text-lg md:ml-2 lg:ml-3">Dashboard</h3>
      </Link>

      <Link
        to=""
        className="flex w-full rounded-lg p-2 my-2 items-center text-gray-700 hover:text-white hover:bg-blue-700"
      >
        <MdRecommend className="md:size-4 lg:size-5" />
        <h3 className="text-sm lg:text-lg md:ml-2 lg:ml-3">Recommandations</h3>
      </Link>

      <Link
        to=""
        className="flex w-full rounded-lg p-2 my-2 items-center text-gray-700 hover:text-white hover:bg-blue-700"
      >
        <GiTargetShot className="md:size-4 lg:size-5" />
        <h3 className="text-sm lg:text-lg md:ml-2 lg:ml-3">Goals</h3>
      </Link>
      <Link
        to=""
        className="flex w-full rounded-lg p-2 my-2 items-center text-gray-700 hover:text-white hover:bg-blue-700"
      >
        <MdAssessment className="md:size-4 lg:size-5" />
        <h3 className="text-sm lg:text-lg md:ml-2 lg:ml-3">Transactions</h3>
      </Link>
      <Link
        to=""
        className="flex w-full rounded-lg p-2 my-2 items-center text-gray-700 hover:text-white hover:bg-blue-700"
      >
        <IoMdSettings className="md:size-4 lg:size-5" />
        <h3 className="text-sm lg:text-lg md:ml-2 lg:ml-3">Parameters</h3>
      </Link>
    </div>
  );
}

export default Sidebar;
