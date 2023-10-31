"use client";
import Markdown, { defaultUrlTransform } from "react-markdown";
import remarkGfm from "remark-gfm";
import {
  useState,
  useRef,
  ChangeEvent,
  useCallback,
  KeyboardEvent,
} from "react";
import { Textarea } from "@/components/ui/textarea";
import useBeforeUnload from "@/components/save-prompt";
import { FormattedMarkdown } from "@/components/formatted";

const defaultMD = `# Markdown Sample

## Table of Contents
1. [Headers](#headers)
2. [Emphasis](#emphasis)
3. [Lists](#lists)
4. [Links](#links)
5. [Images](#images)
6. [Code and Syntax Highlighting](#code-and-syntax-highlighting)
7. [Tables](#tables)
8. [Blockquotes](#blockquotes)
9. [Horizontal Line](#horizontal-line)

## Headers

# H1
## H2
### H3
#### H4
##### H5
###### H6

## Emphasis

*Italic text* or _Italic text_

**Bold text** or __Bold text__

~~Strikethrough~~

## Lists

### Unordered List
- Item 1
- Item 2
  - Subitem 2.1
  - Subitem 2.2

### Ordered List
1. First item
2. Second item
3. Third item

## Links

[Google](https://www.google.com)

[Link with title](https://www.google.com "Google's Homepage")

## Images

![Alt text](https://www.example.com/image.jpg "Optional title")

## Code and Syntax Highlighting

Inline \`code\` has \`back-ticks around\` it.

\`\`\`javascript
var s = "JavaScript syntax highlighting";
alert(s);
\`\`\`
  
\`\`\`python
s = "Python syntax highlighting"
  print(s)
\`\`\`

## Tables

| Header 1 | Header 2 |
|----------|----------|
| Cell 1   | Cell 2   |
| Cell 3   | Cell 4   |

## Blockquotes

> This is a blockquote.

## Horizontal Line

---

`;

export default function EditorHomePage() {
  const [textareaVal, setTextareaVal] = useState<string>(defaultMD);
  const textareaRef = useRef<HTMLTextAreaElement>(null);

  const onKeyDown = useCallback(
    (event: KeyboardEvent<HTMLTextAreaElement>) => {
      if (event.key === "Tab") {
        event.preventDefault();
        event.stopPropagation();

        const target = event.target as HTMLTextAreaElement;
        const { selectionStart, selectionEnd } = target;
        const updatedMd =
          textareaVal.substring(0, selectionStart) +
          "\t" +
          textareaVal.substring(selectionEnd);

        setTextareaVal(updatedMd);

        setTimeout(() => {
          if (textareaRef.current) {
            textareaRef.current.selectionStart =
              textareaRef.current.selectionEnd = selectionStart + 1;
          }
        }, 0);
      }
    },
    [textareaVal]
  );

  useBeforeUnload();

  return (
    <div className="mx-auto max-w-7xl w-full max-h-full py-8 h-full flex flex-col items-center">
      <div className="flex flex-col px-8 py-4">
        <h1 className="text-4xl font-bold">Editor</h1>
        <p className="text-lg leading-tight">
          This is the editor page. You can write markdown here and see it
          rendered on the right. The markdown is saved to the database when you
          leave the page.
        </p>
      </div>
      <div className="flex flex-grow gap-4 w-full px-8 overflow-y-scroll">
        <div className="flex h-full gap-4 w-full">
          <div className="max-h-full w-1/2 overflow-y-scroll">
            <Textarea
              ref={textareaRef}
              className="h-full overflow-scroll"
              value={textareaVal}
              name="input"
              onKeyDown={onKeyDown}
              onChange={(event) => setTextareaVal(event.target.value)}
            />
          </div>
          <div className="max-h-full w-1/2">
            <FormattedMarkdown markdown={textareaVal} />
          </div>
        </div>
      </div>
    </div>
  );
}
