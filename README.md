# dotnet-bundle

The command-line interface (CLI) tools for bundling .NET Core projects into MacOS applications (.app)

### Installation

Install MSBuild target via NuGet package: ```Dotnet.Bundle```

[![NuGet](https://img.shields.io/nuget/v/Dotnet.Bundle.svg)](https://www.nuget.org/packages/Dotnet.Bundle/)

```
<PackageReference Include="Dotnet.Bundle" Version="0.9.5" />
```

Install .NET Core CLI tool via NuGet package: ```dotnet-bundle```

[![NuGet](https://img.shields.io/nuget/v/dotnet-bundle.svg)](https://www.nuget.org/packages/dotnet-bundle/)

```
<DotNetCliToolReference Include="dotnet-bundle" Version="0.9.5" />
```

### Using the tool

```
dotnet bundle [-c|--configuration] [-f|--framework] [-r|--runtime]
```

### Properties

Define properties to override default bundle values

```
<PropertyGroup>
    <CFBundleName></CFBundleName>
    <CFBundleDisplayName></CFBundleDisplayName>
    <CFBundleIdentifier></CFBundleIdentifier>
    <CFBundleVersion></CFBundleVersion>
    <CFBundlePackageType></CFBundlePackageType>
    <CFBundleSignature></CFBundleSignature>
    <CFBundleExecutable></CFBundleExecutable>
    <CFBundleIconFile></CFBundleIconFile>
    <NSPrincipalClass></NSPrincipalClass>
    <NSHighResolutionCapable></NSHighResolutionCapable>
</PropertyGroup>
```

More info: https://developer.apple.com/library/archive/documentation/CoreFoundation/Conceptual/CFBundles/BundleTypes/BundleTypes.html 
