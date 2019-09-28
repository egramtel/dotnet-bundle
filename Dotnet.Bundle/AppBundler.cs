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
            CopyIcon(
                new DirectoryInfo(_builder.OutputDirectory),
                new DirectoryInfo(_builder.ResourcesDirectory));
            
            CopyFiles(
                new DirectoryInfo(_builder.OutputDirectory),
                new DirectoryInfo(_builder.MacosDirectory),
                new DirectoryInfo(_builder.AppDirectory));
        }

        private void CopyFiles(DirectoryInfo source, DirectoryInfo target, DirectoryInfo exclude)
        {
            Directory.CreateDirectory(target.FullName);

            foreach (var fileInfo in source.GetFiles())
            {
                var path = Path.Combine(target.FullName, fileInfo.Name);
                
                _task.Log.LogMessage($"Copying file from: {fileInfo.FullName}");
                _task.Log.LogMessage($"Copying to destination: {path}");
                
                fileInfo.CopyTo(path, true);
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

        private void CopyIcon(DirectoryInfo source, DirectoryInfo target)
        {
            var iconName = Path.GetFileName(_task.CFBundleIconFile);
            _task.Log.LogMessage($"Icon name for bundle is: {iconName}");
            
            if (iconName == null)
            {
                return;
            }
            
            var sourcePath = Path.Combine(source.FullName, iconName);
            _task.Log.LogMessage($"Icon file for bundle is: {sourcePath}");
            
            var targetPath = Path.Combine(target.FullName, iconName);
            _task.Log.LogMessage($"Icon file destination is: {targetPath}");
            
            var sourceFile = new FileInfo(sourcePath);
            
            if (sourceFile.Exists)
            {
                _task.Log.LogMessage($"Copying icon file to destination: {targetPath}");
                sourceFile.CopyTo(targetPath);
            }
        }
    }
}