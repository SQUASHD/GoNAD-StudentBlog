package main

import (
	"bufio"
	"flag"
	"fmt"
	"os"
	"path/filepath"
	"regexp"
	"strings"
)

func main() {
	inputDir := flag.String("in", "", "Directory with C# DTO files")
	outputDir := flag.String("out", "", "Directory to save TypeScript files")
	flag.Parse()

	if *inputDir == "" || *outputDir == "" {
		fmt.Println("Please specify both input and output directories")
		os.Exit(1)
	}

	fmt.Println("Input directory:", *inputDir)
	fmt.Println("Output directory:", *outputDir)

	files, _ := filepath.Glob(filepath.Join(*inputDir, "*.cs"))
	fmt.Println("Found files:", files)
	for _, file := range files {
		fmt.Println("Processing:", file)
		baseName := filepath.Base(file)
		nameWithoutExtension := strings.TrimSuffix(baseName, filepath.Ext(baseName))
		tsFileName := nameWithoutExtension + ".ts"

		outputFilePath := filepath.Join(*outputDir, tsFileName)
		err := os.WriteFile(outputFilePath, []byte(""), 0644)
		if err != nil {
			panic(err)
		}

		convertFile(file, outputFilePath)
	}
}

func convertFile(filePath, outputFilePath string) {
	file, err := os.Open(filePath)
	if err != nil {
		panic(err)
	}
	defer file.Close()

	var typeName string
	var fields []string

	// Assumes DTOs are defined as public record <name>(
	reRecord := regexp.MustCompile(`public record (\w+)\(`)
	// Assumes fields are defined as <attribute> <type> <name>
	reField := regexp.MustCompile(`(?P<Attribute>\[\w*])?\s*(?P<Type>\w+)\s+(?P<FieldName>\w+)`)

	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		line := scanner.Text()

		// typeName empty indicates that the parser has not yet found a type defintion
		if typeName == "" {
			// Check if the line contains a record definition
			if matches := reRecord.FindStringSubmatch(line); matches != nil {
				typeName = matches[1]
				continue
			}
			// else if typeName is not empty, then the parser has found a type definition
		} else if matches := reField.FindStringSubmatch(line); matches != nil {
			result := make(map[string]string)
			for i, name := range reField.SubexpNames() {
				if i != 0 && name != "" { // i != 0 because the entire matched string is also part of the slice.
					result[name] = matches[i]
				}
			}

			fieldType := result["Type"]
			fieldName := result["FieldName"]
			attribute := result["Attribute"]

			tsType, comment := csharpTypeToTypeScript(fieldType)

			var formattedAttribute string

			if attribute != "" {
				formattedAttribute = fmt.Sprintf(" // %s", attribute)
			}

			formattedFieldName := toCamelCase(fieldName)

			fields = append(fields, fmt.Sprintf("  %s: %s;%s%s", formattedFieldName, tsType, comment, formattedAttribute))

			// Assumes that the end of a record is defined as );
		} else if strings.Contains(line, ");") && typeName != "" && len(fields) > 0 {
			// Write to file
			output := "export type " + typeName + " = {\n" + strings.Join(fields, "\n") + "\n}\n"
			f, err := os.OpenFile(outputFilePath, os.O_APPEND|os.O_WRONLY, 0644)
			if err != nil {
				panic(err)
			}
			if _, err = f.WriteString(output); err != nil {
				f.Close()
				panic(err)
			}
			f.Close()

			fmt.Printf("Appended to: %s\n", outputFilePath)
			typeName = ""
			fields = []string{}
		}
	}
}

func csharpTypeToTypeScript(csharpType string) (string, string) {
	comment := ""
	tsType := ""

	switch csharpType {
	case "int":
		tsType = "number"
	case "string":
		tsType = "string"
	case "DateTime":
		tsType = "string"
		comment = " // Represents a DateTime type in string format"
	case "bool":
		tsType = "boolean"
	case "PublicationStatus":
		tsType = "PublicationStatus"
		comment = " // PublicationStatus is an enum, refer to the enum definition for possible values"
	default:
		tsType = "unknown"
	}

	return tsType, comment
}

func toCamelCase(input string) string {
	if len(input) == 0 {
		return ""
	}
	return strings.ToLower(input[:1]) + input[1:]
}
