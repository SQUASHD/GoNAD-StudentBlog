import { Button } from "@/components/ui/button";
import Link from "next/link";

export const dynamic = "force-dynamic";

export default function AuthPage() {
  return (
    <div className="flex flex-col items-center justify-center h-full">
      <h1 className="text-5xl font-bold tracking-tighter">Authentication</h1>
      <p className="py-2">Create an account or sign in</p>
      <div className="flex gap-2">
        <Button asChild className="w-24">
          <Link href="/auth/login">Sign in</Link>
        </Button>

        <Button asChild className="w-24">
          <Link href="/auth/register">Register</Link>
        </Button>
      </div>
    </div>
  );
}
