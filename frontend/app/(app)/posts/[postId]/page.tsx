import { ApiErrorResponse } from "@/lib/errors";
import { typedFetchWithAuth } from "@/lib/fetch";
import { PostResDto } from "@/types/converted-dtos/PostDtos";
import { notFound } from "next/navigation";

export default async function Page({ params }: { params: { postId: string } }) {
  let post: PostResDto | ApiErrorResponse;

  post = await typedFetchWithAuth<PostResDto>(`/posts/${params.postId}`);

  if ("StatusCode" in post && post.StatusCode === 404) notFound();

  post = post as PostResDto;
  return (
    <div>
      <h1>{post.title}</h1>
      <p>{post.content}</p>
    </div>
  );
}
