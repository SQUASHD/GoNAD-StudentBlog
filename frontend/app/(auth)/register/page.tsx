import { registerUser } from "@/app/_actions/auth";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { UserRegisterReqDto } from "@/types/converted-dtos/AuthDtos";
import { redirect } from "next/navigation";

export default async function RegistrationPage() {
  async function handleSubmit(data: FormData) {
    "use server";

    const userName = data.get("userName");
    const firstName = data.get("firstName");
    const lastName = data.get("lastName");
    const email = data.get("email");
    const password = data.get("password");

    if (userName && firstName && lastName && email && password) {
      const registrationData: UserRegisterReqDto = {
        userName: userName.toString(),
        firstName: firstName.toString(),
        lastName: lastName.toString(),
        email: email.toString(),
        password: password.toString(),
      };

      const res = await registerUser(registrationData);
      if (res) {
        redirect("/blog");
      }
    }
  }

  return (
    <div>
      <form action={handleSubmit}>
        <Label>Email</Label>
        <Input name="email" type="email" />
        <Label>Username</Label>
        <Input name="userName" type="text" />
        <Label>First Name</Label>
        <Input name="firstName" type="text" />
        <Label>Last Name</Label>
        <Input name="lastName" type="text" />
        <Label>Password</Label>
        <Input name="password" type="password" />
        <Button type="submit">Register</Button>
      </form>
    </div>
  );
}
