import NavBar from "@/components/navigation";

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <main className="h-full w-screen">
      <NavBar />
      {children}
    </main>
  );
}
