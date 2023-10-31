import { PostPreview } from "@/components/previews";
import { auth } from "@/lib/auth";
import { typedFetchWithAuth } from "@/lib/fetch";
import { PaginatedResultDto } from "@/types/GenericDtos";
import { PostResDto } from "@/types/converted-dtos/PostDtos";
import { notFound } from "next/navigation";
type PostsSearchParams = {
  page: string;
  size: string;
};

export const revalidate = 0;

export const metadata = {
  title: "Blog Posts",
  description: "The student blog project powered by the GoNAD stack.",
};

export default async function PostsPage({
  searchParams,
}: {
  searchParams: PostsSearchParams;
  }) {
  await auth("/posts");
  const pageQuery = searchParams.page ? `?page=${searchParams.page}` : "";
  const sizeQuery = searchParams.size ? `&size=${searchParams.size}` : "";

  const posts = await typedFetchWithAuth<PaginatedResultDto<PostResDto>>(
    `/posts${pageQuery}${sizeQuery}`
  );

  if ("StatusCode" in posts && posts.StatusCode === 404) notFound();

  if ("items" in posts)
    return (
      <div>
        <h1 className="font-semibold text-4xl text-center py-8">
          Recent posts
        </h1>
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
