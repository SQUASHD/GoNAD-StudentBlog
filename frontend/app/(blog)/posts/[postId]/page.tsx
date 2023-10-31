import { FormattedMarkdown } from "@/components/formatted";
import { auth } from "@/lib/auth";
import { ApiErrorResponse } from "@/lib/errors";
import { typedFetchWithAuth } from "@/lib/fetch";
import { PostResDto } from "@/types/converted-dtos/PostDtos";
import { Metadata } from "next";
import { notFound } from "next/navigation";

type Props = {
  params: { postId: string };
};

export async function generateMetadata({ params }: Props): Promise<Metadata> {
  const id = params.postId;
  const post = (await typedFetchWithAuth<PostResDto>(
    `/posts/${id}`
  )) as PostResDto;

  return {
    title: post.title,
  };
}

export default async function Page({ params }: Props) {
  const postId = params.postId ? params.postId : "1";
  await auth(`/posts/${postId}`);

  let post: PostResDto | ApiErrorResponse;

  post = await typedFetchWithAuth<PostResDto>(`/posts/${postId}`);

  if ("StatusCode" in post && post.StatusCode === 404) notFound();

  post = post as PostResDto;
  return (
    <div className="max-w-7xl h-full w-full py-8">
      <FormattedMarkdown markdown={post.content} />
    </div>
  );
}
