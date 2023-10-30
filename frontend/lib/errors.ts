export type ApiErrorResponse = {
  StatusCode: number;
  Message: string;
};

export class ApiError extends Error {
  constructor(public StatusCode: number, public Message: string) {
    super(Message);
  }
}