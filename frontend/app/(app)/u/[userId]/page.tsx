import logError from "@/app/_actions/logger";
import { typedFetchWithAuth } from "@/lib/fetch";
import { UserResDto } from "@/types/converted-dtos/UserDtos";
import { notFound } from "next/navigation";

async function getUserProfile(userId: string) {
  const endpoint = `/users/${userId}`;
  return await typedFetchWithAuth<UserResDto>(endpoint);
}

export default async function UserProfile({
  params,
}: {
  params: { userId: string };
}) {
  const { userId } = params;
  const res = await getUserProfile(userId);

  if ("StatusCode" in res && res.StatusCode === 404) notFound();

  const userDto = res as UserResDto;

  return (
    <div>
      <h1>{userDto.userName}</h1>
      <p>{userDto.email}</p>
    </div>
  );
}
