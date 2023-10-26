package main

import (
	"bufio"
	"flag"
	"fmt"
	"os"
	"path/filepath"
	"strings"
)

func main() {
	// Use a flag for input directory
	root := flag.String("dir", "", "directory to scan for console.log")
	flag.Parse()

	if *root == "" {
		fmt.Println("Please specify the directory to scan")
		os.Exit(1)
	}

	err := filepath.Walk(*root, func(path string, info os.FileInfo, err error) error {
		if err != nil {
			return err
		}

		if info.IsDir() {
			// If the directory is "node_modules" or starts with ".", skip it.
			if info.Name() == "node_modules" || strings.HasPrefix(info.Name(), ".") {
				return filepath.SkipDir
			}
			// It's a directory we don't want to skip; continue the walk.
			return nil
		}

		if strings.HasPrefix(info.Name(), ".") {
			return nil
		}

		if strings.HasSuffix(info.Name(), ".js") ||
			strings.HasSuffix(info.Name(), ".ts") ||
			strings.HasSuffix(info.Name(), ".tsx") ||
			strings.HasSuffix(info.Name(), ".jsx") {
			scanFileForConsoleLog(path)
		}

		return nil
	})

	if err != nil {
		fmt.Printf("error walking the path %v: %v\n", *root, err)
	}
}

func scanFileForConsoleLog(path string) {
	file, err := os.Open(path)
	if err != nil {
		fmt.Printf("error opening the file %v: %v\n", path, err)
		return
	}
	defer file.Close()

	scanner := bufio.NewScanner(file)
	lineNumber := 1
	for scanner.Scan() {
		if strings.Contains(scanner.Text(), "console.log") {
			fmt.Printf("Found console.log at %s:%d\n", path, lineNumber)
		}
		lineNumber++
	}

	if err := scanner.Err(); err != nil {
		fmt.Printf("error scanning the file %v: %v\n", path, err)
	}
}
