import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import { Providers } from "./providers";
import { env } from "@/env.mjs";

const inter = Inter({ subsets: ["latin"] });

export const metadata: Metadata = {
  title: {
    template: '%s | Student Blog Project',
    default: 'Student Blog Project', 
  },
}

export default async function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  const healthURL = env.NEXT_PUBLIC_API_URL + "/health";
  let healthy = true;
  try {
    await fetch(healthURL);
  } catch (e) {
    healthy = false;
  }

  if (!healthy) {
    return (
      <html lang="en" className="h-full" suppressHydrationWarning>
        <body className={`${inter.className} h-full`}>
          <div className="flex flex-col h-full items-center justify-center">
            <h1 className="text-7xl font-black text-red-500 uppercase tracking-tighter">
              API Server Not On
            </h1>
            <p className="text-4xl font-thin tracking-tight">
              Turn it on, you dummy.
            </p>
          </div>
        </body>
      </html>
    );
  }

  return (
    <html lang="en" className="h-full" suppressHydrationWarning>
      <body className={`${inter.className} h-full`}>
        <Providers>{children}</Providers>
      </body>
    </html>
  );
}
