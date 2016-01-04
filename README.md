# valdr .NET Validation

[![Build status](https://ci.appveyor.com/api/projects/status/v9o6s7bkq04k8hlr?svg=true)](https://ci.appveyor.com/project/ilbertz/valdr-dotnet)
[![NuGet](https://img.shields.io/nuget/v/Nca.Valdr.svg)](https://www.nuget.org/packages/Nca.Valdr)
[![NuGet](https://img.shields.io/nuget/dt/Nca.Valdr.svg)](https://www.nuget.org/packages/Nca.Valdr)
[![License](https://img.shields.io/badge/license-MIT-blue.svg?style=flat)](https://github.com/netceteragroup/valdr-dotnet/blob/master/LICENSE.txt)

.NET plugin for [valdr](https://github.com/netceteragroup/valdr),
an AngularJS model validator.

  - [Offering](#offering)
  - [Installation](#installation)
  - [Dependencies](#dependencies)
  - [Mapping of .NET DataAnnotations attributes to valdr constraints](#mapping-of-net-dataannotations-attributes-to-valdr-constraints)
  - [License](#license)

## Offering

valdr .NET parses C# classes for DataAnnotation attributes and extracts their information into a JavaScript file, which includes the [metadata to be used by valdr](https://github.com/netceteragroup/valdr#constraints-json). This allows to apply the exact same
validation rules on the server and on the AngularJS client.

## Installation

Install the [Nuget package](https://www.nuget.org/packages/Nca.Valdr), which will create the solution folder called .build.
```
PM> Install-Package Nca.Valdr
```

In Visual Studio, right-click your project and under Properties/Build Events add the following Post-build event:
```Batchfile
$(SolutionDir).build\Nca.Valdr.exe -i:$(TargetDir)$(TargetFileName) -o:$(ProjectDir)app\app.valdr.js
```

Nca.Valdr.exe accepts the following parameters:
- ```-i:``` input assembly filename (.dll)
- ```-n:``` namespace filter (default: all)
- ```-o:``` output JavaScript filename
- ```-a:``` AngularJS application name (default: app)

## Dependencies

valdr .NET Validation is dependent on valdr in two ways:

* [JSON structure](https://github.com/netceteragroup/valdr#constraints-json) is defined by valdr
* validators listed in the JSON document have to be either a [supported valdr valdidator](https://github.com/netceteragroup/valdr#built-in-validators) or one of your [custom JavaScript validators](https://github.com/netceteragroup/valdr#adding-custom-validators)

Only C# classes decorated with a [DataContract](https://msdn.microsoft.com/de-de/library/system.runtime.serialization.datacontractattribute(v=vs.110).aspx) attribute will be used the generate the valdr metadata.

## Mapping of .NET DataAnnotations attributes to valdr constraints

The [.NET DataAnnotations](https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations%28v=vs.110%29.aspx) attributes defines the mapping of .NET Validation to valdr constraints.

| .NET DataAnnotations | valdr | Comment |
|-----------------|-------|---------|
| [Required](https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations.requiredattribute%28v=vs.110%29.aspx) | [required](https://github.com/netceteragroup/valdr#required) |  |
| [Range](https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations.rangeattribute%28v=vs.110%29.aspx) | [min](https://github.com/netceteragroup/valdr#min--max) |  |
| [Range](https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations.rangeattribute%28v=vs.110%29.aspx) | [max](https://github.com/netceteragroup/valdr#min--max) |  |
| [StringLength](https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations.stringlengthattribute%28v=vs.110%29.aspx) | [size](https://github.com/netceteragroup/valdr#size) |  |
| | [digits](https://github.com/netceteragroup/valdr#digits) | unsupported |
| [RegularExpression](https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations.regularexpressionattribute%28v=vs.110%29.aspx) | [pattern](https://github.com/netceteragroup/valdr#partern) |  |
| | [future](https://github.com/netceteragroup/valdr#future--past) | unsupported |
| | [past](https://github.com/netceteragroup/valdr#future--past) | unsupported |
| [EmailAddress](https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations.emailaddressattribute%28v=vs.110%29.aspx) |[email](https://github.com/netceteragroup/valdr#email) |  |
| [URL](https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations.urlattribute%28v=vs.110%29.aspx) |[url](https://github.com/netceteragroup/valdr#url) |  |

## License

[MIT](http://opensource.org/licenses/MIT) © Netcetera AG
