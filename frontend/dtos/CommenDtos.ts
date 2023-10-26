type CommentResDto = {
  id: number;
  postId: number;
  userId: number;
  content: string;
  createdAt: string; // Represents a DateTime. Format: ISO string
  updatedAt: string; // Represents a DateTime. Format: ISO string
}
type CommentInput = {
  content: string;
}
type CreateCommentDto = {
  postId: number;
  userId: number;
  content: string;
}
type UpdateCommentDto = {
  id: number;
  userId: number;
  content: string;
}
