import { MainNav } from "@/components/client-nav";
import { blogConfig } from "@/config/blog";

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <div className="h-full w-screen flex flex-col">
      <div className="w-full mx-auto">
        <div className="flex w-full justify-center h-20 items-center py-6">
          <MainNav items={blogConfig.mainNav} />
        </div>
      </div>

      <div className="flex flex-1 items-center justify-center">{children}</div>
    </div>
  );
}
