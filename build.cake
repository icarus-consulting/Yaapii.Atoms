var target = Argument("target", "Default");
var configuration   = Argument<string>("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////
var buildArtifacts      = Directory("./artifacts/");

// packages
var yaapiiAtoms                 = Directory("./src/Yaapii.Atoms");

// Tests
var yaapiiAtomsTest             = Directory("./tests/Yaapii.Atoms.Tests/Yaapii.Atoms.Tests.csproj");

var nuGetPackagesWithNetStandard               = new [] 
                                        {
                                            yaapiiAtoms
                                        };

var tests                  = new [] 
                                            { 
                                                yaapiiAtomsTest,
                                            };


// configuration variables
var isAppVeyor          = AppVeyor.IsRunningOnAppVeyor;
var isWindows           = IsRunningOnWindows();
var netcore             = "netcoreapp1.1";
var net                 = "net461";
var netstandard         = "netstandard1.4";



///////////////////////////////////////////////////////////////////////////////
// Clean
///////////////////////////////////////////////////////////////////////////////
Task("Clean")
    .Does(() =>
{
    CleanDirectories(new DirectoryPath[] { buildArtifacts });
});


// Restore nuget Packages
Task("Restore")
  .Does(() =>
{

	var projects = GetFiles("./**/*.csproj");

	foreach(var project in projects)
	{
	    DotNetCoreRestore(project.GetDirectory().FullPath);
  }
});

///////////////////////////////////////////////////////////////////////////////
// Build
///////////////////////////////////////////////////////////////////////////////
Task("Build")
  .IsDependentOn("Clean")
  .IsDependentOn("Restore")
  .Does(() =>
{
    var settings = new DotNetCoreBuildSettings 
    {
        Configuration = configuration
    };

    if(isWindows)
    {
        // build net standard packages
        foreach (var project in nuGetPackagesWithNetStandard)
        {
            Information("Building " + project);
            DotNetCoreBuild(project,settings);
        }
    }
});

///////////////////////////////////////////////////////////////////////////////
// Test
///////////////////////////////////////////////////////////////////////////////

Task("Test")
  .IsDependentOn("Build")
  .Does(() => 
{
    var settings = new DotNetCoreTestSettings
    {
        Configuration = configuration
    };

    if (!isWindows)
    {
        Information("Not running on Windows - skipping tests for full .NET Framework");
        settings.Framework = "netcoreapp1.1";
    }

    foreach(var test in tests)
    {
        DotNetCoreTest(test, settings);
    }
    
});

///////////////////////////////////////////////////////////////////////////////
// Pack
///////////////////////////////////////////////////////////////////////////////
Task("Pack")
    .IsDependentOn("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
{
    if (!isWindows)
    {
        Information("Not running on Windows - skipping pack");
        return;
    }

    var settings = new DotNetCorePackSettings
    {
        Configuration = configuration,
        OutputDirectory = buildArtifacts + Directory("packages"),
        ArgumentCustomization = args => args.Append("--include-symbols")
    };

    // add build suffix for CI builds
    if(isAppVeyor)
    {
        settings.VersionSuffix = "build" + AppVeyor.Environment.Build.Number.ToString().PadLeft(5,'0');
    }

    foreach (var pack in nuGetPackagesWithNetStandard)
    {
        DotNetCorePack(pack, settings);
    }
});

Task("Default")
  .IsDependentOn("Build")
  .IsDependentOn("Test")
  .IsDependentOn("Pack");

RunTarget(target);
