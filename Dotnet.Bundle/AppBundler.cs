using System.IO;

namespace Dotnet.Bundle
{
    public class AppBundler
    {
        private readonly BundleAppTask _task;
        private readonly StructureBuilder _builder;

        public AppBundler(BundleAppTask task, StructureBuilder builder)
        {
            _task = task;
            _builder = builder;
        }

        public void Bundle()
        {
            CopyFiles(
                new DirectoryInfo(_builder.OutputDirectory),
                new DirectoryInfo(_builder.MacosDirectory),
                new DirectoryInfo(_builder.AppDirectory));
            
            CopyIconFile(
                new DirectoryInfo(_builder.OutputDirectory),
                new DirectoryInfo(_builder.ResourcesDirectory));
        }

        private void CopyFiles(DirectoryInfo source, DirectoryInfo target, DirectoryInfo exclude)
        {
            Directory.CreateDirectory(target.FullName);

            foreach (var fileInfo in source.GetFiles())
            {
                fileInfo.CopyTo(Path.Combine(target.FullName, fileInfo.Name), true);
            }

            foreach (var sourceSubDir in source.GetDirectories())
            {
                if (sourceSubDir.FullName != exclude.FullName)
                {
                    var targetSubDir = target.CreateSubdirectory(sourceSubDir.Name);
                    CopyFiles(sourceSubDir, targetSubDir, exclude);
                }
            }
        }

        private void CopyIconFile(DirectoryInfo outputDirectory, DirectoryInfo resourcesDirectory)
        {
            var sourceFile = new FileInfo(Path.Combine(outputDirectory.FullName, _task.BundleIconFile));
            if (sourceFile.Exists)
            {
                sourceFile.CopyTo(
                    Path.Combine(resourcesDirectory.FullName, Path.GetFileName(_task.BundleIconFile)));
            }
        }
    }
}