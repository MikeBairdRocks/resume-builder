using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
// [GitHubActions("nuget", 
//   GitHubActionsImage.UbuntuLatest,
//   AutoGenerate = true,
//   PublishArtifacts = true,
//   OnPushBranches = new[] { MainBranch, ReleaseBranchPrefix + "/*" },
//   CacheKeyFiles = new[] { "src/**/*.csproj" },
//   InvokedTargets = new[] { nameof(Push) },
//   ImportSecrets = new[]{ nameof(NugetApiKey) }
// )]
[GitHubActions("pack", 
  GitHubActionsImage.UbuntuLatest,
  AutoGenerate = true,
  PublishArtifacts = true,
  OnPushBranches = new[] { MainBranch, ReleaseBranchPrefix + "/*" },
  CacheKeyFiles = new[] { "src/**/*.csproj" },
  InvokedTargets = new[] { nameof(Pack) }
)]
class Build : NukeBuild
{
  const string MainBranch = "main";
  const string DevelopBranch = "develop";
  const string ReleaseBranchPrefix = "release";
  
  public static int Main() => Execute<Build>(x => x.Pack);

  [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
  readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

  [Parameter] string NugetApiUrl = "https://api.nuget.org/v3/index.json";
  [Parameter] [Secret] readonly string NugetApiKey;
  
  [Solution] readonly Solution Solution;
  [GitRepository] readonly GitRepository GitRepository;
  [GitVersion(Framework = "net5.0")] readonly GitVersion GitVersion;

  AbsolutePath SourceDirectory => RootDirectory / "src";
  AbsolutePath TestsDirectory => RootDirectory / "tests";
  AbsolutePath OutputDirectory => RootDirectory / "output";

  Target Clean => _ => _
    .Before(Restore)
    .Executes(() =>
    {
      SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
      TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
      EnsureCleanDirectory(OutputDirectory);
    });

  Target Restore => _ => _
    .Executes(() =>
    {
      DotNetRestore(s => s
        .SetProjectFile(Solution));
    });

  Target Pack => _ => _
    .DependsOn(Restore)
    .Produces(OutputDirectory / "*.nupkg")
    .Executes(() =>
    {
      var cli = Solution.GetProject("Cli");

      DotNetPack(s => s
        .SetProject(cli)
        .SetConfiguration(Configuration)
        .SetOutputDirectory(OutputDirectory)
        .SetAssemblyVersion(GitVersion.AssemblySemVer)
        .SetFileVersion(GitVersion.AssemblySemFileVer)
        .SetInformationalVersion(GitVersion.InformationalVersion)
        .SetIncludeSymbols(true)
        .EnableNoRestore());
    });
  
  Target Push => _ => _
    .Consumes(Pack)
    .DependsOn(Pack)
    .Requires(() => NugetApiUrl)
    .Requires(() => NugetApiKey)
    .Executes(() =>
    {
      DotNetNuGetPush(s => s
        .SetTargetPath(OutputDirectory / "**/*.nupkg")
        .SetSource(NugetApiUrl)
        .SetApiKey(NugetApiKey)
        .SetSkipDuplicate(true));
    });
}