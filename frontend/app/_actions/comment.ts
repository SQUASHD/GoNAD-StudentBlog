"use server";
import { createCommentSchema } from "@/components/comments-form";
import { typedFetchWithAuth } from "@/lib/fetch";
import {
  CommentInputReqDto,
  CommentResDto,
} from "@/types/converted-dtos/CommenDtos";
import { revalidatePath } from "next/cache";
import { z } from "zod";

export async function createComment(data: z.infer<typeof createCommentSchema>) {
  const res = await typedFetchWithAuth<CommentResDto>(
    `/comments/${data.postId}`,
    {
      method: "POST",
      body: JSON.stringify({
        content: data.content,
      } satisfies CommentInputReqDto),
    },
  );

  // TODO: standardize ok response to get type-safe status code
  if ("content" in res) {
    revalidatePath(`/posts/${data.postId}`, "page");
  }
  return res;
}

export async function deleteCommentById(id: string) {
  const res = await typedFetchWithAuth<CommentResDto>(`/comments/${id}`, {
    method: "DELETE",
  });
  if ("content" in res) {
    revalidatePath(`/posts/${res.postId}`, "page");
    revalidatePath(`/dashboard/comments`, "page");
  }
  return res;
}
