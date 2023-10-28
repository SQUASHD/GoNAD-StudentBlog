import Link from "next/link";

type FormattedLinkProps = {
  href: string;
  children: React.ReactNode;
};

export function FormattedLink({ href, children }: FormattedLinkProps) {
  return (
    <Link href={href} className=" text-blue-600 underline">
      {children}
    </Link>
  );
}
