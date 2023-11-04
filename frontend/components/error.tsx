import { cn } from "@/lib/utils";

type ErrorMessageProps =
  | {
      error: string;
      type: "form";
    }
  | {
      error: string;
      type: "page";
    };
export function ErrorMessage({ error, type }: ErrorMessageProps) {
  return (
    <div
      className={cn(
        "bg-red-100 border flex items-center justify-center border-red-400 text-red-700 px-4 py-3 rounded relative",
        type === "form" && "mt-2",
        type === "page" && "mt-4",
      )}
    >
      <span className="block sm:inline">{error}</span>
    </div>
  );
}
