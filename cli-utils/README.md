# Go CLI Utils

Welcome to the part of the stack that puts the 'Go' in 'GoNAD' – the CLI utils I made to ease development.

## DTO Converter

I changed a DTO on the backend, then realized I had to manually update the types on the frontend. Once? Okay, fine. Twice? Annoying. Thrice? I wrote a tool. It converts the DTOs from my backend (C# Records) to TypeScript types.


### Using the DTO converter

```
TODO: Add instructions for running
```

### Limitations

- The DTOs have to adhere to a certain format
    - They have to be Records
    - The type name (record) must be on a separate line from the fields
- Not all attributes are added as comments

## Log Finder

No more meticulously searching for console.logs that might make it into production.
I've intentionally left out node_modules and .* files from the log finder.

I should probably add this as a step in my CI/CD pipeline... tomorrow.

### Using the Log Finder

```
TODO: Add instructions
```

