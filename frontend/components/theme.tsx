"use client";

import { useTheme } from "next-themes";
import { Icons } from "./icons";
import { Button } from "./ui/button";
import { cn } from "@/lib/utils";

type DarkModeToggleProps = {
  className?: string;
};

export const DarkModeToggle = (props: DarkModeToggleProps) => {
  const { setTheme, theme } = useTheme();

  return (
    <Button
      variant="ghost"
      size="sm"
      className={cn("w-9 px-0", props.className)}
      onClick={() => (theme === "dark" ? setTheme("light") : setTheme("dark"))}
    >
      <Icons.sun className="rotate-0 scale-100 transition-all dark:-rotate-90 dark:scale-0" />
      <Icons.moon className="absolute rotate-90 scale-0 transition-all dark:rotate-0 dark:scale-100" />
      <span className="sr-only">Toggle theme</span>
    </Button>
  );
};
