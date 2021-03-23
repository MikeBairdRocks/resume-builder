using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Threading.Tasks;
using ResumeBuilder.Cli.Commands;

namespace ResumeBuilder.Cli
{
  class Program
  {
    private static Task<int> Main(string[] args)
    {
      var command = new RootCommand
      {
        Name = "resume"
      };
      
      command.AddCommand(new ExportCommand());
      command.AddCommand(new ValidateCommand());
      
      command.Handler = CommandHandler.Create<IHelpBuilder>(help =>
      {
        help.Write(command);
        return 1;
      });
      
      var builder = new CommandLineBuilder(command);
      builder.UseHelp();
      builder.UseVersionOption();
      builder.UseDebugDirective();
      builder.UseParseErrorReporting();
      builder.CancelOnProcessTermination();
      
      var parser = builder.Build();
      return parser.InvokeAsync(args);
    }
  }
}