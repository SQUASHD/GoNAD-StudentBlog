import { PaginatedSearchParams } from "@/types";
import { type ClassValue, clsx } from "clsx";
import { twMerge } from "tailwind-merge";

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}

export const debounce = <F extends (...args: Parameters<F>) => ReturnType<F>>(
  func: F,
  waitFor: number
) => {
  let timeout: NodeJS.Timeout;

  const debounced = (...args: Parameters<F>) => {
    clearTimeout(timeout);
    timeout = setTimeout(() => func(...args), waitFor);
  };

  return debounced;
};

export function getSearchQueryString(searchParams: PaginatedSearchParams) {
  const pageQuery = searchParams.page ? `?page=${searchParams.page}` : "";
  const sizeQuery = searchParams.size ? `&size=${searchParams.size}` : "";
  // TODO: Add orderBy
  // const orderByQuery = searchParams.orderBy ? `&orderBy=${searchParams.orderBy}` : "";

  // return `${pageQuery}${sizeQuery}${orderByQuery}`;

  return `${pageQuery}${sizeQuery}`;
}
