import { auth, getUserId } from "@/lib/auth";
import { redirect } from "next/navigation";

export default async function RedirecToProfile() {
  await auth("/u");
  const userId = getUserId();

  if (!userId) redirect("/auth/login");

  redirect(`/u/${userId}`);
}
