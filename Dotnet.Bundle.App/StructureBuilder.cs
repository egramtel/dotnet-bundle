using System.IO;

namespace Dotnet.Bundle.App
{
    public class StructureBuilder
    {
        private BundleAppTask _task;

        public StructureBuilder(BundleAppTask task)
        {
            _task = task;
        }

        public string OutputDirectory => Path.Combine(_task.PublishDir);

        public string AppDirectory => Path.Combine(Path.Combine(OutputDirectory, _task.BundleDisplayName + ".app"));
        
        public string ContentsDirectory => Path.Combine(AppDirectory, "Contents");
        
        public string MacosDirectory => Path.Combine(ContentsDirectory, "MacOS");
        
        public string ResourcesDirectory => Path.Combine(ContentsDirectory, "Resources");

        public void Build()
        {
            if (Directory.Exists(AppDirectory))
            {
                Directory.Delete(AppDirectory, true);
            }
            
            Directory.CreateDirectory(AppDirectory);
            Directory.CreateDirectory(ContentsDirectory);
            Directory.CreateDirectory(MacosDirectory);
            Directory.CreateDirectory(ResourcesDirectory);
        }
    }
}