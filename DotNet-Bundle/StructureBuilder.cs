using System.IO;

namespace Dotnet.Bundle
{
    public class StructureBuilder
    {
        private BundleAppTask _task;

        public StructureBuilder(BundleAppTask task)
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
            _task.Log.LogMessage($"Publish directory is: {PublishDirectory}");
            
            if (Directory.Exists(AppDirectory))
            {
                _task.Log.LogMessage($"Clearing bundle directory");
                Directory.Delete(AppDirectory, true);
            }
            
            _task.Log.LogMessage($"Creating bundle directory: {AppDirectory}");
            Directory.CreateDirectory(AppDirectory);
            
            _task.Log.LogMessage($"Creating contents directory: {ContentsDirectory}");
            Directory.CreateDirectory(ContentsDirectory);
            
            _task.Log.LogMessage($"Creating MacOS directory: {MacosDirectory}");
            Directory.CreateDirectory(MacosDirectory);
            
            _task.Log.LogMessage($"Creating resources directory: {ResourcesDirectory}");
            Directory.CreateDirectory(ResourcesDirectory);
        }
    }
}