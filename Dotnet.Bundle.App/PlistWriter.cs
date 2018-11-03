using System;
using System.IO;
using System.Xml;

namespace Dotnet.Bundle.App
{
    public class PlistWriter
    {
        private readonly BundleAppTask _task;
        private readonly StructureBuilder _builder;

        public PlistWriter(BundleAppTask task, StructureBuilder builder)
        {
            _task = task;
            _builder = builder;
        }

        public void Write()
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
                NewLineOnAttributes = false
            };

            var path = Path.Combine(_builder.ContentsDirectory, "Info.plist");
            
            using (var xmlWriter = XmlWriter.Create(path, settings))
            {   
                xmlWriter.WriteStartDocument();
                
                xmlWriter.WriteRaw(Environment.NewLine);
                xmlWriter.WriteRaw(
                    "<!DOCTYPE plist PUBLIC \"-//Apple//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd\">");
                xmlWriter.WriteRaw(Environment.NewLine);
                
                xmlWriter.WriteStartElement("plist");
                xmlWriter.WriteAttributeString("version", "1.0");
                xmlWriter.WriteStartElement("dict");
                
                WriteProperty(xmlWriter, nameof(_task.BundleName), _task.BundleName);
                WriteProperty(xmlWriter, nameof(_task.BundleDisplayName), _task.BundleDisplayName);
                WriteProperty(xmlWriter, nameof(_task.BundleIdentifier), _task.BundleIdentifier);
                WriteProperty(xmlWriter, nameof(_task.BundleVersion), _task.BundleVersion);
                WriteProperty(xmlWriter, nameof(_task.BundlePackageType), _task.BundlePackageType);
                WriteProperty(xmlWriter, nameof(_task.BundleSignature), _task.BundleSignature);
                WriteProperty(xmlWriter, nameof(_task.BundleExecutable), _task.BundleExecutable);
                WriteProperty(xmlWriter, nameof(_task.BundleIconFile), Path.GetFileName(_task.BundleIconFile));
                
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
            }
        }

        private void WriteProperty(XmlWriter xmlWriter, string name, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                xmlWriter.WriteStartElement("key");
                xmlWriter.WriteString("CF" + name);
                xmlWriter.WriteEndElement();
                
                xmlWriter.WriteStartElement("string");
                xmlWriter.WriteString(value);
                xmlWriter.WriteEndElement();
            }
        }
    }
}