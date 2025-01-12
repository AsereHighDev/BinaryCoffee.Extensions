# BinaryCoffee Extension

## Overview
The `BinaryCoffee.Extensions` library provides utility methods to extend and simplify common operations with strings, collections, and `StringBuilder`. It also includes asynchronous support for some operations. The library is modular, grouping functionalities into distinct classes. 📚✨

---

## Modules and Methods

### 1. **StringExtensions**
Extensions for advanced string manipulation. 🧵

#### **Methods**

- **ToCamelCase**
  Converts a string to CamelCase format.
  ```csharp
  string result = "my_variable".ToCamelCase(); // "myVariable"
  ```

- **ToPascalCase**
  Converts a string to PascalCase format.
  ```csharp
  string result = "my-variable".ToPascalCase(); // "MyVariable"
  ```

- **ToSnakeLowerCase**
  Converts a string to snake_case format in lowercase.
  ```csharp
  string result = "MyVariable".ToSnakeLowerCase(); // "my_variable"
  ```

- **ToSnakeUpperCase**
  Converts a string to SNAKE_CASE format in uppercase.
  ```csharp
  string result = "MyVariable".ToSnakeUpperCase(); // "MY_VARIABLE"
  ```

- **MatchesPattern**
  Validates if a string matches a regular expression pattern. 🔍
  ```csharp
  bool isValid = "example@domain.com".MatchesPattern("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$"); // true
  ```

- **StripTags**
  Removes HTML or XML tags from a string. 🧼
  ```csharp
  string result = "<p>Hello World</p>".StripTags(); // "Hello World"
  ```

- **Reverse**
  Reverses the content of a string. 🔄
  ```csharp
  string result = "abcd".Reverse(); // "dcba"
  ```

- **ToSlug**
  Converts a string into a URL-friendly slug. 🌐
  ```csharp
  string result = "This is a Test".ToSlug(); // "this-is-a-test"
  ```

- **CapitalizeWords**
  Capitalizes the first letter of each word in a string. ✨
  ```csharp
  string result = "hello world".CapitalizeWords(); // "Hello World"
  ```

- **NormalizeSpaces**
  Trims and normalizes spaces within a string. 🧹
  ```csharp
  string result = "  Extra   spaces  here  ".NormalizeSpaces(); // "Extra spaces here"
  ```

- **RemoveCharacters**
  Removes specified characters from a string. ❌
  ```csharp
  string result = "Hello123".RemoveCharacters('1', '2', '3'); // "Hello"
  ```

---

### 2. **CollectionExtensions**
Extensions for working with collections. 📦

#### **Methods**

- **Join**
  Joins elements of a collection into a single string with a custom separator and format.
  ```csharp
  string result = new[] { 1, 2, 3 }.Join(x => x.ToString(), ", "); // "1, 2, 3"
  ```

- **AddNested**
  Adds an item to a nested collection within a dictionary. Creates the collection if it doesn’t exist. 🗂️
  ```csharp
  var dict = new Dictionary<string, List<int>>();
  dict.AddNested("key", 42);
  ```

- **TransformAsync**
  Applies an asynchronous transformation to each element in a collection. ⚡
  ```csharp
  var results = await new[] { "a", "b" }.TransformAsync(async str => await Task.FromResult(str.ToUpper()));
  ```

---

### 3. **StringBuilderExtensions**
Extensions for working with `StringBuilder`. 🏗️

#### **Methods**

- **AppendRepeated**
  Appends a value to a `StringBuilder` multiple times. 🔁
  ```csharp
  var builder = new StringBuilder().AppendRepeated("abc", 3); // "abcabcabc"
  ```

---

## Usage Examples

### Converting String Formats
```csharp
string input = "my-variable";
string camelCase = input.ToCamelCase();    // "myVariable"
string pascalCase = input.ToPascalCase();  // "MyVariable"
string snakeLower = input.ToSnakeLowerCase(); // "my_variable"
```

### Validating Strings
```csharp
bool isEmail = "example@domain.com".MatchesPattern("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$"); // true
```

### Cleaning Strings
```csharp
string noTags = "<p>Hello</p>".StripTags(); // "Hello"
string normalized = "  Too    many spaces  ".NormalizeSpaces(); // "Too many spaces"
```

### Working with Collections
```csharp
var numbers = new[] { 1, 2, 3 };
string joined = numbers.Join(n => n.ToString(), ", "); // "1, 2, 3"
```

---

## Testing 🧪

Unit tests have been provided for all methods in the library. These tests ensure:
- Proper handling of edge cases (e.g., `null`, empty strings).
- Validation of expected outputs.

Run tests using a supported testing framework like `xUnit`. For example:
```bash
dotnet test
```

---

## Contributing 🤝

Contributions are welcome! Please submit issues or pull requests on the GitHub repository. Ensure that any new functionality includes comprehensive unit tests and adheres to the library’s style and structure.

---

## License 📜
This library is distributed under the MIT License. See the LICENSE file for details.

---

## Contact 📬
For questions or support, please contact the maintainer at `support@binarycoffee.com`.

