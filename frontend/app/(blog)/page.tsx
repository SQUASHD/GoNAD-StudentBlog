import { auth } from "@/lib/auth/auth-utils";
import { typedFetchWithAuth } from "@/lib/fetch";
import { PaginatedResultDto } from "@/types/GenericDtos";
import { CommentResDto } from "@/types/converted-dtos/CommenDtos";
import { PostResDto } from "@/types/converted-dtos/PostDtos";
import { UserResDto } from "@/types/converted-dtos/UserDtos";

import { H2 } from "@/components/typography";
import { PostPreview } from "@/components/previews";

export const revalidate = 0;

export default async function HomePage() {
  await auth("/");
  let posts = await typedFetchWithAuth<PaginatedResultDto<PostResDto>>(
    "/posts?size=10&orderBy=desc",
  );

  posts = posts as PaginatedResultDto<PostResDto>;
  return (
    <div className="flex flex-col h-full w-full">
      <div className="flex flex-col gap-8 w-full pb-8 flex-grow">
        <H2>Recent Posts</H2>
        {posts?.items.length === 0 && <p>No posts yet.</p>}
        <ul className="flex flex-col gap-4 w-full">
          {posts?.items.map((post) => (
            <li key={post.id}>
              <PostPreview post={post} />
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
}
