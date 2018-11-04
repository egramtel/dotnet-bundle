# dotnet-bundle

Command-line interface tools for bundling .NET Core projects into MacOS applications (.app)

### Installation

Install MSBuild target via NuGet package: ```Dotnet.Bundle```

[![NuGet](https://img.shields.io/nuget/v/Dotnet.Bundle.svg)](https://www.nuget.org/packages/Dotnet.Bundle/)

```
<PackageReference Include="Dotnet.Bundle" Version="0.9.6" />
```

Install .NET Core CLI tool via NuGet package: ```dotnet-bundle```

[![NuGet](https://img.shields.io/nuget/v/dotnet-bundle.svg)](https://www.nuget.org/packages/dotnet-bundle/)

```
<DotNetCliToolReference Include="dotnet-bundle" Version="0.9.6" />
```

### Using the tool

```
dotnet bundle [-c|--configuration] [-f|--framework] [-r|--runtime] [-o|--output]
```

### Properties

Define properties to override default bundle values

```
<PropertyGroup>
    <CFBundleName>AppName</CFBundleName>
    <CFBundleDisplayName>App Name</CFBundleDisplayName>
    <CFBundleIdentifier>com.example</CFBundleIdentifier>
    <CFBundleVersion>1.0.0</CFBundleVersion>
    <CFBundlePackageType>AAPL</CFBundlePackageType>
    <CFBundleSignature>????</CFBundleSignature>
    <CFBundleExecutable>AppName</CFBundleExecutable>
    <CFBundleIconFile>AppName.icns</CFBundleIconFile>
    <NSPrincipalClass>NSApplication</NSPrincipalClass>
    <NSHighResolutionCapable>true</NSHighResolutionCapable>
</PropertyGroup>
```

More info: https://developer.apple.com/library/archive/documentation/CoreFoundation/Conceptual/CFBundles/BundleTypes/BundleTypes.html 
