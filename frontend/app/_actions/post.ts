"use server";
import { typedFetchWithAuth } from "@/lib/fetch";
import { CreatePostReqDto, UpdatePostReqDto, PostResDto } from "@/types/converted-dtos/PostDtos";
import { z } from "zod";
import { updatePostSchema } from "@/components/editor-client";
import { revalidatePath } from "next/cache";

export async function createNewPost() {
  let res = await typedFetchWithAuth<PostResDto>("/posts", {
    method: "POST",
    body: JSON.stringify({
      title: "Untitled Post",
      content: "",
    } satisfies CreatePostReqDto),
  });

  return res;
}

export async function updatePost(values: z.infer<typeof updatePostSchema>) {
  let res = await typedFetchWithAuth<PostResDto>(`/posts/${values.postId}`, {
    method: "PUT",
    body: JSON.stringify({
      title: values.title,
      content: values.content,
      status: values.status,
    } satisfies UpdatePostReqDto),
  });
  console.log(res);
  if ("content" in res) {
    revalidatePath(`/posts/${res.id}`, "page");
    revalidatePath(`/dashboard/posts`, "page");
  }

  return res;
}

export async function deletePostById(postId: string) {
  const res = await typedFetchWithAuth<PostResDto>(`/posts/${postId}`, {
    method: "DELETE",
  });
  if ("content" in res) {
    revalidatePath(`/dashboard/posts`, "page");
    revalidatePath(`/posts/${res.id}`, "page");
  }
  return res;
}
