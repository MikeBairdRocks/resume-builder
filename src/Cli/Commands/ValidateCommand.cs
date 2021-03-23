using System.CommandLine;
using System.IO;

namespace ResumeBuilder.Cli.Commands
{
  public class ValidateCommand : Command
  {
    public ValidateCommand() : base("validate", "Validates your resume.json against the schema to ensure it complies with the standard.")
    {
      AddOption(FileOption);
    }
    
    private static Option FileOption =>
      new Option(new[] {"-f", "--file"}, "Path to a resume.json file")
      {
        Argument = new Argument<FileInfo>(() => new FileInfo(Defaults.ResumePath))
        {
          Name = "filepath",
          Arity = ArgumentArity.ZeroOrOne
        },
        IsRequired = true
      };
  }
}