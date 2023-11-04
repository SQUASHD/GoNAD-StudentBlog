import { signOutUser } from "@/app/_actions/auth";
import { RegisterForm } from "@/components/auth/auth-forms";
import { FormattedLink } from "@/components/formatted";
import { Button } from "@/components/ui/button";
import { isUserSignedIn } from "@/lib/auth";
import { cn } from "@/lib/utils";
import Link from "next/link";
import { Fragment } from "react";

export default async function RegistrationPage() {
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
          <p className="pb-4">You are already registered</p>
          <div className="flex gap-2">
            <Button asChild>
              <Link href="/">To Blog</Link>
            </Button>
            <form action={signOutUser}>
              <Button type="submit">Sign out</Button>
            </form>
          </div>
        </div>
        <div className={cn(isAuth ? "h-0 invisible overflow-hidden" : "")}>
          <RegisterForm />
          <div className="flex flex-col justify-center items-center gap-1 pt-4">
            <h2 className="font-semibold">Already a registered user?</h2>
            <p>
              Click <FormattedLink href="/auth/login">here</FormattedLink> to
              sign in.
            </p>
          </div>
        </div>
      </div>
    </Fragment>
  );
}
