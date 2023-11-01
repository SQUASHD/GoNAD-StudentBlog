"use client";
import * as z from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import {
  loginUser,
  refreshAccessToken,
  registerUser,
} from "@/app/_actions/auth";
import { Input } from "../ui/input";
import { useState } from "react";
import { ErrorMessage } from "../error";
import { LucideLoader } from "lucide-react";
import { useRouter } from "next/navigation";

export const loginFormSchema = z.object({
  username: z.string().min(3, "Username must be at least 3 characters"),
  password: z.string().min(6, "Password must be at least 6 characters"),
});

export const registerFormSchema = z.object({
  userName: z.string().min(3, "Username must be at least 3 characters"),
  firstName: z.string().min(3, "Username must be at least 3 characters"),
  lastName: z.string().min(3, "Username must be at least 3 characters"),
  email: z.string().email().min(3, "Username must be at least 3 characters"),
  password: z.string().min(6, "Password must be at least 6 characters"),
});

type LoginFormProps = {
  redirectUrl: string;
};

export function LoginForm({ redirectUrl }: LoginFormProps) {
  const [err, setErr] = useState<string | null>(null);
  const [loading, setLoading] = useState<boolean>(false);

  const router = useRouter();

  const loginForm = useForm<z.infer<typeof loginFormSchema>>({
    resolver: zodResolver(loginFormSchema),
    defaultValues: {
      username: "",
      password: "",
    },
  });

  async function submitLogin(data: z.infer<typeof loginFormSchema>) {
    try {
      setLoading(true);
      const res = await loginUser(data);
      if ("StatusCode" in res) {
        setErr(res.Message);
      } else if (res.accessToken) {
        router.push(redirectUrl ?? "/");
      }
    } finally {
      setLoading(false);
    }
  }
  return (
    <div className="max-w-sm sm:max-w-md md:max-w-lg w-full">
      <Form {...loginForm}>
        <form
          onSubmit={loginForm.handleSubmit(submitLogin)}
          className="w-full flex flex-col gap-y-2"
        >
          <FormField
            control={loginForm.control}
            name="username"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Username</FormLabel>
                <FormControl>
                  <Input autoComplete="username" {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={loginForm.control}
            name="password"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Password</FormLabel>
                <FormControl>
                  <Input
                    type="password"
                    autoComplete="current-password"
                    {...field}
                  />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          {loading ? (
            <Button disabled className="w-full">
              {<LucideLoader className="animate-spin" />}
            </Button>
          ) : (
            <Button type="submit" className="w-full">
              Log in
            </Button>
          )}
        </form>
      </Form>
      {err && <ErrorMessage error={err} type="form" />}
    </div>
  );
}

export function RegisterForm() {
  const [err, setErr] = useState<string | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  
  const router = useRouter();

  const registerForm = useForm<z.infer<typeof registerFormSchema>>({
    resolver: zodResolver(registerFormSchema),
    defaultValues: {
      userName: "",
      firstName: "",
      lastName: "",
      email: "",
      password: "",
    },
  });

  async function submitRegister(data: z.infer<typeof registerFormSchema>) {
    try {
      setLoading(true);
      const res = await registerUser(data);
      if ("StatusCode" in res) {
        setErr(res.Message);
      }
      else if (res.accessToken) {
        router.push("/");
      }
    } finally {
      setLoading(false);
    }
  }
  return (
    <div className="max-w-sm sm:max-w-md md:max-w-lg w-full">
      <Form {...registerForm}>
        <form
          onSubmit={registerForm.handleSubmit(submitRegister)}
          className="gap-y-2 w-full grid grid-cols-2 gap-x-2"
        >
          <FormField
            control={registerForm.control}
            name="userName"
            render={({ field }) => (
              <FormItem className="col-span-2">
                <FormLabel>Username</FormLabel>
                <FormControl>
                  <Input {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={registerForm.control}
            name="email"
            render={({ field }) => (
              <FormItem className="col-span-2">
                <FormLabel>Email</FormLabel>
                <FormControl>
                  <Input placeholder="your@email.com" {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={registerForm.control}
            name="password"
            render={({ field }) => (
              <FormItem className="col-span-2">
                <FormLabel>Password</FormLabel>
                <FormControl>
                  <Input type="password" placeholder="" {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={registerForm.control}
            name="firstName"
            render={({ field }) => (
              <FormItem>
                <FormLabel>First Name</FormLabel>
                <FormControl>
                  <Input placeholder="" {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={registerForm.control}
            name="lastName"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Last Name</FormLabel>
                <FormControl>
                  <Input placeholder="" {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <div className="col-span-2">
            {loading ? (
              <Button disabled className="w-full">
                {<LucideLoader className="animate-spin" />}
              </Button>
            ) : (
              <Button type="submit" className="w-full">
                Submit
              </Button>
            )}
          </div>
        </form>
      </Form>
      {err && <ErrorMessage error={err} type="form" />}
    </div>
  );
}

export function RefreshSessionForm() {
  const [err, setErr] = useState<string | null>(null);
  const [loading, setLoading] = useState<boolean>(false);

  async function submitRefesh() {
    try {
      setLoading(true);
      const res = await refreshAccessToken();
      if ("StatusCode" && "Message" in res) {
        setErr(res.Message);
      }
    } catch (error) {
      setErr("Something went wrong.");
    } finally {
      setLoading(false);
    }
  }
  return (
    <div className="max-w-sm sm:max-w-md md:max-w-lg w-full">
      <form action={submitRefesh} className="w-full flex flex-col gap-y-2">
        {loading ? (
          <Button disabled className="w-full">
            {<LucideLoader className="animate-spin" />}
          </Button>
        ) : (
          <Button type="submit" className="w-full">
            Refresh session
          </Button>
        )}
      </form>
      {err && <ErrorMessage error={err} type="form" />}
    </div>
  );
}
