using System;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Dotnet.Bundle
{
    public class BundleAppTask : Task
    {
        [Required]
        public string PublishDir { get; set; }
        
        [Required]
        public string BundleName { get; set; }

        [Required]
        public string BundleDisplayName { get; set; }

        [Required]
        public string BundleIdentifier { get; set; }

        [Required]
        public string BundleVersion { get; set; }

        [Required]
        public string BundlePackageType { get; set; }

        [Required]
        public string BundleSignature { get; set; }

        [Required]
        public string BundleExecutable { get; set; }

        [Required]
        public string BundleIconFile { get; set; }
        
        [Required]
        public string BundlePrincipalClass { get; set; }
        
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
