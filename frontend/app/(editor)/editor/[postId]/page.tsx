import Editor from "@/components/editor-client";
import { auth, getAccessToken } from "@/lib/auth";
import { typedFetchWithAuth } from "@/lib/fetch";

import { PostResDto } from "@/types/converted-dtos/PostDtos";
import { notFound } from "next/navigation";

async function getPost(postId: string) {
  return typedFetchWithAuth<PostResDto>(`/posts/${postId}`);
}

export default async function PostEditor({
  params,
}: {
  params: { postId: string };
}) {
  await auth(`/editor/${params.postId}`);
  let post = await getPost(params.postId);


  if ("StatusCode" in post && post.StatusCode === 404) notFound();

  post = post as PostResDto;
  return (
    <Editor
      postContent={post.content}
      postId={`${post.id}`}
      postTitle={post.title}
    />
  );
}
