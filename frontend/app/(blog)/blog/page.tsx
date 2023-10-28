import { typedFetchWithAccessToken } from "@/lib/fetch";
import { PaginatedResultDto } from "@/types/GenericDtos";
import { PostResDto } from "@/types/converted-dtos/PostDtos";

export default async function BlogLandingPage() {
  const posts =
    await typedFetchWithAccessToken<PaginatedResultDto<PostResDto>>("/posts");

  console.log(posts);

  return (
    <div>
      <h1>Blog</h1>
      <ul>
        {posts?.items.map((post) => (
          <li key={post.id}>
            <a href={`/blog/${post.id}`}>{post.title}</a>
          </li>
        ))}
      </ul>
    </div>
  );
}
