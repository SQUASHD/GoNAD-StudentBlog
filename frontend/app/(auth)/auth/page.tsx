import { Button } from "@/components/ui/button";
import Link from "next/link";

export default function AuthPage() {
  return (
    <div className="flex flex-col">
      <Button className="w-24" asChild>
        <Link href="/auth/login">Login</Link>
      </Button>
      <Button className="w-24" asChild>
        <Link href="/auth/register">Register</Link>
      </Button>
    </div>
  );
}
