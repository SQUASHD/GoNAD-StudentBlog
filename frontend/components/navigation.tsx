import Link from "next/link";

export default function NavBar() {
  return (
    <nav className="w-full h-8 bg-primary">
      <Link href="/"> Home </Link>
    </nav>
  );
}
