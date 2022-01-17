using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.Build.Framework;

namespace Dotnet.Bundle
{
    public class PlistWriter
    {
        private readonly IBundleAppTask _task;
        private readonly StructureBuilder _builder;

        private static readonly string[] ArrayTypeProperties = { "CFBundleURLSchemes" };
        private const char Separator = ';';

        public PlistWriter(IBundleAppTask task, StructureBuilder builder)
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
            
            _task.LogMessage($"Writing property list file: {path}");
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

                //This can be re-factored
                WriteProperty(xmlWriter, nameof(_task.CFBundleName), _task.CFBundleName);
                WriteProperty(xmlWriter, nameof(_task.CFBundleDisplayName), _task.CFBundleDisplayName);
                WriteProperty(xmlWriter, nameof(_task.CFBundleIdentifier), _task.CFBundleIdentifier);
                WriteProperty(xmlWriter, nameof(_task.CFBundleVersion), _task.CFBundleVersion);
                WriteProperty(xmlWriter, nameof(_task.CFBundlePackageType), _task.CFBundlePackageType);
                WriteProperty(xmlWriter, nameof(_task.CFBundleSignature), _task.CFBundleSignature);
                WriteProperty(xmlWriter, nameof(_task.CFBundleExecutable), _task.CFBundleExecutable);
                WriteProperty(xmlWriter, nameof(_task.CFBundleIconFile), Path.GetFileName(_task.CFBundleIconFile));
                WriteProperty(xmlWriter, nameof(_task.CFBundleShortVersionString), _task.CFBundleShortVersionString);
                WriteProperty(xmlWriter, nameof(_task.NSPrincipalClass), _task.NSPrincipalClass);
                WriteProperty(xmlWriter, nameof(_task.NSHighResolutionCapable), _task.NSHighResolutionCapable);

                if (_task.NSRequiresAquaSystemAppearanceNullable.HasValue)
                {
                    WriteProperty(xmlWriter, nameof(_task.NSRequiresAquaSystemAppearance), _task.NSRequiresAquaSystemAppearanceNullable.Value);
                }

                if (_task.CFBundleURLTypes.Length != 0)
                {
                    WriteProperty(xmlWriter, nameof(_task.CFBundleURLTypes), _task.CFBundleURLTypes);
                }
 

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
            }
        }

        private void WriteProperty(XmlWriter xmlWriter, string name, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                xmlWriter.WriteStartElement("key");
                xmlWriter.WriteString(name);
                xmlWriter.WriteEndElement();
                
                xmlWriter.WriteStartElement("string");
                xmlWriter.WriteString(value);
                xmlWriter.WriteEndElement();
            }
        }
        
        private void WriteProperty(XmlWriter xmlWriter, string name, bool value)
        {
            xmlWriter.WriteStartElement("key");
            xmlWriter.WriteString(name);
            xmlWriter.WriteEndElement();

            if (value)
            {
                xmlWriter.WriteStartElement("true");
            }
            else
            {
                xmlWriter.WriteStartElement("false");
            }
            
            xmlWriter.WriteEndElement();
        }

        private void WriteProperty(XmlWriter xmlWriter, string name, string[] values)
        {
            if (values.Length != 0)
            {
                xmlWriter.WriteStartElement("key");
                xmlWriter.WriteString(name);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("array");
                foreach (var value in values)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        xmlWriter.WriteStartElement("string");
                        xmlWriter.WriteString(value);
                        xmlWriter.WriteEndElement();
                    }
                }

                xmlWriter.WriteEndElement();
            }
        }


        private void WriteProperty(XmlWriter xmlWriter, string name, ITaskItem[] values)
        {
            xmlWriter.WriteStartElement("key");
            xmlWriter.WriteString(name);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("array");            

            foreach (var value in values)
            {
                xmlWriter.WriteStartElement("dict");
                var metadataDictionary = value.CloneCustomMetadata();

                foreach (DictionaryEntry entry in metadataDictionary)
                {
                    var dictValue = entry.Value.ToString();
                    var dictKey = entry.Key.ToString();

                    if (dictValue.Contains(Separator.ToString()) || ArrayTypeProperties.Contains(dictKey)) //array
                    {
                        WriteProperty(xmlWriter, dictKey, dictValue.Split(Separator));
                    }
                    else
                    {
                        WriteProperty(xmlWriter, dictKey, dictValue);
                    }
                }

                xmlWriter.WriteEndElement(); //End dict
            }

            xmlWriter.WriteEndElement(); //End outside array
        }
    }
}