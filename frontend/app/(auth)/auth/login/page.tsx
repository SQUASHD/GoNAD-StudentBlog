import { Metadata } from "next";
import { Button } from "@/components/ui/button";
import { LoginForm } from "@/components/auth/auth-forms";
import Link from "next/link";
import { signOutUser } from "@/app/_actions/auth";
import { isUserSignedIn } from "@/lib/auth";
import { RedirectUserParams } from "@/types";
import { Fragment } from "react";
import { cn } from "@/lib/utils";

export const metadata: Metadata = {
  title: "Student Blog | Login",
  description: "Authentiation page for the student blog project.",
};

export default async function LoginPage({
  searchParams,
}: {
  searchParams: RedirectUserParams;
}) {
  const isAuth = isUserSignedIn();

  return (
    <Fragment>
      <div className="flex flex-col">
        <div
          className={cn(
            isAuth
              ? "flex flex-col items-center justify-center h-full"
              : "h-0 invisible overflow-hidden",
          )}
        >
          <h1 className="text-5xl font-bold tracking-tighter">
            Congratulations!
          </h1>
          <p className="pb-4">You are already signed in</p>
          <div className="flex gap-2">
            <Button asChild>
              <Link href="/">To Blog</Link>
            </Button>
            <form action={signOutUser}>
              <Button type="submit">Sign out</Button>
            </form>
          </div>
        </div>
        <div
          className={cn(isAuth ? "h-0 invisible overflow-hidden" : "visible")}
        >
          <LoginForm redirectUrl={searchParams.redirect} />
        </div>
      </div>
    </Fragment>
  );
}
