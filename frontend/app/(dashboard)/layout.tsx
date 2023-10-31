import { DashboardNav, MainNav } from "@/components/client-nav";
import { dashboardConfig } from "@/config/dashboard";

export default function DashboardLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <div>
      <MainNav items={dashboardConfig.mainNav} />
      <DashboardNav items={dashboardConfig.sidebarNav} />
      <main className="flex w-full flex-1 flex-col overflow-hidden">
        {children}
      </main>
    </div>
  );
}
