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

type FormattedMarkdownProps = {
  markdown: string;
};

export function FormattedMarkdown({ markdown }: FormattedMarkdownProps) {
  return (
    <article className="prose h-full rounded-md border p-4 overflow-auto">
      <Markdown urlTransform={defaultUrlTransform} remarkPlugins={[remarkGfm]}>
        {markdown}
      </Markdown>
    </article>
  );
}
