import { MainNav } from "@/components/client-nav";
import { blogConfig } from "@/config/blog";

export default async function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <div className="min-h-full w-screen flex flex-col relative">
      <div className="flex w-full h-20 items-center justify-center py-6 sticky top-0 bg-background">
        <MainNav items={blogConfig.mainNav} className=" max-w-screen-lg" />
      </div>
      <div className="flex w-full flex-grow flex-col items-center justify-center">
        <div className="max-w-screen-md w-full">{children}</div>
      </div>
    </div>
  );
}
