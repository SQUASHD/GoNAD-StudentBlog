import { FormattedLink } from "@/components/formatted-primitives";

export default function NotFound() {
  return (
    <div className="h-full w-screen flex flex-col items-center justify-center">
    <h2 className="text-5xl font-semibold tracking-tighter">
      Post Not Found
    </h2>
    <p>Sorry, couldn&apos;t find a post matching that PostId</p>
    <FormattedLink href="/">Return Home</FormattedLink>
  </div>
  );
}