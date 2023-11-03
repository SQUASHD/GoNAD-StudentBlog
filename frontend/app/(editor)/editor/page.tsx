import { redirect } from "next/navigation";

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

export default function Editor() {
  redirect("/dashboard/posts");
}
