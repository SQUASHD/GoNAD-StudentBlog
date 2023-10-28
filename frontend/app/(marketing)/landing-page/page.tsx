import Link from "next/link";
import { Button } from "@/components/ui/button";

export const metadata = {
  title: "Student Blog Project",
  content: "The landing page for the student blog project.",
};

export default function LandingPage() {
  return (
    <main className="h-full w-screen flex flex-col gap-2 items-center justify-center">
      <h1 className="text-5xl font-extrabold tracking-tighter">
        Student Blog Project
      </h1>
      <p className="max-w-lg text-center leading-tight text-lg">
        This is a project to create a blog for students to share their
        experiences of learning to program.
      </p>
      <div className="flex gap-2">
        <Button asChild className="w-24">
          <Link href="/auth/login">Log in</Link>
        </Button>
        <Button asChild className="w-24">
          <Link href="/auth/register">Register</Link>
        </Button>
      </div>
    </main>
  );
}
