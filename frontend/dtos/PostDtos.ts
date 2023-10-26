type PostInput = {
  title: string;
  content: string;
}
type CreatePostDto = {
  userId: number;
  title: string;
  content: string;
}
type UpdatePostDto = {
  userId: number;
  title: string;
  content: string;
}
type PostResDto = {
  id: number;
  userId: number;
  title: string;
  content: string;
  createdAt: string; // Represents a DateTime. Format: ISO string
  updatedAt: string; // Represents a DateTime. Format: ISO string
}
