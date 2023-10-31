import { FormattedLink } from "@/components/formatted";

export default function NotFound() {
  return (
    <div className="h-full w-screen flex flex-col items-center justify-center">
      <h2 className="text-5xl font-semibold tracking-tighter">
        User Not Found
      </h2>
      <p>Sorry, couldn&apos;t find a user matching that User ID</p>
      <FormattedLink href="/">Return Home</FormattedLink>
    </div>
  );
}
