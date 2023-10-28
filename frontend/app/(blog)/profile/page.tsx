import { typedFetchWithAccessToken } from "@/lib/fetch";
import { parseJwt } from "@/lib/jwt";
import { cookies } from "next/headers";

type UserResDto = {
  id: number;
  userName: string;
  firstName: string;
  lastName: string;
  email: string;
  createdAt: string;
  updatedAt: string;
};

async function getUserProfile(): Promise<UserResDto | null> {
  const cookieStore = cookies();
  const accessToken = cookieStore.get("access_token")?.value;

  if (!accessToken) {
    return null;
  }

  const decodedToken = parseJwt(accessToken);
  const userId = decodedToken["user_id"];

  const res = await typedFetchWithAccessToken<UserResDto>(`/users/${userId}`);

  return res;
}

export default async function ProfilePage() {
  const user = await getUserProfile();

  if (!user)
    return (
      <main className="w-screen h-full flex flex-col items-center justify-center">
        <h1>Profile</h1>
        <p>User not found.</p>
      </main>
    );

  console.log(user);

  const createdAt = new Date(user.createdAt);
  const updatedAt = new Date(user.updatedAt);

  return (
    <main className="w-screen h-full flex flex-col items-center justify-center">
      <h1>Profile</h1>
      <p>Username: {user.userName}</p>
      <p>First Name: {user.firstName}</p>
      <p>Last Name: {user.lastName}</p>
      <p>Email: {user.email}</p>
      <p>Created At: {createdAt.toLocaleString()}</p>
      <p>Updated At: {updatedAt.toLocaleString()}</p>
    </main>
  );
}
