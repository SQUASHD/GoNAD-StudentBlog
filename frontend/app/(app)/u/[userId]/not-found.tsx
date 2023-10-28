import { FormattedLink } from "@/components/formatted-primitives";
import Link from "next/link";

export default function NotFound() {
  return (
    <div className="h-full w-screen flex flex-col items-center justify-center">
      <h2 className="text-5xl font-semibold tracking-tighter">
        User Not Found
      </h2>
      <p>Sorry, couldn't find a user mathing that User ID</p>
      <FormattedLink href="/">Return Home</FormattedLink>
    </div>
  );
}
