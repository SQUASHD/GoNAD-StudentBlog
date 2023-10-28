import { signOutUser } from "@/app/_actions/auth";
import { RegisterForm } from "@/components/auth/auth-forms";
import { FormattedLink } from "@/components/formatted-primitives";
import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";
import { userIsSignedIn } from "@/lib/auth";
import Link from "next/link";

export default async function RegistrationPage() {
  const signedIn = await userIsSignedIn();
  if (signedIn) {
    return (
      <div className="flex flex-col items-center justify-center h-full">
        <h1 className="text-5xl font-bold tracking-tighter">
          Congratulations!
        </h1>
        <p className="pb-4">You are already registered</p>
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

  return (
    <div>
      <RegisterForm />
      <div className="flex flex-col justify-center items-center gap-1 pt-4">
        <h2 className="font-semibold">Already a registered user?</h2>
        <p>
          Click <FormattedLink href="/auth/login">here</FormattedLink> to sign
          in.
        </p>
      </div>
    </div>
  );
}
