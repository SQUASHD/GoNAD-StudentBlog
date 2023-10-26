import Forbidden from "@/components/auth/forbidden";
import { userHasRole } from "@/lib/auth";
import { redirect } from "next/navigation";

export default async function EditorLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  const { authenticated, authorized } = await userHasRole("admin");

  console.log("authenticated", authenticated);
  console.log("authorized", authorized);

  if (!authenticated) {
    return redirect("/login");
  }
  if (!authorized) {
    return <Forbidden />;
  }

  if (authenticated && authorized) return <>{children}</>;
}
