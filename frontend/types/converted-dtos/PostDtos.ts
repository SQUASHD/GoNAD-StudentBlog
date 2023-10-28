export type PostInputReqDto = {
  title: string;
  content: string;
}
export type PostResDto = {
  id: number;
  userId: number;
  title: string;
  content: string;
  createdAt: string; // Represents a DateTime type in string format
  updatedAt: string; // Represents a DateTime type in string format
}
