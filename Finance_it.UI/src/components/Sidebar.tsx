import { MdSpaceDashboard } from "react-icons/md";
import { MdRecommend } from "react-icons/md";
import { GiTargetShot } from "react-icons/gi";
import { MdAssessment } from "react-icons/md";
import { IoMdSettings } from "react-icons/io";
import { useAppSelector } from "../state/hooks";
import { Link } from "react-router-dom";
import type { SidebarProps } from "../types/sidebarTypes";

function Sidebar({ setClick }: SidebarProps) {
  const userState = useAppSelector((state) => state.authUser);

  return (
    <div className=" h-dvh lg:ml-[5%] lg:border-l border-r border-gray-200 fixed">
      <div className="flex items-center justify-center mb-6">
        <img
          src="/logo.png"
          alt="App Logo"
          className="w-15 h-15  "
        />
        <h3 className=" font-semibold ">Hello, {userState.user?.name}</h3>
      </div>

      <Link
        to=""
        onClick={setClick}
        className="flex w-full rounded-lg p-2 my-2 items-center text-gray-700 hover:text-white hover:bg-blue-700"
      >
        <MdSpaceDashboard className="size-4 lg:size-5" />
        <h3 className="text-sm lg:text-lg ml-2 lg:ml-3">Dashboard</h3>
      </Link>

      <Link
        to=""
        onClick={setClick}
        className="flex w-full rounded-lg p-2 my-2 items-center text-gray-700 hover:text-white hover:bg-blue-700"
      >
        <MdRecommend className="size-4 lg:size-5" />
        <h3 className="text-sm lg:text-lg ml-2 lg:ml-3">Recommandations</h3>
      </Link>

      <Link
        to=""
        onClick={setClick}
        className="flex w-full rounded-lg p-2 my-2 items-center text-gray-700 hover:text-white hover:bg-blue-700"
      >
        <GiTargetShot className="size-4 lg:size-5" />
        <h3 className="text-sm lg:text-lg ml-2 lg:ml-3">Goals</h3>
      </Link>
      <Link
        to=""
        onClick={setClick}
        className="flex w-full rounded-lg p-2 my-2 items-center text-gray-700 hover:text-white hover:bg-blue-700"
      >
        <MdAssessment className="size-4 lg:size-5" />
        <h3 className="text-sm lg:text-lg ml-2 lg:ml-3">Transactions</h3>
      </Link>
      <Link
        to=""
        onClick={setClick}
        className="flex w-full rounded-lg p-2 my-2 items-center text-gray-700 hover:text-white hover:bg-blue-700"
      >
        <IoMdSettings className="size-4 lg:size-5" />
        <h3 className="text-sm lg:text-lg ml-2 lg:ml-3">Parameters</h3>
      </Link>
    </div>
  );
}

export default Sidebar;
