export function prettifyDate(date: string): string {
  return new Date(date).toLocaleDateString("en-UK", {
    weekday: "long",
    year: "numeric",
    month: "long",
    day: "numeric",
  });
}

export function nTimeAgo(date: string) {
  const time = new Date(date).getTime();
  const now = new Date().getTime();
  const diff = now - time;
  const seconds = Math.floor(diff / 1000);
  const minutes = Math.floor(seconds / 60);
  const hours = Math.floor(minutes / 60);
  const days = Math.floor(hours / 24);

  if (seconds < 60) {
    if (seconds < 10) return "just now";
    return `${seconds} seconds ago`;
  }
  if (minutes < 60) {
    if (minutes === 1) return "1 minute ago";
    return `${minutes} minutes ago`;
  }
  if (hours < 24) {
    if (hours === 1) return "1 hour ago";
    return `${hours} hours ago`;
  }
  if (days < 7) {
    if (days === 1) return "1 day ago";
    return `${days} days ago`;
  }
  return prettifyDate(date);
}
