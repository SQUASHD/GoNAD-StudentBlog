
export const metadata = {
  title: "Auth",
};

export default function AuthLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <main className="h-full w-screen flex items-center justify-center">
      {children}
    </main>
  );
}
