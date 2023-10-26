"use client";

import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Button } from "../ui/button";

export default function AuthForm() {
  return (
    <form className="w-full flex flex-col gap-2">
      <Label htmlFor="username" className="sr-only">
        Username
      </Label>
      <Input type="text" placeholder="Username" />
      <Label htmlFor="password" className="sr-only">
        Password
      </Label>
      <Input type="password" placeholder="Password" />
      <Button type="submit">Sign in</Button>
    </form>
  );
}
