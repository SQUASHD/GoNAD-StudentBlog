"use client";

import { useEffect } from "react";

/**
 * This hook adds a listener to the window to prompt the user to save their work
 * if they attempt to close the window or navigate away from the page.
 */

export default function useBeforeUnload() {
  useEffect(() => {
    const unloadCallback = (event: BeforeUnloadEvent) => {
      event.preventDefault();
      event.returnValue = "";
      return "";
    };

    window.addEventListener("beforeunload", unloadCallback);
    return () => window.removeEventListener("beforeunload", unloadCallback);
  }, []);
}
