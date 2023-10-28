import NavBar from "@/components/navigation";

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <>
      <NavBar></NavBar>
      {children}
    </>
  );
}
