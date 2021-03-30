using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Help;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ResumeBuilder.Cli.Commands;
using ResumeBuilder.Core.Template;
using ResumeBuilder.Core.Template.Fluid;

namespace ResumeBuilder.Cli
{
  class Program
  {
    private static async Task Main(string[] args) =>
      await BuildCommandLine()
        .UseHost(_ => Host.CreateDefaultBuilder(args), host =>
        {
          host
            .ConfigureServices((context, services) =>
            {
              var rootPath = context.HostingEnvironment.ContentRootPath;
              context.HostingEnvironment.ContentRootFileProvider = new PhysicalFileProvider(rootPath);
              
              var templateRootPath = Path.Combine(rootPath, "Templates");
              services.AddSingleton<IFileProvider>(new PhysicalFileProvider(templateRootPath));
              services.AddSingleton<ITemplateEngine, FluidTemplateEngine>();
            })
            .ConfigureLogging((context, builder) =>
            {
              builder.ClearProviders();
              builder.AddConsole();
              builder
                .AddFilter("System", LogLevel.None)
                .AddFilter("Microsoft", LogLevel.None);
            });
        })
        .UseDefaults()
        .Build()
        .InvokeAsync(args);

    private static CommandLineBuilder BuildCommandLine()
    {
      var command = new RootCommand
      {
        Name = "resume"
      };

      command.AddCommand(new ExportCommand());
      command.AddCommand(new ValidateCommand());
      command.Handler = CommandHandler.Create<IHelpBuilder>(help => { help.Write(command); });

      var builder = new CommandLineBuilder(command)
        .UseHelp()
        .UseVersionOption()
        .UseDebugDirective()
        .UseParseErrorReporting()
        .CancelOnProcessTermination();

      return builder;
    }
  }
}