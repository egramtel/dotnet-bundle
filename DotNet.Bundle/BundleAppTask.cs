using System;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Dotnet.Bundle
{
    public class BundleAppTask : Task
    {
        [Required]
        public string OutDir { get; set; }
        
        [Required]
        public string PublishDir { get; set; }
        
        [Required]
        public string CFBundleName { get; set; }

        [Required]
        public string CFBundleDisplayName { get; set; }

        [Required]
        public string CFBundleIdentifier { get; set; }

        [Required]
        public string CFBundleVersion { get; set; }

        [Required]
        public string CFBundlePackageType { get; set; }

        [Required]
        public string CFBundleSignature { get; set; }

        [Required]
        public string CFBundleExecutable { get; set; }

        [Required]
        public string CFBundleIconFile { get; set; }

        [Required]
        public string CFBundleShortVersionString { get; set; } 

        [Required]
        public string NSPrincipalClass { get; set; }
        
        [Required]
        public bool NSHighResolutionCapable { get; set; }

        public override bool Execute()
        {
            var builder = new StructureBuilder(this);
            builder.Build();
            
            var bundler = new AppBundler(this, builder);
            bundler.Bundle();
            
            var plistWriter = new PlistWriter(this, builder);
            plistWriter.Write();

            return true;
        }
    }
}
