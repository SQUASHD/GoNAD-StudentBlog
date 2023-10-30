import { refreshAccessToken } from "@/app/_actions/auth";
import { RefreshSessionForm } from "@/components/auth/auth-forms";
import { Button } from "@/components/ui/button";
import { getAccessToken, getRefreshToken } from "@/lib/auth";
import { redirect } from "next/navigation";

type RefreshSessionParams = {
  redirect: string;
};

export default function RefreshSession({
  params,
}: {
  params: RefreshSessionParams;
}) {
  const redirectUrl = params.redirect ? `/${params.redirect}` : "/";
  const refreshToken = getRefreshToken();
  const accessToken = getAccessToken();

  if (refreshToken && accessToken) redirect(redirectUrl);

  if (!refreshToken) redirect("/auth/login");

  return (
    <div>
      <h1 className="text-5xl font-bold tracking-tighter">Session expired</h1>
      <p className="pb-4">Press the button below to attempt to refresh it.</p>
      <RefreshSessionForm />
    </div>
  );
}
