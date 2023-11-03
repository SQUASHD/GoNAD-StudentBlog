import { CreateNewPostButton } from "@/components/post-form";
import { Button } from "@/components/ui/button";
import { PostRow } from "@/components/user-items";
import { getUserId } from "@/lib/auth";
import { typedFetchWithAuth } from "@/lib/fetch";
import { getSearchQueryString } from "@/lib/utils";
import { PaginatedSearchParams } from "@/types";
import { PaginatedResultDto } from "@/types/GenericDtos";
import { PostResDto } from "@/types/converted-dtos/PostDtos";
import Link from "next/link";

async function getUserPosts(query: string) {
  const userId = getUserId();
  const endpoint = `/users/${userId}/posts${query}`;
  return typedFetchWithAuth<PaginatedResultDto<PostResDto>>(endpoint);
}

export default async function PostsOverview({
  searchParams,
}: {
  searchParams: PaginatedSearchParams;
}) {
  const queryString = getSearchQueryString(searchParams);
  const posts = await getUserPosts(queryString);
  if ("items" in posts) {
    return (
      <div className="p-4 h-full">
        <CreateNewPostButton type="icon" />
        {posts.totalItems === 0 ? (
          <div className="h-full w-full flex flex-col items-center justify-center">
            <h1 className="text-5xl font-bold tracking-tighter">No posts</h1>
            <p>You haven&apos;t made any posts yet</p>
            <CreateNewPostButton type="text" />
          </div>
        ) : (
          <ul className="flex flex-col">
            {posts?.items.map((post) => (
              <li key={post.id}>
                <PostRow post={post} />
              </li>
            ))}
          </ul>
        )}
      </div>
    );
  }
  return <></>;
}
