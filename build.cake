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
var version                 = "4.1.4";

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

// API key tokens for deployment
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
            NoRestore = true
        };
    settings.ArgumentCustomization = args =>
        args
        .Append($"-p:AssemblyVersion={version}")
        .Append($"-p:FileVersion={version}");

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
// Credentials
///////////////////////////////////////////////////////////////////////////////
Task("Credentials")
.WithCriteria(() => isAppVeyor && BuildSystem.AppVeyor.Environment.Repository.Tag.IsTag)
.Does(() =>
{
    Information(Figlet("Credentials"));
   
    codeCovToken = EnvironmentVariable("CODECOV_TOKEN");
    if (string.IsNullOrEmpty(codeCovToken))
    {
        throw new Exception("Environment variable 'CODECOV_TOKEN' is not set");
    }
});

///////////////////////////////////////////////////////////////////////////////
// Default
///////////////////////////////////////////////////////////////////////////////
Task("Default")
.IsDependentOn("Credentials")
.IsDependentOn("Version")
.IsDependentOn("Clean")
.IsDependentOn("Restore")
.IsDependentOn("Build")
.IsDependentOn("UnitTests")
.IsDependentOn("GenerateCoverage")
.IsDependentOn("UploadCoverage");

RunTarget(target);
