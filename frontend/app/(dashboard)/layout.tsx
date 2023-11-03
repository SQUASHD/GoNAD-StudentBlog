import { DashboardNav, MainNav } from "@/components/client-nav";
import { dashboardConfig } from "@/config/dashboard";
import { auth } from "@/lib/auth";

export default async function DashboardLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  await auth("/dashboard");
  return (
    <div className="flex flex-col h-full w-screen">
      <div className="flex w-full h-20 px-4 border border-b items-center py-6">
        <MainNav items={dashboardConfig.mainNav} />
      </div>
      <div className="flex flex-grow h-full">
        <div className="w-60 h-full p-4">
          <DashboardNav items={dashboardConfig.sidebarNav} />
        </div>
        <main className="flex w-full flex-grow flex-col h-full overflow-hidden">
          {children}
        </main>
      </div>
    </div>
  );
}
