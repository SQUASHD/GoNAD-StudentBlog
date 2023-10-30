type ErrorLevel = "critical" | "warning" | "info";

type LogEntry = {
  message: string;
  additionalInfo?: any;
  timestamp?: string;
};

// TODO: Add a logger service to send logs to a server
export default function logError(entry: LogEntry, level: ErrorLevel) {
  entry.timestamp = new Date().toISOString();

  switch (level) {
    case "critical":
      console.error(
        `[CRITICAL] [${entry.timestamp}]`,
        entry.message,
        entry.additionalInfo
      );
      break;
    case "warning":
      console.warn(
        `[WARNING] [${entry.timestamp}]`,
        entry.message,
        entry.additionalInfo
      );
      break;
    case "info":
      console.info(
        `[INFO] [${entry.timestamp}]`,
        entry.message,
        entry.additionalInfo
      );
      break;
    default:
      console.log(
        `[UNKNOWN] [${entry.timestamp}]`,
        entry.message,
        entry.additionalInfo
      );
      break;
  }
}
