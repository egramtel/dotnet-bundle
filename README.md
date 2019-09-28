# dotnet-bundle

Command-line interface tools for bundling .NET Core projects into MacOS applications (.app)

### Installation

Install MSBuild task via NuGet package: ```Dotnet.Bundle```

[![NuGet](https://img.shields.io/nuget/v/Dotnet.Bundle.svg)](https://www.nuget.org/packages/Dotnet.Bundle/)

```
<PackageReference Include="Dotnet.Bundle" Version="*" />
```

### Using the tool

```
dotnet msbuild -t:BundleApp -p:RuntimeIdentifier=osx-x64 [-p: ...]
```

### Properties

Define properties to override default bundle values

```
<PropertyGroup>
    <CFBundleName>AppName</CFBundleName> <!-- Also defines .app file name -->
    <CFBundleDisplayName>App Name</CFBundleDisplayName>
    <CFBundleIdentifier>com.example</CFBundleIdentifier>
    <CFBundleVersion>1.0.0</CFBundleVersion>
    <CFBundlePackageType>AAPL</CFBundlePackageType>
    <CFBundleSignature>????</CFBundleSignature>
    <CFBundleExecutable>AppName</CFBundleExecutable>
    <CFBundleIconFile>AppName.icns</CFBundleIconFile> <!-- Will be copied from output directory -->
    <NSPrincipalClass>NSApplication</NSPrincipalClass>
    <NSHighResolutionCapable>true</NSHighResolutionCapable>
</PropertyGroup>
```

More info: https://developer.apple.com/library/archive/documentation/CoreFoundation/Conceptual/CFBundles/BundleTypes/BundleTypes.html 
