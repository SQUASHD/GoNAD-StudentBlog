"use client";
import { createComment, deleteCommentById } from "@/app/_actions/comment";
import { useEffect, useState } from "react";
import { z } from "zod";
import { useToast } from "./ui/use-toast";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Form, FormControl, FormField, FormItem, FormMessage } from "./ui/form";
import { Textarea } from "./ui/textarea";
import { Button } from "./ui/button";
import { Icons } from "./icons";

export const createCommentSchema = z.object({
  postId: z.string(),
  content: z
    .string()
    .min(6, "Comment must be at least 3 characters")
    .max(140, "Comment must be less than 140 characters"),
});

type CommentFormProps = {
  postId: string;
};

export default function CommentForm({ postId }: CommentFormProps) {
  const { toast } = useToast();

  async function submitComment(data: z.infer<typeof createCommentSchema>) {
    try {
      const res = await createComment(data);
      if ("StatusCode" in res) {
        toast({
          title: `Error ${res.StatusCode}`,
          description: `${res.Message}`,
          variant: "destructive",
        });
      } else {
        toast({
          title: "Comment created!",
          description: "Your comment has been created.",
        });
      }
    } catch (err) {
      if (err instanceof Error) {
        toast({
          title: "Error",
          description: err.message,
          variant: "destructive",
        });
      }
    }
  }

  const createCommentForm = useForm<z.infer<typeof createCommentSchema>>({
    resolver: zodResolver(createCommentSchema),
    defaultValues: {
      postId: postId,
      content: "",
    },
  });

  const {
    formState: { errors },
  } = createCommentForm;

  useEffect(() => {
    for (const error of Object.values(errors)) {
      if (error) {
        toast({
          title: "Validation Error",
          description: error.message || "You have a validation error",
          variant: "destructive",
        });
      }
    }
  }, [errors, toast]);

  return (
    <Form {...createCommentForm}>
      <form
        onSubmit={createCommentForm.handleSubmit(submitComment)}
        className="w-full flex-grow"
      >
        <FormField
          control={createCommentForm.control}
          name="content"
          render={({ field }) => (
            <FormControl>
              <Textarea
                placeholder="Write a comment..."
                {...field}
                className="w-full relative"
                maxLength={140}
              />
            </FormControl>
          )}
        />
        <FormMessage />
        <div className="flex justify-end w-full mt-4">
          <Button type="submit" className=" self-end">
            Comment
          </Button>
        </div>
      </form>
    </Form>
  );
}

export function DeleteCommentButton({ commentId }: { commentId: string }) {
  const { toast } = useToast();

  async function deleteComment() {
    try {
      const res = await deleteCommentById(commentId);
      if ("StatusCode" in res) {
        toast({
          title: `Error ${res.StatusCode}`,
          description: `${res.Message}`,
          variant: "destructive",
        });
      } else {
        toast({
          title: "Comment deleted!",
          description: "Your comment has been deleted.",
        });
      }
    } catch (err) {
      if (err instanceof Error) {
        toast({
          title: "Error",
          description: err.message,
          variant: "destructive",
        });
      }
    }
  }

  return (
    <Button onClick={deleteComment} className="text-red-600 underline">
      <Icons.trash />
    </Button>
  );
}
