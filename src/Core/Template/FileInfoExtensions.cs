using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;

namespace ResumeBuilder.Core.Template
{
  public static class FileInfoExtensions
  {
    public static async Task<string> ReadToEndAsync(this IFileInfo file)
    {
      if(!file.Exists) 
        throw new FileNotFoundException($"File '{file.Name}' not found.", file.Name);
      
      await using var stream = file.CreateReadStream();
      using var reader = new StreamReader(stream);
      
      var output = await reader.ReadToEndAsync();

      return output;
    }
  }
}