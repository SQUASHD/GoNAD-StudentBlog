import { Metadata } from "next";
import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";
import { LoginForm } from "@/components/auth/auth-forms";
import { userIsSignedIn } from "@/lib/auth/";
import Link from "next/link";
import { signOutUser } from "@/app/_actions/auth";

export const metadata: Metadata = {
  title: "Student Blog | Login",
  description: "Authentication forms built using the components.",
};

export default async function LoginPage() {
  const signedIn = await userIsSignedIn();

  if (signedIn) {
    return (
      <div className="flex flex-col items-center justify-center h-full">
        <h1 className="text-5xl font-bold tracking-tighter">
          Congratulations!
        </h1>
        <p className="pb-4">You are already signed in</p>
        <div className="flex gap-2">
          <Button asChild>
            <Link href="/blog">To Blog</Link>
          </Button>
          <form action={signOutUser}>
            <Button type="submit">Sign out</Button>
          </form>
        </div>
      </div>
    );
  }
  return <LoginForm />;
}
