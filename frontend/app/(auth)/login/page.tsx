import { Metadata } from "next";
import AuthForm from "@/components/auth/login-form";
import { Card } from "@/components/ui/card";

import { loginUser } from "@/app/_actions/auth";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { redirect } from "next/navigation";

export const metadata: Metadata = {
  title: "Authentication",
  description: "Authentication forms built using the components.",
};

type LoginData = {
  username: string;
  password: string;
};

export default function AuthenticationPage() {
  async function handleSubmit(data: FormData) {
    "use server";
    const username = data.get("username");
    const password = data.get("password");

    if (username && password) {
      const loginData: LoginData = {
        username: username.toString(),
        password: password.toString(),
      };

      console.log(loginData);

      const res = await loginUser(loginData);
      if (res) {
        redirect("/");
      }
    }
  }

  return (
    <form action={handleSubmit}>
      <Input name="username" type="text" />
      <Input name="password" type="password" />
      <Button type="submit">Login</Button>
    </form>
  );
}
