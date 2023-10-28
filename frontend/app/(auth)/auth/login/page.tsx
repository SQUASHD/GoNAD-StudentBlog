import { Metadata } from "next";
import { loginUser } from "@/app/_actions/auth";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { redirect } from "next/navigation";
import { Label } from "@/components/ui/label";
import { UserLoginReqDto } from "@/types/converted-dtos/AuthDtos";

export const metadata: Metadata = {
  title: "Authentication",
  description: "Authentication forms built using the components.",
};

export default function LoginPage() {
  async function handleSubmit(data: FormData) {
    "use server";
    const username = data.get("username");
    const password = data.get("password");

    if (username && password) {
      const req: UserLoginReqDto = {
        userName: username.toString(),
        password: password.toString(),
      };

      const res = await loginUser(req);
      if (res) {
        redirect("/");
      }
    }
  }

  return (
    <form action={handleSubmit}>
      <Label>Username</Label>
      <Input name="username" type="text" />
      <Input name="password" type="password" />
      <Button type="submit">Login</Button>
    </form>
  );
}
