type CommentResDto = {
  id: number;
  postId: number;
  userId: number;
  content: string;
  createdAt: string; // Represents a DateTime type in string format
  updatedAt: string; // Represents a DateTime type in string format
}
type CommentInputReqDto = {
  content: string;
}
