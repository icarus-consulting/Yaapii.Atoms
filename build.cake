#tool nuget:?package=GitReleaseManager&version=0.11
#tool nuget:?package=OpenCover&version=4.7.922
#tool nuget:?package=Codecov&version=1.12.3
#addin nuget:?package=Cake.Figlet&version=1.3.1
#addin nuget:?package=Cake.Codecov&version=0.9.1
#addin nuget:?package=Cake.Incubator&version=5.1.0

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

var unitTests               = Directory("./tests");
var blacklistedUnitTests    = new List<string>() { }; 

///////////////////////////////////////////////////////////////////////////////
// CONFIGURATION VARIABLES
///////////////////////////////////////////////////////////////////////////////
var isAppVeyor              = AppVeyor.IsRunningOnAppVeyor;
var isWindows               = IsRunningOnWindows();

// For GitHub release
var owner                   = "icarus-consulting";
var repository              = "Yaapii.Atoms";

// For NuGetFeed
var nuGetSource             = "https://api.nuget.org/v3/index.json";
var appVeyorNuGetFeed       = "https://ci.appveyor.com/nuget/icarus/api/v2/package";

// API key tokens for deployment
var gitHubToken             = "";
var nugetReleaseToken       = "";
var appVeyorFeedToken       = "";
var codeCovToken            = "";

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
// Clean
///////////////////////////////////////////////////////////////////////////////
Task("Clean")
.Does(() =>
{
    Information(Figlet("Clean"));
    
    CleanDirectories(new DirectoryPath[] { buildArtifacts });
    foreach(var module in GetSubDirectories(modules))
    {
        var name = module.GetDirectoryName();
        if(!blacklistedModules.Contains(name))
        {
            CleanDirectories(
                new DirectoryPath[] 
                { 
                    $"{module}/bin",
                    $"{module}/obj",
                }
            );
        }
    }
});

///////////////////////////////////////////////////////////////////////////////
// Restore
///////////////////////////////////////////////////////////////////////////////
Task("Restore")
.Does(() =>
{
    Information(Figlet("Restore"));
    
    NuGetRestore($"./{repository}.sln");
});

///////////////////////////////////////////////////////////////////////////////
// Build
///////////////////////////////////////////////////////////////////////////////
Task("Build")
.IsDependentOn("Version")
.IsDependentOn("Clean")
.IsDependentOn("Restore")
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
    foreach(var test in GetSubDirectories(unitTests))
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
            new FilePath($"{buildArtifacts.Path}/coverage.xml"),
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
    
    Codecov($"{buildArtifacts.Path}/coverage.xml", codeCovToken);
});

///////////////////////////////////////////////////////////////////////////////
// Assert Packages
///////////////////////////////////////////////////////////////////////////////
Task("AssertPackages")
.Does(() => 
{
    Information(Figlet("Assert Packages"));

    foreach (var module in GetSubDirectories(modules))
    {
        var name = module.GetDirectoryName();
        if(!blacklistedModules.Contains(name))
        {
            var project = ParseProject(new FilePath($"{module}/{name}.csproj"), configuration);
            var packageVersion = new Dictionary<string, string>();
            foreach (var package in project.PackageReferences)
            {
                packageVersion.Add(package.Name, package.Version);
            }

            foreach (var package in packageVersion)
            {
                if (package.Key.Contains(".Sources"))
                {
                    var nonSourcesPackage = package.Key.Replace(".Sources", string.Empty);
                    if (packageVersion[nonSourcesPackage] != package.Value)
                    {
                        throw new Exception(
                            $"Reference nuget packages must have equal version in project {name}:{Environment.NewLine}"
                            + $"\t{package.Key} {package.Value} and {nonSourcesPackage} {packageVersion[nonSourcesPackage]}.{Environment.NewLine}"
                            + $"\tUpdate nuget package in the {name}.csproj file.{Environment.NewLine}"
                            + $"\tHint: search for '<PackageReference Include=\"{package.Key}\" Version=\"{package.Value}\" Condition=\"'$(Configuration)' == 'ReleaseSources'\">'."
                        );    
                    }
                }
            }
        }
    }
    Information("Package validation passed.");
});

///////////////////////////////////////////////////////////////////////////////
// NuGet
///////////////////////////////////////////////////////////////////////////////
Task("NuGet")
.IsDependentOn("Version")
.IsDependentOn("Clean")
.IsDependentOn("AssertPackages")
.IsDependentOn("Restore")
.IsDependentOn("Build")
.Does(() =>
{
    Information(Figlet("NuGet"));
    
    var settings = new DotNetCorePackSettings()
    {
        Configuration = configuration,
        OutputDirectory = buildArtifacts,
        NoRestore = true,
        NoBuild = true,
        VersionSuffix = ""
    };
    settings.ArgumentCustomization = args => args.Append("--include-symbols").Append("-p:SymbolPackageFormat=snupkg");
    settings.MSBuildSettings = new DotNetCoreMSBuildSettings().SetVersionPrefix(version);

    var settingsSources = new DotNetCorePackSettings()
    {
        Configuration = "ReleaseSources",
        OutputDirectory = buildArtifacts,
        NoRestore = false,
        NoBuild = false,
        VersionSuffix = ""
    };
    settingsSources.MSBuildSettings = new DotNetCoreMSBuildSettings().SetVersionPrefix(version);

    foreach (var module in GetSubDirectories(modules))
    {
        var name = module.GetDirectoryName();
        if(!blacklistedModules.Contains(name))
        {
            DotNetCorePack(
                module.ToString(),
                settings
            );

            settingsSources.ArgumentCustomization = args => args.Append($"-p:PackageId={name}.Sources").Append("-p:IncludeBuildOutput=false");
            DotNetCorePack(
                module.ToString(),
                settingsSources
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
    nugetReleaseToken = EnvironmentVariable("NUGET_TOKEN");
    appVeyorFeedToken = EnvironmentVariable("APPVEYOR_TOKEN");
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
    var nugets = string.Join(",", GetFiles("./artifacts/*.*nupkg").Select(f => f.FullPath) );
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
// NuGet Feed
///////////////////////////////////////////////////////////////////////////////
Task("NuGetFeed")
.WithCriteria(() => isAppVeyor && BuildSystem.AppVeyor.Environment.Repository.Tag.IsTag)
.IsDependentOn("NuGet")
.IsDependentOn("Credentials")
.Does(() => 
{
    Information(Figlet("NuGet Feed"));
    
    var nugets = GetFiles($"{buildArtifacts.Path}/*.nupkg");
    foreach(var package in nugets)
    {
        if (package.GetFilename().ToString().Contains(".Sources"))
        {
            NuGetPush(
                package,
                new NuGetPushSettings {
                    Source = appVeyorNuGetFeed,
                    ApiKey = appVeyorFeedToken
                }
            );
        }
        else
        {
            NuGetPush(
                package,
                new NuGetPushSettings {
                    Source = nuGetSource,
                    ApiKey = nugetReleaseToken
                }
            );
        }
    }
    var symbols = GetFiles($"{buildArtifacts.Path}/*.snupkg");
    foreach(var symbol in symbols)
    {
        NuGetPush(
            symbol,
            new NuGetPushSettings {
                Source = nuGetSource,
                ApiKey = nugetReleaseToken
            }
        );
    }
});

///////////////////////////////////////////////////////////////////////////////
// Default
///////////////////////////////////////////////////////////////////////////////
Task("Default")
.IsDependentOn("Version")
.IsDependentOn("Clean")
.IsDependentOn("Restore")
.IsDependentOn("Build")
.IsDependentOn("UnitTests")
.IsDependentOn("GenerateCoverage")
.IsDependentOn("Credentials")
.IsDependentOn("UploadCoverage")
.IsDependentOn("AssertPackages")
.IsDependentOn("NuGet")
.IsDependentOn("GitHubRelease")
.IsDependentOn("NuGetFeed");

RunTarget(target);
