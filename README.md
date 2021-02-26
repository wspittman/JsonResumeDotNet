# JsonResumeDotNet

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/wspittman/JsonResumeDotNet/blob/master/LICENSE)
[![JsonResume version](https://img.shields.io/badge/JsonResume-1.0.0-blue)](https://github.com/jsonresume/resume-schema)

Status: **Under Construction**. Complete, but has yet to be used in a non-trivial way.

JsonResumeDotNet is a C# library for creating, manipulating, parsing, and validating objects using the [JSON Resume](https://jsonresume.org/) schema. The JSON Resume initiative aims to create a JSON standard for resume data.

This library matches JsonResume schema [v1.0.0](https://github.com/jsonresume/resume-schema/releases/tag/v1.0.0).

The current schema definition can be found in the [resume-schema](https://github.com/jsonresume/resume-schema) repository.

## Installation

TBD

## Usage

Add the JsonResume namespace to your project

```C#
using JsonResume;
```

### Parsing

A `Resume` object can be created from a JSON string with the `Resume.FromJson` method.

```C#
string json = "{{ \"basics\": {{ \"name\": \"Richard\" }} }}";
var resume = Resume.FromJson(json);

// "Richard"
Console.WriteLine(resume.Basics.Name);
```

None of the properties are considered required.

```C#
// Empty Resume object
var resume = Resume.FromJson("{{ }}");
```

By default, `Resume.FromJson` will throw an error for parsing failures. This includes, but may not be limited to:
- Incorrectly formatted JSON: `{ "basics": "name": "Richard" }`
- Incorrectly typed property: `{ "awards": [ { "date": "Not a date" } ] }`

```C#
// Throws an exception
var resume = Resume.FromJson("{{{{");
```

Setting the `throwOnError` parameter to `false` will collect errors in the `ParsingErrors` property.

```C#
// No exception
var resume = Resume.FromJson("{{{{", throwOnError: false);

// "2 Errors Found"
Console.WriteLine($"{resume.ParsingErrors.Count} Errors Found");
```

By default, `Resume.FromJson` will NOT throw an error for unrecognized members.

```C#
// No exception
string json = "{{ \"basics\": {{ \"name\": \"Richard\", \"sandwiches\": \"Yes!\" }} }}";
var resume = Resume.FromJson(json);

// "Richard"
Console.WriteLine(resume.Basics.Name);
```

Setting the `errorOnUnknownMember` parameter to `true` will cause unrecognized members to be treated as errors.

```C#
// Exception thrown
string json = "{{ \"basics\": {{ \"name\": \"Richard\", \"sandwiches\": \"Yes!\" }} }}";
var resume = Resume.FromJson(json);
```

### Editing

A `Resume` object can be edited (or built from scratch) by editing its properties.

```C#
// Create a new object 
var resume = new Resume()
{
  Basics = new Basics()
  {
    Name = "Richard"
  }
};

// Edit a property
resume.Basics.Label = "Programmer";

// "Richard, Programmer"
Console.WriteLine(resume.Basics.Name + ", " + resume.Basics.Label);
```

### Serializing

A `Resume` object can be serialized by calling its `ToJson` method.

```C#
string serialized = resume.ToJson();
```

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

[MIT](https://choosealicense.com/licenses/mit/)

## Inspiration

Several other JSON Resume implementers inspired aspects of this libary.
- [SharpResume](https://github.com/aloisdg/SharpResume) (C#)
- [JsonResumeSharp](https://github.com/sinathr/JsonResumeSharp) (C#)
- [JsonResume Validator](https://github.com/eaxdev/Java-JsonResume-Validator) (Java)