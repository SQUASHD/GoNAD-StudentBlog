"use client";
import {
  type KeyboardEvent,
  MutableRefObject,
  useCallback,
  useEffect,
  useRef,
  useState,
  Fragment,
} from "react";
import { Textarea } from "@/components/ui/textarea";
import { FormattedMarkdown } from "@/components/formatted";
import { z } from "zod";
import { useForm } from "react-hook-form";
import { updatePost } from "@/app/_actions/post";
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "./ui/form";
import { Button } from "./ui/button";
import { useToast } from "./ui/use-toast";
import { Icons } from "./icons";
import { Input } from "./ui/input";
import { zodResolver } from "@hookform/resolvers/zod";
import useBeforeUnload from "./save-prompt";
import { PostResDto } from "@/types/converted-dtos/PostDtos";
import { DropdownMenu } from "./ui/dropdown-menu";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "./ui/select";
import Link from "next/link";
import { AlertDialog } from "@radix-ui/react-alert-dialog";
import {
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
} from "./ui/alert-dialog";
import { useRouter } from "next/navigation";

/**
 * This hook allows the user to use tab to indent or unindent text in the textarea.
 * It returns a callback that should be passed to the onKeyDown event handler
 */

function useTabbedSpaces(
  textareaVal: string,
  setTextareaVal: (value: string) => void,
  textareaRef: MutableRefObject<HTMLTextAreaElement | null>,
) {
  return useCallback(
    (event: KeyboardEvent<HTMLTextAreaElement>) => {
      // Tab keydown handler
      if (event.key === "Tab") {
        const tabSize = 2; // May be configurable in the future
        event.preventDefault();

        const target = event.target as HTMLTextAreaElement;
        const { selectionStart, selectionEnd } = target;

        let start = selectionStart;
        while (start > 0 && textareaVal[start - 1] !== "\n") {
          start--;
        }

        let end = selectionEnd;
        while (end < textareaVal.length && textareaVal[end] !== "\n") {
          end++;
        }

        let startAdjustment = 0;
        let endAdjustment = 0;

        // TODO: Prevent newlines if they are the the only character on the last line
        const modifiedText = textareaVal
          .substring(start, end)
          .split("\n")
          .map((line, index) => {
            if (!event.shiftKey) {
              /* 
              Tab key press: Add two spaces at the beginning of each line
              The start always moves forward by two spaces, while the end moves 2 * (number of lines)
               */
              if (index === 0) startAdjustment += tabSize;
              endAdjustment += tabSize;
              return "  " + line;
            } else {
              /*
              Shift + Tab key press: Remove up to two spaces from the beginning of each line
              Sometimes the line will have less than two spaces, so we need to keep track of how many spaces we removed
              */
              let spacesRemoved = 0;
              while (line.startsWith(" ") && spacesRemoved < tabSize) {
                line = line.substring(1);
                spacesRemoved++;
              }
              if (index === 0) startAdjustment -= spacesRemoved;
              endAdjustment -= spacesRemoved;
              return line;
            }
          })
          .join("\n");

        const updatedText =
          textareaVal.substring(0, start) + // text before selection
          modifiedText +
          textareaVal.substring(end); // text after selection

        setTextareaVal(updatedText);

        // Need to wait for the textarea to update before setting the selection
        setTimeout(() => {
          if (textareaRef.current) {
            textareaRef.current.selectionStart =
              selectionStart + startAdjustment;
            textareaRef.current.selectionEnd = selectionEnd + endAdjustment;
          }
        }, 0);
      }
    },
    [textareaVal, setTextareaVal, textareaRef],
  );
}

export const updatePostSchema = z.object({
  postId: z.string(),
  title: z
    .string()
    .min(3, "Title must be at least 3 characters")
    .max(60, "Title must be less than 60 characters"),
  content: z
    .string()
    .min(3, "Content must be at least 3 characters")
    .max(10000, "Content must be less than 10000 characters"),
  status: z.enum(["Draft", "Published"]),
});

type EditorProps = {
  post: PostResDto;
};

export default function Editor({ post }: EditorProps) {
  const { toast } = useToast();
  const [title, setTitle] = useState<string>(post.title);
  const [textareaVal, setTextareaVal] = useState<string>(post.content);
  const [loading, setLoading] = useState<boolean>(false);

  const router = useRouter();

  const textareaRef = useRef<HTMLTextAreaElement | null>(
    null,
  ) as MutableRefObject<HTMLTextAreaElement | null>;

  async function submitUpdate(data: z.infer<typeof updatePostSchema>) {
    console.log(data);
    try {
      setLoading(true);
      const res = await updatePost(data);
      if ("StatusCode" in res) {
        toast({
          title: `Error ${res.StatusCode}`,
          description: `${res.Message}`,
          variant: "destructive",
        });
      } else {
        toast({
          title: "Post updated!",
          description: "Your post has been updated.",
        });
      }
    } catch (err) {
      if (err instanceof Error) {
        toast({
          title: "Error",
          description: `${err.message}`,
          variant: "destructive",
        });
      }
    } finally {
      setLoading(false);
    }
  }

  const updatePostForm = useForm<z.infer<typeof updatePostSchema>>({
    resolver: zodResolver(updatePostSchema),
    // Set default values for the form based on GET request
    defaultValues: {
      postId: `${post.id}`,
      title: post.title,
      content: post.content,
      status: post.status,
    },
  });

  // Because the editor is actually a form, the errors should be destructured from the formState
  // This way it can be presented to the user in a toast
  // TODO: make a custom hook for this
  const {
    formState: { errors },
  } = updatePostForm;

  useEffect(() => {
    for (const error of Object.values(errors)) {
      if (error) {
        toast({
          title: "Validation Error",
          description: error.message || "You have a validation error",
          variant: "destructive",
        });
      }
    }
  }, [errors, toast]);

  // useBeforeUnload();

  const handleKeyDown = useTabbedSpaces(
    textareaVal,
    setTextareaVal,
    textareaRef,
  );

  return (
    <Fragment>
      <Button
        onClick={() => router.back()}
        className=" w-8 p-1 h-8 flex items-center justify-center absolute top-2 left-2"
      >
        <Icons.arrowRight className="rotate-180 w-4 h-4" />
      </Button>
      <Form {...updatePostForm}>
        <form
          onSubmit={updatePostForm.handleSubmit(submitUpdate)}
          className="h-full"
        >
          <div className="mx-auto max-w-7xl w-full max-h-full py-8 h-full flex flex-col items-center">
            <div className="flex justify-between w-full  px-8 py-4">
              <div className="flex flex-col">
                <h1 className="text-4xl font-bold">Editor</h1>
                <p className="text-lg leading-tight max-w-xl">
                  This is the editor page. You can write markdown here and see
                  it rendered on the right. Remember to save your work!
                </p>
              </div>
              <div className="flex gap-2">
                <FormField
                  control={updatePostForm.control}
                  name="status"
                  render={({ field }) => (
                    <Fragment>
                      <FormLabel className="sr-only">
                        Publication Status
                      </FormLabel>
                      <Select
                        onValueChange={field.onChange}
                        defaultValue={field.value}
                      >
                        <FormControl>
                          <SelectTrigger className="w-32">
                            <SelectValue placeholder="" />
                          </SelectTrigger>
                        </FormControl>
                        <SelectContent>
                          <SelectItem
                            value="Draft"
                            disabled={post.status === "Published"}
                          >
                            Draft
                          </SelectItem>
                          <SelectItem value="Published">Published</SelectItem>
                        </SelectContent>
                      </Select>
                      <FormMessage />
                    </Fragment>
                  )}
                />
                {loading ? (
                  <Button disabled={!loading} className="w-32">
                    {<Icons.spinner className="animate-spin" />}
                  </Button>
                ) : (
                  <Button type="submit" className="w-32">
                    Update Post
                  </Button>
                )}
              </div>
            </div>
            <div className="flex flex-grow gap-4 w-full px-8 overflow-y-scroll">
              <div className="flex h-full gap-4 w-full">
                <div className="w-1/2">
                  <FormField
                    control={updatePostForm.control}
                    name="title"
                    render={({ field }) => (
                      <FormItem>
                        <FormControl>
                          <Input
                            {...field}
                            id="title"
                            className="w-full h-12 px-4 text-2xl font-bold border-b-0 rounded-b-none ring-0 focus:ring-0 focus-visible:ring-0"
                            placeholder={"Your post title"}
                            value={title}
                            ref={field.ref}
                            onChange={(e) => {
                              setTitle(e.target.value);
                              field.onChange(e);
                            }}
                            onBlur={field.onBlur}
                          />
                        </FormControl>
                      </FormItem>
                    )}
                  />
                  <FormField
                    control={updatePostForm.control}
                    name="content"
                    render={({ field }) => (
                      <FormControl>
                        <Textarea
                          ref={(e) => {
                            textareaRef.current = e;
                            field.ref(e);
                          }}
                          id="content"
                          className="h-full rounded-t-none border-t-0 overflow-scroll ring-0 focus-visible:ring-0 whitespace-nowrap resize-none"
                          value={textareaVal}
                          name="content"
                          onKeyDown={handleKeyDown}
                          onChange={(event) => {
                            setTextareaVal(event.target.value);
                            field.onChange(event);
                          }}
                          onBlur={field.onBlur}
                        />
                      </FormControl>
                    )}
                  />
                </div>

                <div className="w-1/2">
                  <FormattedMarkdown title={title} content={textareaVal} />
                </div>
              </div>
            </div>
          </div>
        </form>
      </Form>
    </Fragment>
  );
}
