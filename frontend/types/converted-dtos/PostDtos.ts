import { PublicationStatus } from "../internal-enums";

export type CreatePostReqDto = {
  title: string;
  content: string;
};
export type UpdatePostReqDto = {
  title: string;
  content: string;
  status: PublicationStatus; // PublicationStatus is an enum, refer to the enum definition for possible values
};
export type PostResDto = {
  id: number;
  userId: number;
  title: string;
  content: string;
  status: PublicationStatus; // PublicationStatus is an enum, refer to the enum definition for possible values
  createdAt: string; // Represents a DateTime type in string format
  updatedAt: string; // Represents a DateTime type in string format
};
