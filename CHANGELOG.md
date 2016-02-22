# Changelog

## 1.1.3 - 2016-02-22
- Fixed bug with multiple resource files
- Rename ValdrType/ValdrMember to ...Attribute
- Fixed some analysis/styleCop recommendations

## 1.1.2 - 2016-02-15
- Separate assembly loading from parser logic
- Nca.Valdr.Core NuGet package to be used for runtime generation of valdr contraints
- Introduced two attributes than can be used in place of DataContract/DataMember
- Strongly named Nca.Valdr to be consumable from assemblies that are strong-name signed

## 1.1.1 - 2016-01-10
- Assembly resolving bug fixed

## 1.1.0 - 2016-01-08
- Moved the CLI to "packages\Nca.Valdr.1.1.0\tools\Nca.Valdr.Console.exe"
- Created an API under Nca.Valdr.dll for the metadata parser

## 1.0.0 - 2016-01-02
- Initial release
