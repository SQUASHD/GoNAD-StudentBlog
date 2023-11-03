import { FormattedPageSection } from "@/components/formatted";
import { PostPreview } from "@/components/previews";
import { Button } from "@/components/ui/button";
import { auth } from "@/lib/auth";
import { typedFetchWithAuth } from "@/lib/fetch";
import { getSearchQueryString } from "@/lib/utils";
import { PaginatedSearchParams } from "@/types";
import { PaginatedResultDto } from "@/types/GenericDtos";
import { PostResDto } from "@/types/converted-dtos/PostDtos";
import Link from "next/link";

import { notFound } from "next/navigation";
import { Fragment } from "react";

export const revalidate = 0;

export const metadata = {
  title: "Blog Posts",
  description: "The student blog project powered by the GoNAD stack.",
};

export default async function PostsPage({
  searchParams,
}: {
  searchParams: PaginatedSearchParams;
}) {
  await auth("/posts");

  const searchParamsQuery = getSearchQueryString(searchParams);

  const posts = await typedFetchWithAuth<PaginatedResultDto<PostResDto>>(
    `/posts${searchParamsQuery}`
  );

  if ("StatusCode" in posts && posts.StatusCode === 404) notFound();

  if ("items" in posts)
    return (
      <Fragment>
        <FormattedPageSection>
          <h1 className="font-semibold text-4xl text-center py-8">
            Recent posts
          </h1>
          <ul className="grid space-y-6">
            {posts?.items.map((post) => (
              <li key={post.id}>
                <PostPreview post={post} />
              </li>
            ))}
          </ul>
        </FormattedPageSection>
        <FormattedPageSection>
          {posts?.currentPage < posts?.totalPages && (
            <Button>
              <Link href={`/posts?page=${posts?.currentPage + 1}`}>
                Next page
              </Link>
            </Button>
          )}
        </FormattedPageSection>
      </Fragment>
    );
}
