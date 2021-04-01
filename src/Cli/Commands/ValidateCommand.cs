using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Text;
using ResumeBuilder.Core.Schema;

namespace ResumeBuilder.Cli.Commands
{
  public class ValidateCommand : Command
  {
    public ValidateCommand()
      : base("validate", "Validates your resume.json against the schema to ensure it complies with the standard.")
    {
      AddArgument(new Argument("file"));

      Handler = CommandHandler.Create<FileInfo, IConsole>(async (file, console) =>
      {
        if (!file.Exists)
          throw new FileNotFoundException("Resume not found", file.FullName);

        await using var stream = file.OpenRead();
        using var reader = new StreamReader(stream, Encoding.UTF8);
        var resumeJson = await reader.ReadToEndAsync();

        var validator = new Validator();
        var result = await validator.ValidateV1(resumeJson);
        if (result.IsValid)
        {
          console.Out.Write($"{file.Name} is valid.");
        }
        else
        {
          console.Out.Write($"{file.Name} is NOT valid:");

          foreach (var message in result.Messages)
          {
            console.Out.Write($"{message}");
          }
        }
      });
    }
  }
}