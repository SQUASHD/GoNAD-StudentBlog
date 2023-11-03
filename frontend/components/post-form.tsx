"use client";
import { createNewPost, deletePostById } from "@/app/_actions/post";
import { useToast } from "./ui/use-toast";
import { Button } from "./ui/button";
import { Icons } from "./icons";
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
} from "./ui/alert-dialog";
import { QuickToolTip } from "./a11y";
import { useRouter } from "next/navigation";
import { cn } from "@/lib/utils";

export function DeletePostButton({ postId }: { postId: string }) {
  const { toast } = useToast();

  async function deletePost() {
    try {
      const res = await deletePostById(postId);
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
    <AlertDialog>
      <QuickToolTip text="Delete post">
        <AlertDialogTrigger asChild>
          <Button
            size={"icon"}
            variant={"destructive"}
            className="h-8 w-8 aspect-square"
          >
            <Icons.trash className="h-4 aspect-square" />
          </Button>
        </AlertDialogTrigger>
      </QuickToolTip>
      <AlertDialogContent>
        <AlertDialogHeader>
          <AlertDialogTitle>Are you absolutely sure?</AlertDialogTitle>
          <AlertDialogDescription>
            This action cannot be undone. This will permanently delete the post
            and all of its comments.
          </AlertDialogDescription>
        </AlertDialogHeader>
        <AlertDialogFooter>
          <AlertDialogCancel>Cancel</AlertDialogCancel>

          <Button onClick={() => deletePost()} variant="destructive" asChild>
            <AlertDialogAction>Continue</AlertDialogAction>
          </Button>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>
  );
}

type CreateNewPostButtonProps =
  | {
      type: "icon";
      className?: string;
    }
  | {
      type: "text";
      className?: string;
    };

export function CreateNewPostButton({
  type,
  className,
}: CreateNewPostButtonProps) {
  const { toast } = useToast();
  const router = useRouter();

  async function handleClick() {
    try {
      const res = await createNewPost();
      if ("StatusCode" in res) {
        toast({
          title: `Error ${res.StatusCode}`,
          description: `${res.Message}`,
          variant: "destructive",
        });
      } else router.push(`/editor/${res.id}`);
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
  if (type === "icon")
    return (
      <Button
        size={"icon"}
        className={cn("h-8 w-8 aspect-square", className)}
        onClick={() => handleClick()}
      >
        <Icons.plus className="h-4 aspect-square" />
      </Button>
    );
  else if (type === "text")
    return (
      <Button onClick={() => handleClick()} className={cn("", className)}>
        Create new post
      </Button>
    );
}
