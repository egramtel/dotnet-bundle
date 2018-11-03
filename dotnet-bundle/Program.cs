using System;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.CommandLineUtils;

namespace Dotnet.Bundle
{
    class Program
    {
        static int Main(string[] args)
        {
            var application = new CommandLineApplication();
            
            var runtime = application.Option(
                "-r |--runtime <runtime>",
                "Publishes the application for a given runtime.",
                CommandOptionType.SingleValue);

            var framework = application.Option(
                "-f | --framework <framework>",
                "Publishes the application for the specified target framework.",
                CommandOptionType.SingleValue);

            var configuration = application.Option(
                "-c | --configuration <configuration>",
                "Defines the build configuration. The default value is Debug.",
                CommandOptionType.SingleValue);

            var kind = application.Option(
                "-k | --kind <kind>",
                "Defines bundle kind. Possible values are App or Dmg",
                CommandOptionType.SingleValue);

            application.OnExecute(() =>
            {
                if (!framework.HasValue())
                {
                    Console.WriteLine("Target framework is not specified.");
                    return -1;
                }

                if (!runtime.HasValue())
                {
                    Console.WriteLine("Target runtime is not specified.");
                    return -1;
                }

                if (!kind.HasValue())
                {
                    Console.WriteLine("Bundle kind is not specified.");
                    return -1;
                }
                else if (kind.Value() != "App" || kind.Value() != "Dmg")
                {
                    Console.WriteLine("Invalid bundle kind is specified.");
                    return -1;
                }

                var command = new StringBuilder();
                command.Append($"msbuild /t:Bundle{kind.Value()} ");
                command.Append($"/p:RuntimeIdentifier={runtime.Value()} ");
                command.Append($"/p:TargetFramework={framework.Value()} ");

                if (configuration.HasValue())
                {
                    command.Append($"/p:Configuration={configuration.Value()} ");
                }

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "dotnet",
                        Arguments = command.ToString()
                    }
                };

                process.Start();
                process.WaitForExit();

                return process.ExitCode;
            });

            return application.Execute(args);
        }
    }
}