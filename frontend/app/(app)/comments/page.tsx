import { typedFetchWithAuth } from "@/lib/fetch";
import { PaginatedResultDto } from "@/types/GenericDtos";
import { CommentResDto } from "@/types/converted-dtos/CommenDtos";
import { CommentsCard } from "@/components/previews";

type PostsSearchParams = {
  page: string;
  size: string;
};

export const revalidate = 0;

export const metadata = {
  title: "Student Blog Project | Comments",
  description: "The student blog project powered by the GoNAD stack.",
};

export default async function CommentsPage({
  searchParams,
}: {
  searchParams: PostsSearchParams;
}) {
  const pageQuery = searchParams.page ? `?page=${searchParams.page}` : "";
  const sizeQuery = searchParams.size ? `&size=${searchParams.size}` : "";

  const comments = await typedFetchWithAuth<PaginatedResultDto<CommentResDto>>(
    `/comments${pageQuery}${sizeQuery}`
  );

  if ("items" in comments)
    return (
      <div className="max-w-3xl w-full">
        <h1 className="font-semibold text-4xl text-center py-8">
          Recent comments
        </h1>
        <ul className="grid grid-cols-3 gap-x-6 gap-y-4">
          {comments?.items.map((comment) => (
            <li key={comment.id}>
              <CommentsCard comment={comment} />
            </li>
          ))}
        </ul>
        <div>
          Showing page {comments?.currentPage} of {comments?.totalPages}
        </div>
        <div>Total comments: {comments?.totalItems}</div>
      </div>
    );
}
