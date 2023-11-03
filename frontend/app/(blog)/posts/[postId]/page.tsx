import CommentForm from "@/components/comments-form";
import {
  FormattedMarkdown,
  FormattedPageSection,
} from "@/components/formatted";
import { auth } from "@/lib/auth";
import { typedFetchWithAuth } from "@/lib/fetch";
import { CommentResDto } from "@/types/converted-dtos/CommenDtos";
import { PostResDto } from "@/types/converted-dtos/PostDtos";
import { Metadata } from "next";
import { notFound } from "next/navigation";
import { Fragment } from "react";

type Props = {
  params: { postId: string };
};

export async function generateMetadata({ params }: Props): Promise<Metadata> {
  const post = (await getPost(params.postId)) as PostResDto;

  return {
    title: post.title,
  };
}

async function getPost(id: string) {
  const post = await typedFetchWithAuth<PostResDto>(`/posts/${id}`);
  return post;
}

async function getComments(id: string) {
  const comments = await typedFetchWithAuth<Array<CommentResDto>>(
    `/posts/${id}/comments`
  );
  console.log(comments);
  return comments;
}

export default async function Page({ params }: Props) {
  const postId = params.postId ? params.postId : "1";
  await auth(`/posts/${postId}`);

  let postReq =  getPost(postId);
  let commentsReq = getComments(postId);
  
  let [post, comments] = await Promise.all([postReq, commentsReq]);

  if ("StatusCode" in post && post.StatusCode === 404) notFound();

  post = post as PostResDto;
  return (
    <Fragment>
      <FormattedPageSection>
        <FormattedMarkdown title={post.title} content={post.content} />
      </FormattedPageSection>

      <FormattedPageSection>
        <div className="flex flex-col items-center w-full">
          <CommentForm postId={postId} />
          <ul className="flex flex-col w-full">
            {comments instanceof Array && comments.length > 0
              ? comments?.map((comment) => (
                  <li key={comment.id}>
                    <div className="w-full flex gap-2">
                      <div>
                        <div className="w-8 aspect-square rounded-full bg-primary-foreground" />
                      </div>
                      <div>
                        <span>{comment.content}</span>
                      </div>
                    </div>
                  </li>
                ))
              : null}
          </ul>
        </div>
      </FormattedPageSection>
    </Fragment>
  );
}
