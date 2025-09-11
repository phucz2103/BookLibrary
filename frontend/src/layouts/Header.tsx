import type React from "react";
import type { User } from "../types/user";
import { LogOut, Search } from "lucide-react";

interface HeaderProps {
  user: User;
  searchTerm: string;
  onSearchChange: (value: string) => void;
  onLogout: () => void;
  currentPageTitle: string;
}

const Header: React.FC<HeaderProps> = ({
  user,
  searchTerm,
  onSearchChange,
  onLogout,
  currentPageTitle,
}) => {
  return (
    <header className="bg-white shadow-sm px-6 py-4">
      <div className="flex items-center justify-between">
        <div className="flex items-center space-x-4">
          <h2 className="text-2xl font-semibold text-gray-800">
            {currentPageTitle}
          </h2>
        </div>

        <div className="flex items-center space-x-4">
          <div className="relative">
            <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-4 h-4" />
            <input
              type="text"
              placeholder="Tìm kiếm sách..."
              value={searchTerm}
              onChange={(e) => onSearchChange(e.target.value)}
              className="pl-10 pr-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
          </div>

          <div className="flex items-center space-x-3">
            <img
              src={user.avatar}
              alt="Avatar"
              className="w-10 h-10 rounded-full object-cover"
            />
            <div className="text-sm">
              <p className="font-medium text-gray-900">{user.fullname}</p>
              <p className="text-gray-500">{user.role}</p>
            </div>
            <button
              onClick={onLogout}
              className="p-2 text-gray-500 hover:text-red-600 hover:bg-red-50 rounded-lg transition-colors"
            >
              <LogOut className="w-5 h-5" />
            </button>
          </div>
        </div>
      </div>
    </header>
  );
};
export default Header;
