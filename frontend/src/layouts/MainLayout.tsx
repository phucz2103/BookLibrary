import Sidebar from "./sidebar";
import type { User } from "../types/user";
import Header from "./Header";

interface MainLayoutProps {
  user: User;
  selectedMenu: string;
  searchTerm: string;
  currentPageTitle: string;
  onMenuSelect: (menuId: string) => void;
  onSearchChange: (value: string) => void;
  onLogout: () => void;
  children: React.ReactNode;
}
const MainLayout: React.FC<MainLayoutProps> = ({
  user,
  selectedMenu,
  searchTerm,
  currentPageTitle,
  onMenuSelect,
  onSearchChange,
  onLogout,
  children,
}) => {
  return (
    <div className="flex h-screen bg-gray-100">
      <Sidebar selectedMenu={selectedMenu} onMenuSelect={onMenuSelect} />

      <div className="flex-1 flex flex-col overflow-hidden">
        <Header
          user={user}
          searchTerm={searchTerm}
          onSearchChange={onSearchChange}
          onLogout={onLogout}
          currentPageTitle={currentPageTitle}
        />

        <main className="flex-1 overflow-auto p-6">{children}</main>
      </div>
    </div>
  );
};

export default MainLayout;
