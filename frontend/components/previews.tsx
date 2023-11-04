import { CommentResDto } from "@/types/converted-dtos/CommenDtos";
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
  CardTitle,
} from "./ui/card";
import { Button } from "./ui/button";
import Link from "next/link";
import { PostResDto } from "@/types/converted-dtos/PostDtos";
import { UserResDto } from "@/types/converted-dtos/UserDtos";
import Markdown, { defaultUrlTransform } from "react-markdown";
import remarkGfm from "remark-gfm";
import { prettifyDate } from "@/lib/format";
import { MarkdownFormatWrapper } from "./formatted";

type CommentPreviewProps = {
  comment: CommentResDto;
};

export function CommentsCard({ comment }: CommentPreviewProps) {
  return (
    <Card>
      <CardHeader>
        <CardTitle>{comment.userId}</CardTitle>
      </CardHeader>
      <CardContent>{comment.content}</CardContent>
      <CardFooter>
        <Button asChild>
          <Link href={`/posts/${comment.postId}`}>Read the post</Link>
        </Button>
      </CardFooter>
    </Card>
  );
}

type PostPreviewProps = {
  post: PostResDto;
};

export function PostPreview({ post }: PostPreviewProps) {
  return (
    <Card className="w-full">
      <CardHeader className="w-full">
        <CardTitle className="w-full">
          <div className="flex justify-between w-full">
            <span className="max-w-sm">{post.title}</span>
            <div className="flex flex-col">
              <span className="text-sm text-gray-500">
                {prettifyDate(post.createdAt)}
              </span>
              <span className="text-sm text-primary-muted text-right">
                Username
              </span>
            </div>
          </div>
        </CardTitle>
      </CardHeader>
      <CardContent>
        <MarkdownFormatWrapper className="max-h-96 overflow-hidden w-full">
          <Markdown
            urlTransform={defaultUrlTransform}
            remarkPlugins={[remarkGfm]}
          >
            {post.content}
          </Markdown>
        </MarkdownFormatWrapper>
      </CardContent>
      <CardFooter>
        <Button asChild>
          {post.content.length > 1000 ? (
            <Link href={`/posts/${post.id}`}>Read more</Link>
          ) : (
            <Link href={`/posts/${post.id}`}>Read the post</Link>
          )}
        </Button>
      </CardFooter>
    </Card>
  );
}

type UserPreviewProps = {
  user: UserResDto;
};

export function UsersCard({ user }: UserPreviewProps) {
  return (
    <Card>
      <CardHeader>
        <CardTitle>
          {user.firstName} {user.lastName}
        </CardTitle>
      </CardHeader>
      <CardContent>{user.userName}</CardContent>
      <CardFooter>
        <Button asChild>
          <Link href={`/u/${user.id}`}>See profile</Link>
        </Button>
      </CardFooter>
    </Card>
  );
}
