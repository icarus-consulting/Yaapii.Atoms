#tool nuget:?package=GitReleaseManager
#tool nuget:?package=OpenCover
#tool nuget:?package=Codecov
#addin "Cake.Figlet"
#addin nuget:?package=Cake.Codecov&version=0.5.0

var target                  = Argument("target", "Default");
var configuration           = "Release";

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////
var buildArtifacts          = Directory("./artifacts");
var deployment              = Directory("./artifacts/deployment");
var version                 = "2.1.3";

///////////////////////////////////////////////////////////////////////////////
// MODULES
///////////////////////////////////////////////////////////////////////////////
var modules                 = Directory("./src");
var blacklistedModules      = new List<string>() { };

var tests                   = Directory("./tests");
var blacklistedUnitTests    = new List<string>() { };

///////////////////////////////////////////////////////////////////////////////
// CONFIGURATION VARIABLES
///////////////////////////////////////////////////////////////////////////////
var isAppVeyor              = AppVeyor.IsRunningOnAppVeyor;
var isWindows               = IsRunningOnWindows();

// For GitHub release
var owner                   = "icarus-consulting";
var repository              = "Yaapii.Atoms";

// For publishing NuGetFeed
var nuGetSource             = "https://api.nuget.org/v3/index.json";

// API key tokens for deployment
var gitHubToken             = "";
var nuGetToken              = "";
var codeCovToken            = "";

///////////////////////////////////////////////////////////////////////////////
// Clean
///////////////////////////////////////////////////////////////////////////////
Task("Clean")
    .Does(() => 
    {
        Information(Figlet("Clean"));
    
        CleanDirectories(new DirectoryPath[] { buildArtifacts });
    });

///////////////////////////////////////////////////////////////////////////////
// Restore
///////////////////////////////////////////////////////////////////////////////
Task("Restore")
    .Does(() =>
    {
        Information(Figlet("Restore"));

        var projects = GetFiles("./**/*.csproj");
        foreach(var project in projects)
        {
            DotNetCoreRestore(project.GetDirectory().FullPath);
        }
    });

///////////////////////////////////////////////////////////////////////////////
// Version
///////////////////////////////////////////////////////////////////////////////
Task("Version")
    .WithCriteria(() => isAppVeyor && BuildSystem.AppVeyor.Environment.Repository.Tag.IsTag)
    .Does(() => 
    {
        Information(Figlet("Version"));
    
        version = BuildSystem.AppVeyor.Environment.Repository.Tag.Name;
        Information($"Set version to '{version}'");
    });

///////////////////////////////////////////////////////////////////////////////
// Build
///////////////////////////////////////////////////////////////////////////////
Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Version")
    .Does(() =>
    {   
        Information(Figlet("Build"));

        var settings = 
            new DotNetCoreBuildSettings()
            {
                Configuration = configuration,
                NoRestore = true,
                MSBuildSettings = new DotNetCoreMSBuildSettings().SetVersionPrefix(version)
            };
        var skipped = new List<string>();
        foreach(var module in GetSubDirectories(modules))
        {
            var name = module.GetDirectoryName();
            if(!blacklistedModules.Contains(name))
            {
                Information($"Building {name}");
            
                DotNetCoreBuild(
                    module.FullPath,
                    settings
                );
            }
            else
            {
                skipped.Add(name);
            }
        }
        if (skipped.Count > 0)
        {
            Warning("The following builds have been skipped:");
            foreach(var name in skipped)
            {
                Warning($"  {name}");
            }
        }
    });

///////////////////////////////////////////////////////////////////////////////
// Unit Tests
///////////////////////////////////////////////////////////////////////////////
Task("UnitTests")
    .IsDependentOn("Build")
    .Does(() =>
    {
        Information(Figlet("Unit Tests"));

        var settings = 
            new DotNetCoreTestSettings()
            {
                Configuration = configuration,
                NoRestore = true
            };
        var skipped = new List<string>();
        foreach(var test in GetSubDirectories(tests))
        {
            var name = test.GetDirectoryName();
            if(blacklistedUnitTests.Contains(name))
            {
                skipped.Add(name);
            }
            else if(!name.StartsWith("TmxTest"))
            {
                Information($"Testing {name}");
                DotNetCoreTest(
                    test.FullPath,
                    settings
                );
            }
        }
        if (skipped.Count > 0)
        {
            Warning("The following tests have been skipped:");
            foreach(var name in skipped)
            {
                Warning($"  {name}");
            }
        }
    });

///////////////////////////////////////////////////////////////////////////////
// Generate Coverage
///////////////////////////////////////////////////////////////////////////////
Task("GenerateCoverage")
    .IsDependentOn("Build")
    .Does(() => 
    {
        Information(Figlet("Generate Coverage"));

        try
        {
            OpenCover(
                tool => 
                {
                    tool.DotNetCoreTest(
                        "./tests/Yaapii.Atoms.Tests/",
                        new DotNetCoreTestSettings
                        {
                             Configuration = configuration
                        }
                    );
                },
                new FilePath("./coverage.xml"),
                new OpenCoverSettings()
                {
                    OldStyle = true
                }.WithFilter("+[Yaapii.Atoms]*")
            );
        }
        catch(Exception ex)
        {
            Information("Error: " + ex.ToString());
        }
    });

///////////////////////////////////////////////////////////////////////////////
// Upload Coverage
///////////////////////////////////////////////////////////////////////////////
Task("UploadCoverage")
    .IsDependentOn("GenerateCoverage")
    .IsDependentOn("Credentials")
    .WithCriteria(() => isAppVeyor)
    .Does(() =>
    {
        Information(Figlet("Upload Coverage"));

        Codecov("coverage.xml", codeCovToken);
    });

///////////////////////////////////////////////////////////////////////////////
// NuGet
///////////////////////////////////////////////////////////////////////////////
Task("NuGet")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Version")
    .Does(() => 
    {
        Information(Figlet("NuGet"));
    
        var settings = new DotNetCorePackSettings()
        {
            Configuration = configuration,
            OutputDirectory = buildArtifacts,
            NoRestore = true,
            VersionSuffix = ""
        };
        settings.ArgumentCustomization = args => args.Append("--include-symbols").Append("-p:SymbolPackageFormat=snupkg");
        settings.MSBuildSettings = new DotNetCoreMSBuildSettings().SetVersionPrefix(version);
        foreach(var module in GetSubDirectories(modules))
        {
            var name = module.GetDirectoryName();
            if(!blacklistedModules.Contains(name))
            {
                Information($"Creating NuGet package for {name}");
            
                DotNetCorePack(
                    module.ToString(),
                    settings
                );
            }
            else
            {
                Warning($"Skipping NuGet package for {name}");
            }
        }
    });

///////////////////////////////////////////////////////////////////////////////
// Credentials
///////////////////////////////////////////////////////////////////////////////
Task("Credentials")
    .WithCriteria(() => isAppVeyor)
    .Does(() =>
    {
        Information(Figlet("Credentials"));

        gitHubToken = EnvironmentVariable("GITHUB_TOKEN");
        nuGetToken = EnvironmentVariable("NUGET_TOKEN");
        codeCovToken = EnvironmentVariable("CODECOV_TOKEN");
    });

///////////////////////////////////////////////////////////////////////////////
// GitHub Release
///////////////////////////////////////////////////////////////////////////////
Task("GitHubRelease")
    .WithCriteria(() => isAppVeyor && BuildSystem.AppVeyor.Environment.Repository.Tag.IsTag)
    .IsDependentOn("Version")
    .IsDependentOn("NuGet")
    .IsDependentOn("Credentials")
    .Does(() => 
    {
        Information(Figlet("GitHub Release"));
    
        GitReleaseManagerCreate(
            gitHubToken,
            owner,
            repository, 
            new GitReleaseManagerCreateSettings {
                Milestone         = version,
                Name              = version,
                Prerelease        = false,
                TargetCommitish   = "master"
            }
        );
          
        var nugets = string.Join(",", GetFiles("./artifacts/*.nupkg").Select(f => f.FullPath) );
        Information($"Release files:{Environment.NewLine}  " + nugets.Replace(",", $"{Environment.NewLine}  "));
        GitReleaseManagerAddAssets(
            gitHubToken,
            owner,
            repository,
            version,
            nugets
        );
        GitReleaseManagerPublish(gitHubToken, owner, repository, version);
    });

///////////////////////////////////////////////////////////////////////////////
// NuGetFeed
///////////////////////////////////////////////////////////////////////////////
Task("NuGetFeed")
    .WithCriteria(() => isAppVeyor && BuildSystem.AppVeyor.Environment.Repository.Tag.IsTag)
    .IsDependentOn("NuGet")
    .IsDependentOn("Credentials")
    .Does(() => 
    {
        Information(Figlet("NuGetFeed"));
    
        var nugets = GetFiles($"{buildArtifacts.Path}/*.nupkg");
        foreach(var package in nugets)
        {
            NuGetPush(
                package,
                new NuGetPushSettings {
                    Source = nuGetSource,
                    ApiKey = nuGetToken
                }
            );
        }
        var symbols = GetFiles($"{buildArtifacts.Path}/*.snupkg");
        foreach(var symbol in symbols)
        {
            NuGetPush(
                symbol,
                new NuGetPushSettings {
                    Source = nuGetSource,
                    ApiKey = nuGetToken
                }
            );
        }
    });

///////////////////////////////////////////////////////////////////////////////
// Default
///////////////////////////////////////////////////////////////////////////////
Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Version")
    .IsDependentOn("Build")
    .IsDependentOn("UnitTests")
    .IsDependentOn("GenerateCoverage")
    .IsDependentOn("UploadCoverage")
    .IsDependentOn("NuGet")
    .IsDependentOn("GitHubRelease")
    .IsDependentOn("NuGetFeed");

RunTarget(target);