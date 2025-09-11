import type React from "react";
import type { MenuItem } from "../types/items";
import {
  Archive,
  BarChart,
  Book,
  Calendar,
  FileText,
  Home,
  Settings,
  Users,
} from "lucide-react";

interface SidebarProps {
  selectedMenu: string;
  onMenuSelect: (menuId: string) => void;
  menuItems: MenuItem[];
}

const Sidebar: React.FC<SidebarProps> = ({
  selectedMenu,
  onMenuSelect,
  menuItems,
}) => {
  return (
    <div className="w-64 bg-white shadow-lg">
      <div className="p-6 border-b">
        <h1 className="text-xl font-bold text-gray-800">QUẢN LÝ THƯ VIỆN</h1>
      </div>

      <nav className="mt-6">
        {menuItems.map((item) => {
          const Icon = item.icon;
          return (
            <button
              key={item.id}
              onClick={() => onMenuSelect(item.id)}
              className={`w-full flex items-center px-6 py-3 text-left hover:bg-blue-50 transition-colors ${
                selectedMenu === item.id
                  ? "bg-blue-50 border-r-2 border-blue-500 text-blue-600"
                  : "text-gray-600"
              }`}
            >
              <Icon className="w-5 h-5 mr-3" />
              {item.label}
            </button>
          );
        })}
      </nav>
    </div>
  );
};
export default Sidebar;
