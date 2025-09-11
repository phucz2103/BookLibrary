import {
  BarChart,
  Book,
  Calendar,
  FileText,
  Home,
  Settings,
  Users,
} from "lucide-react";
import type React from "react";
import type { MenuItem } from "../types/items";
import { createContext, useContext, useEffect, useState } from "react";
import type { User } from "../types/user";
import { useLocation, useNavigate } from "react-router-dom";
import { authService } from "../services/authService";
import Header from "./Header";
import Sidebar from "./sidebar";

interface LayoutContextType {
  searchTerm: string;
  setSearchTerm: (term: string) => void;
}

const LayoutContext = createContext<LayoutContextType>({
  searchTerm: "",
  setSearchTerm: () => {},
});

// Hook để sử dụng context
export const useLayoutContext = () => {
  const context = useContext(LayoutContext);
  if (!context) {
    throw new Error("useLayoutContext must be used within LayoutWrapper");
  }
  return context;
};

interface LayoutWrapperProps {
  children: React.ReactNode;
}

const LayoutWrapper: React.FC<LayoutWrapperProps> = ({ children }) => {
  const navigate = useNavigate();
  const location = useLocation();

  const [searchTerm, setSearchTerm] = useState("");
  const [user, setUser] = useState<User>({
    userID: "1",
    fullname: "Quản lý thư viện",
    avatar:
      "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=40&h=40&fit=crop&crop=face",
    role: "Admin",
  });

  const menuItems: MenuItem[] = [
    { id: "dashboard", icon: Home, label: "Trang chủ" },
    { id: "books", icon: Book, label: "Quản lý Sách" },
    { id: "readers", icon: Users, label: "Quản lý Độc giả" },
    { id: "lending", icon: FileText, label: "Quản lý Mượn Sách" },
    { id: "returns", icon: Calendar, label: "Quản lý Trả Sách" },
    { id: "reports", icon: BarChart, label: "Báo cáo Thống kê" },
    { id: "settings", icon: Settings, label: "Cài đặt" },
  ];

  const getSelectedMenuFromPath = (pathname: string): string => {
    if (pathname === "/") return "dashboard";
    if (pathname.startsWith("/book")) return "books";
    if (pathname.startsWith("/reader")) return "readers";
    if (pathname.startsWith("/lending")) return "lending";
    if (pathname.startsWith("/returns")) return "returns";
    if (pathname.startsWith("/reports")) return "reports";
    if (pathname.startsWith("/settings")) return "settings";
    return "dashboard";
  };

  const getPageTitleFromPath = (pathname: string): string => {
    if (pathname === "/") return "Trang chủ";
    if (pathname === "/books") return "Quản lý Sách";
    if (pathname.startsWith("/book/")) return "Chi tiết sách";
    if (pathname.startsWith("/reader")) return "Quản lý Độc giả";
    if (pathname.startsWith("/lending")) return "Quản lý Mượn Sách";
    if (pathname.startsWith("/returns")) return "Quản lý Trả Sách";
    if (pathname.startsWith("/reports")) return "Báo cáo Thống kê";
    if (pathname.startsWith("/settings")) return "Cài đặt";
    return "Thư viện";
  };

  //   useEffect(() => {
  //     const currentUser = TokenUtils.getUserData();
  //     if (currentUser) {
  //       setUser(currentUser);
  //     }
  //   }, []);

  const selectedMenu = getSelectedMenuFromPath(location.pathname);
  const currentPageTitle = getPageTitleFromPath(location.pathname);

  const handleMenuSelect = (menuId: string) => {
    switch (menuId) {
      case "dashboard":
        navigate("/");
        break;
      case "books":
        navigate("/books");
        break;
      case "readers":
        navigate("/readers");
        break;
      case "lending":
        navigate("/lending");
        break;
      case "returns":
        navigate("/returns");
        break;
      case "reports":
        navigate("/reports");
        break;
      case "settings":
        navigate("/settings");
        break;
      default:
        navigate("/");
    }
  };

  const handleLogout = async () => {
    try {
      await authService.logout();
      window.location.href = "/login";
    } catch (error) {
      console.error("Lỗi khi đăng xuất:", error);
    }
  };

  return (
    <div className="flex h-screen bg-gray-100">
      {/* Sidebar */}
      <Sidebar
        selectedMenu={selectedMenu}
        onMenuSelect={handleMenuSelect}
        menuItems={menuItems}
      />

      {/* Main Content Area */}
      <div className="flex-1 flex flex-col overflow-hidden">
        {/* Header */}
        <Header
          user={user}
          searchTerm={searchTerm}
          onSearchChange={setSearchTerm}
          onLogout={handleLogout}
          currentPageTitle={currentPageTitle}
        />

        {/* Page Content */}
        <main className="flex-1 overflow-auto p-6">{children}</main>
      </div>
    </div>
  );
};
export default LayoutWrapper;
