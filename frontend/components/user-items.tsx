import { PostResDto } from "@/types/converted-dtos/PostDtos";
import Link from "next/link";

import { Icons } from "./icons";
import { Button } from "./ui/button";
import { DeletePostButton } from "./post-form";
import { Popover, PopoverContent } from "./ui/popover";
import { QuickToolTip } from "./a11y";

type PostRowProps = {
  post: PostResDto;
};

export function PostRow({ post }: PostRowProps) {
  return (
    <div className="w-full h-12 rounded-sm bg-secondary hover:bg-opacity-90 flex items-center px-4 py-2 justify-between">
      <div>
        <span>{post.title}</span>
      </div>
      <div className="flex gap-2">
        <QuickToolTip text="Edit post">
          <Button size={"icon"} asChild className="h-8 w-8 aspect-square">
            <Link href={`/editor/${post.id}`}>
              <Icons.edit className="h-4 aspect-square" />
            </Link>
          </Button>
        </QuickToolTip>
        <QuickToolTip text="View post">
          <Button size={"icon"} asChild className="h-8 w-8 aspect-square">
            <Link href={`/posts/${post.id}`}>
              <Icons.eye className="h-4 aspect-square" />
            </Link>
          </Button>
        </QuickToolTip>

        <DeletePostButton postId={`${post.id}`} />
      </div>
    </div>
  );
}
