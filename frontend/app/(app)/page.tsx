import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { auth } from "@/lib/auth/auth-utils";
import { typedFetchWithAuth } from "@/lib/fetch";

import { PaginatedResultDto } from "@/types/GenericDtos";
import { CommentResDto } from "@/types/converted-dtos/CommenDtos";
import { PostResDto } from "@/types/converted-dtos/PostDtos";
import { UserResDto } from "@/types/converted-dtos/UserDtos";
import Link from "next/link";
import { Fragment } from "react";
import { H1 } from "@/components/typography";
import { CommentsCard, PostPreview, UsersCard } from "@/components/previews";

export const revalidate = 0;

export const metadata = {
  title: "Student Blog Project",
  description: "The student blog project powered by the GoNAD stack.",
};

export default async function HomePage() {
  await auth("/");

  const postsReq = typedFetchWithAuth<PaginatedResultDto<PostResDto>>("/posts");

  const commentsReq =
    typedFetchWithAuth<PaginatedResultDto<CommentResDto>>("/comments");

  const usersReq = typedFetchWithAuth<PaginatedResultDto<UserResDto>>("/users");

  let [posts, comments, users] = await Promise.all([
    postsReq,
    commentsReq,
    usersReq,
  ]);

  posts = posts as PaginatedResultDto<PostResDto>;
  comments = comments as PaginatedResultDto<CommentResDto>;
  users = users as PaginatedResultDto<UserResDto>;

  return (
    <Fragment>
      <div className="flex flex-col gap-8">
        <H1>Recent Posts</H1>
        {posts?.items.length === 0 && <p>No posts yet.</p>}
        <ul className="grid grid-cols-3 gap-x-6 gap-y-3">
          {posts?.items.map((post) => (
            <li key={post.id}>
              <PostPreview post={post} />
            </li>
          ))}
        </ul>
        <H1>Recent Comments</H1>
        {comments?.items.length === 0 && <p>No comments yet.</p>}
        <ul className="grid grid-cols-3 gap-x-6 gap-y-3">
          {comments?.items.map((comment) => (
            <li key={comment.id}>
              <CommentsCard comment={comment} />
            </li>
          ))}
        </ul>

        <H1>Recent Users</H1>
        {users?.items.length === 0 && <p>No users yet.</p>}
        <ul className="grid grid-cols-3 gap-x-6 gap-y-3">
          {users?.items.map((user) => (
            <li key={user.id}>
              <UsersCard user={user} />
            </li>
          ))}
        </ul>
      </div>
    </Fragment>
  );
}
