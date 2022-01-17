using System;
using System.IO;
using Dotnet.Bundle;
using Moq;
using Tests.Helper;
using Xunit;

namespace Tests
{

    public class PlistWriterShould
    {
        private StructureBuilder _builder { get; set; }
        private IBundleAppTask _task { get; set; }
        public PlistWriterShould()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory());

            var taskMock = Mock.Of<IBundleAppTask>(m =>
                m.OutDir == path &&
                m.PublishDir == path &&
                m.CFBundleDisplayName == "MyApp"
            );

            Mock.Get(taskMock).Setup(s => s.LogMessage(It.IsAny<string>()))
                .Callback((string message) =>
                {
                    Console.WriteLine(message);
                });


            // taskMock.Setup(x => x.LogMessage(It.IsAny<string>()))
            //     .Callback((string message) =>
            //     {
            //         Console.WriteLine(message);
            //     });
            // _task = new BundleAppTask();

            _task = taskMock;
            // _task.OutDir = path;
            // _task.PublishDir = "test";
            _builder = new StructureBuilder(_task);
            _builder.Build();
        }
        [Fact]
        public void TestWrite()
        {
            var plistWriter = new PlistWriter(_task, _builder);
            plistWriter.Write();

            var plistPath = Path.Combine(_builder.ContentsDirectory, "Info.plist");
            Assert.True(File.Exists(plistPath), "Info.plist was not written");

            var result = File.ReadAllText(plistPath);

            var testData = "TestWrite";
            var classPath = $"{typeof(PlistWriter).Name.ToString()}/";
            var expectedResult = DataLoader.LoadFileText(classPath, "TestWrite");

            if (result != expectedResult)
            {
                var dataDir = $"{Directory.GetCurrentDirectory()}{DataLoader.rootPath}{classPath}{testData}.txt";

                File.WriteAllText(dataDir, result);
            }

            Assert.Equal(expectedResult, result);
        }
    }


}