import { getUserId } from "@/lib/auth";
import { redirect } from "next/navigation";


export default async function RedirecToProfile() {
  const userId = getUserId();

  if (!userId) redirect("/auth/login");
  
  redirect(`/u/${userId}`);
}