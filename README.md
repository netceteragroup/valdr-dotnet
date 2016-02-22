# valdr .NET Validation

[![Build status](https://ci.appveyor.com/api/projects/status/v9o6s7bkq04k8hlr?svg=true)](https://ci.appveyor.com/project/ilbertz/valdr-dotnet)
[![Build Status](https://travis-ci.org/netceteragroup/valdr-dotnet.svg?branch=master)](https://travis-ci.org/netceteragroup/valdr-dotnet)
[![Coverage Status](https://coveralls.io/repos/netceteragroup/valdr-dotnet/badge.svg?branch=master&service=github)](https://coveralls.io/github/netceteragroup/valdr-dotnet?branch=master)

.NET plugin for [valdr](https://github.com/netceteragroup/valdr),
an AngularJS model validator.

  - [Offering](#offering)
  - [Installation](#installation---valdr-net)
  - [Dependencies](#dependencies)
  - [Mapping of .NET DataAnnotations attributes to valdr constraints](#mapping-of-net-dataannotations-attributes-to-valdr-constraints)
  - [License](#license)

## Offering

valdr .NET parses C# classes for DataAnnotation attributes and extracts their information into a JavaScript file, which includes the [metadata to be used by valdr](https://github.com/netceteragroup/valdr#constraints-json). This allows to apply the exact same
validation rules on the server and on the AngularJS client.  valdr .NET core exposes only the parser logic and a few helpful attributes, so that it can be used in your own tooling as needed.  

The biggest difference between the two packages is that valdr .NET does not support contract identification by attributes other than DataContract / DataMember.  valdr .NET core allows use of arbitrary types in place of these.

## Installation - valdr .NET

[![NuGet](https://img.shields.io/nuget/v/Nca.Valdr.svg)](https://www.nuget.org/packages/Nca.Valdr)
[![License](https://img.shields.io/badge/license-MIT-blue.svg?style=flat)](https://github.com/netceteragroup/valdr-dotnet/blob/master/LICENSE.txt)

To install the [Nuget package](https://www.nuget.org/packages/Nca.Valdr), run the following command in the [Package Manager Console](http://docs.nuget.org/consume/package-manager-console):
```
PM> Install-Package Nca.Valdr
```

In Visual Studio, right-click your project and under Properties/Build Events add the following Post-build event:
```Batchfile
$(SolutionDir)packages\Nca.Valdr.1.1.3\tools\Nca.Valdr.Console.exe -i:$(TargetDir)$(TargetFileName) -o:$(ProjectDir)app\app.valdr.js
```

Nca.Valdr.exe accepts the following parameters:
- ```-i:``` input assembly filename (.dll)
- ```-n:``` namespace filter (default: all)
- ```-o:``` output JavaScript filename
- ```-a:``` AngularJS application name (default: "app")
- ```-c:``` Culture (optional, e.g. "en" or "en-US")

At this time, only C# classes decorated with a [DataContract](https://msdn.microsoft.com/en-us/library/system.runtime.serialization.datacontractattribute(v=vs.110).aspx) attribute will be used the generate the valdr metadata.

## Installation - valdr .NET core

[![NuGet](https://img.shields.io/nuget/v/Nca.Valdr.Core.svg)](https://www.nuget.org/packages/Nca.Valdr.Core)
[![License](https://img.shields.io/badge/license-MIT-blue.svg?style=flat)](https://github.com/netceteragroup/valdr-dotnet/blob/master/LICENSE.txt)

To install the [Nuget package](https://www.nuget.org/packages/Nca.Valdr.Core), run the following command in the [Package Manager Console](http://docs.nuget.org/consume/package-manager-console):
```
PM> Install-Package Nca.Valdr.Core
```

This will add the core library to your project, which brings with it the parser and a couple of attributes that can be used to tag your models.  These tools that can be used to generate constraints as needed.  Here is an example that will generate constraints at runtime using the provided constraints and serve through a controller action:

```csharp

    public class ConstraintsController : Controller
    {
        private readonly IParser _constraintParser;

        public LaunchController(IParser constraintParser)
        {
            _constraintParser = constraintParser;
        }

        [Route("/api/Constraints")]
        [ResponseCache(Duration = 600)]
        public IActionResult Index()
        {
            JObject constraints = _constraintParser.Parse(
				//optional culture (for resolving validation messages from resource files)				
				CultureInfo.CurrentCulture, 
				//optional namespace filter - StartsWith search
				null, 
				//type and member name on type used to identify and name constraints
                new ValdrTypeAttributeDescriptor(typeof(ValdrTypeAttribute), nameof(ValdrTypeAttribute.Name)), 
				//type name used to identify data members
				nameof(ValdrMemberAttribute), 
				//assembly(s) to parse for constraint generation
                Assembly.GetAssembly(typeof (MyDTO)) 
			);
            
            return new ObjectResult(constraints);
        }
    }
```

When using the parser directly, it is possible to specify different attributes to use for contract tagging.  The only restriction is that they need to be defined using the "Named Argument" syntax instead of constructor parameters, eg

```csharp

	[ValdrType(Name = "ConstraintForMyDTO")]
	public class MyDTO
	{
		[ValdrMember(Name = "PropertyNameOnClientSide")]
		public string MyProperty { get; set; }
	}
```

## Dependencies

valdr .NET Validation is dependent on valdr in two ways:

* [JSON structure](https://github.com/netceteragroup/valdr#constraints-json) is defined by valdr
* validators listed in the JSON document have to be either a [supported valdr valdidator](https://github.com/netceteragroup/valdr#built-in-validators) or one of your [custom JavaScript validators](https://github.com/netceteragroup/valdr#adding-custom-validators)

To indicate which valdr version a specific valdr .NET version supports there's a simple rule: the first
digit of the valdr .NET version denotes the supported valdr version. Version 1.x will support valdr 1.
This means that valdr .NET 1.x+1 may introduce breaking changes over 1.x because the second version digit
kind-of represents the "major" version.

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
