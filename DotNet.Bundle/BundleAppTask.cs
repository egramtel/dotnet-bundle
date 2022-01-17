using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Dotnet.Bundle
{
    public interface IBundleAppTask
    {
        string OutDir { get; set; }

        string PublishDir { get; set; }

        string CFBundleName { get; set; }

        string CFBundleDisplayName { get; set; }

        string CFBundleIdentifier { get; set; }

        string CFBundleVersion { get; set; }

        string CFBundlePackageType { get; set; }

        string CFBundleSignature { get; set; }

        string CFBundleExecutable { get; set; }

        string CFBundleIconFile { get; set; }

        string CFBundleShortVersionString { get; set; }

        string NSPrincipalClass { get; set; }

        bool NSHighResolutionCapable { get; set; }

        bool NSRequiresAquaSystemAppearance { get; set; }

        bool? NSRequiresAquaSystemAppearanceNullable { get; set; }

        ITaskItem[] CFBundleURLTypes { get; set; }

        bool Execute();
        void LogMessage(string message);
    }
    public class BundleAppTask : Task, IBundleAppTask
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

        public bool NSRequiresAquaSystemAppearance
        {
            get => NSRequiresAquaSystemAppearanceNullable.Value;
            set => NSRequiresAquaSystemAppearanceNullable = value;
        }

        public bool? NSRequiresAquaSystemAppearanceNullable { get; set; }

        public ITaskItem[] CFBundleURLTypes { get; set; }
        public void LogMessage(string message)
        {
            Log.LogMessage(message);
        }

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
