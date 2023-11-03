import { Icons } from "@/components/icons";
export type { RedirectUserParams } from "@/types/user-flow";

export type NavItem = {
  title: string;
  href: string;
  disabled?: boolean;
};

export type PaginatedSearchParams = {
  page: string;
  size: string;
  // TODO: Add orderBy
  // orderBy: string; 
};

export type MainNavItem = NavItem;

export type SidebarNavItem = {
  title: string;
  disabled?: boolean;
  external?: boolean;
  icon?: keyof typeof Icons;
} & (
  | {
      href: string;
      items?: never;
    }
  | {
      href?: string;
      items: NavItem[];
    }
);

export type SiteConfig = {
  name: string;
  description: string;
  url: string;
};

export type BlogConfig = {
  mainNav: MainNavItem[];
};

export type DashboardConfig = {
  mainNav: MainNavItem[];
  sidebarNav: SidebarNavItem[];
};
