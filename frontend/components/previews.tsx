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
    <Card>
      <CardHeader>
        <CardTitle>{post.title}</CardTitle>
      </CardHeader>
      <CardContent>{post.content}</CardContent>
      <CardFooter>
        <Button asChild>
          <Link href={`/posts/${post.id}`}>Read more</Link>
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
