import {
  Tooltip,
  TooltipContent,
  TooltipProvider,
  TooltipTrigger,
} from "./ui/tooltip";

type ToolTipProps = {
  children: React.ReactNode;
  text: string;
};

export function QuickToolTip({ children, text }: ToolTipProps) {
  return (
    <TooltipProvider>
      <Tooltip>
        <TooltipContent>{text}</TooltipContent>
        {/* asChild prevents hydration errors */}
        <TooltipTrigger asChild>{children}</TooltipTrigger>
      </Tooltip>
    </TooltipProvider>
  );
}
