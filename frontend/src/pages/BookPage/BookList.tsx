import { useState } from "react";
import type { User } from "../../types/user";
import type { MenuItem } from "../../types/items";
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
import { authService } from "../../services/authService";
import { TokenUtils } from "../../utils/TokenUtils";
import BookDashboard from "./BookDashboard";
import MainLayout from "../../layouts/MainLayout";

const BookList: React.FC = () => {
  const [selectedMenu, setSelectedMenu] = useState("dashboard");
  const [searchTerm, setSearchTerm] = useState("");
  const [user, setUser] = useState<User>({
    userID: "abc",
    fullname: "Nguyễn Văn A",
    avatar:
      "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=40&h=40&fit=crop&crop=face",
    role: "Quản lý thư viện",
  });

  const menuItems: MenuItem[] = [
    { id: "dashboard", icon: Home, label: "Trang chủ" },
    { id: "books", icon: Book, label: "Quản lý Sách" },
    { id: "readers", icon: Users, label: "Quản lý Độc giả" },
    { id: "lending", icon: FileText, label: "Quản lý Mượn Sách" },
    { id: "returns", icon: Calendar, label: "Quản lý Trả Sách" },
    { id: "reports", icon: BarChart, label: "Báo cáo Thống kê" },
    { id: "archive", icon: Archive, label: "Quản lý Kho" },
    { id: "settings", icon: Settings, label: "Cài đặt" },
  ];

  if (!TokenUtils.isAuthenticated()) {
    // Redirect to login page
    window.location.href = "/login";
    return;
  }

  //   const currentUser = authService.getCurrentUser();
  //   if (currentUser) {
  //     setUser(currentUser);
  //   }

  const handleLogout = async () => {
    try {
      await authService.logout();
      window.location.href = "/login";
    } catch (error) {
      console.error("Lỗi khi đăng xuất:", error);
    }
  };

  const currentPageTitle =
    menuItems.find((item) => item.id === selectedMenu)?.label || "Trang chủ";

  const renderContent = () => {
    switch (selectedMenu) {
      case "dashboard":
        return <BookDashboard searchTerm={searchTerm} />;
      default:
        return (
          <div className="bg-white rounded-lg shadow-md p-6">
            <h3 className="text-lg font-semibold text-gray-800 mb-4">
              {currentPageTitle}
            </h3>
            <p className="text-gray-600">
              Nội dung cho phần {currentPageTitle} sẽ được phát triển ở đây.
            </p>
          </div>
        );
    }
  };

  return (
    <MainLayout
      user={user}
      selectedMenu={selectedMenu}
      searchTerm={searchTerm}
      currentPageTitle={currentPageTitle}
      onMenuSelect={setSelectedMenu}
      onSearchChange={setSearchTerm}
      onLogout={handleLogout}
    >
      {renderContent()}
    </MainLayout>
  );
};
export default BookList;
