import { cn } from "@/lib/utils";
import Link from "next/link";
import Markdown, { defaultUrlTransform } from "react-markdown";
import remarkGfm from "remark-gfm";

type FormattedLinkProps = {
  href: string;
  children: React.ReactNode;
};

export function FormattedLink({ href, children }: FormattedLinkProps) {
  return (
    <Link href={href} className=" text-blue-600 underline">
      {children}
    </Link>
  );
}

type MDWrapperProps = {
  children: React.ReactNode;
  className?: string;
};

export function MarkdownFormatWrapper({ children, className }: MDWrapperProps) {
  return (
    <article
      className={cn(
        "prose prose-p:text-primary prose-headings:text-primary",
        className
      )}
    >
      {children}
    </article>
  );
}

type FormattedMarkdownProps = {
  content: string;
  title: String;
};
export function FormattedMarkdown({ content, title }: FormattedMarkdownProps) {
  return (
    <MarkdownFormatWrapper>
      <h2>{title}</h2>
      <Markdown urlTransform={defaultUrlTransform} remarkPlugins={[remarkGfm]}>
        {content}
      </Markdown>
    </MarkdownFormatWrapper>
  );
}

export function FormattedPageSection({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <section className="max-w-screen-md py-8 w-full mx-auto px-4">
      {children}
    </section>
  );
}
