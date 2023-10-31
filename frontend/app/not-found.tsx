import Link from "next/link";
import { headers } from "next/headers";
import { FormattedLink } from "@/components/formatted";

export default function NotFound() {
  return (
    <main className="h-full w-screen flex flex-col items-center justify-center">
      <h2 className="text-5xl tracking-tighter font-extrabold">404</h2>
      <p className="text-xl">Could not find requested resource</p>
      <p>
        Back to the <FormattedLink href="/">front page</FormattedLink>
      </p>
    </main>
  );
}
