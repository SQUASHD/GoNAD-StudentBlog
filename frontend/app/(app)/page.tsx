import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { userIsSignedIn } from "@/lib/auth";
import { typedFetchWithAccessToken } from "@/lib/fetch";
import { PaginatedResultDto } from "@/types/GenericDtos";
import { PostResDto } from "@/types/converted-dtos/PostDtos";
import Link from "next/link";
import { redirect } from "next/navigation";
import { use } from "react";

type PostPreviewProps = {
  post: PostResDto;
};

function PostPreview({ post }: PostPreviewProps) {
  return (
    <Card>
      <CardHeader>
        <CardTitle>{post.title}</CardTitle>
      </CardHeader>
      <CardContent>{post.content}</CardContent>
      <CardFooter>
        <Button asChild>
          <Link href={`/blog/${post.id}`}>Read more</Link>
        </Button>
      </CardFooter>
    </Card>
  );
}

export const metadata = {
  title: "Student Blog Project",
  description: "The student blog project powered by the GoNAD stack.",
};

export default async function HomePage() {
  const signedIn = await userIsSignedIn();

  if (!signedIn) {
    redirect("/landing-page");
  }

  const posts =
    await typedFetchWithAccessToken<PaginatedResultDto<PostResDto>>("/posts");

  if ("StatusCode" in posts) return;

  return (
    <div>
      <h1>Blog</h1>
      <ul className="grid grid-cols-3 space-x-6">
        {posts?.items.map((post) => (
          <li key={post.id}>
            <PostPreview post={post} />
          </li>
        ))}
      </ul>
    </div>
  );
}
