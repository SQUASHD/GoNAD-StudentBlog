import NavBar from "@/components/navigation";
import { NavigationMenu } from "@/components/ui/navigation-menu";

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <>
      <NavBar></NavBar>
      {children}
    </>
  );
}
