# dotnet-bundle

Command-line interface tools for bundling .NET Core projects into MacOS applications (.app)

### Installation

Install MSBuild task via NuGet package: `Dotnet.Bundle`

[![NuGet](https://img.shields.io/nuget/v/Dotnet.Bundle.svg)](https://www.nuget.org/packages/Dotnet.Bundle/)

```
<PackageReference Include="Dotnet.Bundle" Version="*" />
```

### Development
* `cd TestBundle`
* run `dotnet msbuild -t:BundleApp -p:RuntimeIdentifier=osx-x64`
* verify `Build/Debug/net5.0/osx-x64/publish` contents

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
    <CFBundlePackageType>APPL</CFBundlePackageType>
    <CFBundleSignature>????</CFBundleSignature>
    <CFBundleExecutable>AppName</CFBundleExecutable>
    <CFBundleIconFile>AppName.icns</CFBundleIconFile> <!-- Will be copied from output directory -->
    <NSPrincipalClass>NSApplication</NSPrincipalClass>
    <NSHighResolutionCapable>true</NSHighResolutionCapable>

    <!-- Optional -->
    <NSRequiresAquaSystemAppearance>true</NSRequiresAquaSystemAppearance>
</PropertyGroup>

<ItemGroup>
    <!-- Optional URLTypes.Check TestBundle.csproj for a working example. -->
    <CFBundleURLTypes Include="dummy"> <!-- The name of this file is irrelevant, it's a MSBuild requirement.-->
        <CFBundleURLName>TestApp URL</CFBundleURLName>
        <CFBundleURLSchemes>testappurl;testappurl://</CFBundleURLSchemes> <!-- Note the ";" separator-->
    </CFBundleURLTypes>
    <CFBundleURLTypes Include="dummy">
        <CFBundleURLName>TestApp URL2</CFBundleURLName>
        <CFBundleURLSchemes>test://</CFBundleURLSchemes>
    </CFBundleURLTypes>
</ItemGroup>
```

More info: https://developer.apple.com/library/archive/documentation/CoreFoundation/Conceptual/CFBundles/BundleTypes/BundleTypes.html
