using System.IO;

namespace Dotnet.Bundle
{
    public class StructureBuilder
    {
        private IBundleAppTask _task;

        public StructureBuilder(IBundleAppTask task)
        {
            _task = task;
        }

        public string OutputDirectory => _task.OutDir;
        
        public string PublishDirectory => _task.PublishDir;

        public string AppDirectory => Path.Combine(Path.Combine(PublishDirectory, _task.CFBundleDisplayName + ".app"));
        
        public string ContentsDirectory => Path.Combine(AppDirectory, "Contents");
        
        public string MacosDirectory => Path.Combine(ContentsDirectory, "MacOS");
        
        public string ResourcesDirectory => Path.Combine(ContentsDirectory, "Resources");

        public void Build()
        {
            _task.LogMessage($"Publish directory is: {PublishDirectory}");
            
            if (Directory.Exists(AppDirectory))
            {
                _task.LogMessage($"Clearing bundle directory");
                Directory.Delete(AppDirectory, true);
            }
            
            _task.LogMessage($"Creating bundle directory: {AppDirectory}");
            Directory.CreateDirectory(AppDirectory);
            
            _task.LogMessage($"Creating contents directory: {ContentsDirectory}");
            Directory.CreateDirectory(ContentsDirectory);
            
            _task.LogMessage($"Creating MacOS directory: {MacosDirectory}");
            Directory.CreateDirectory(MacosDirectory);
            
            _task.LogMessage($"Creating resources directory: {ResourcesDirectory}");
            Directory.CreateDirectory(ResourcesDirectory);
        }
    }
}