import type { LucideProps } from "lucide-react";

export interface MenuItem {
  id: string;
  icon: React.ComponentType<LucideProps>;
  label: string;
}
