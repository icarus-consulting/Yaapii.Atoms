var target = Argument("target", "Default");
var configuration   = Argument<string>("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////
var buildArtifacts      = Directory("./artifacts/");

// Yaapii Model packages
var yaapiiModelStation          = Directory("./src/Yaapii.Station");
var yaapiiModelKinematic        = Directory("./src/Yaapii.Kinematic");
var yaapiiModelRobotProgram     = Directory("./src/Yaapii.Olp");
var yaapiiModelRobotProgramKuka = Directory("./src/Yaapii.Olp.Kuka");
var yaapiiAtoms                 = Directory("./src/Yaapii.Atoms");
// Infrastructure
var yaapiiInfStationTmx         = Directory("./src/Yaapii.Station.Tmx");
var yaapiiInfRobotProgramTmx    = Directory("./src/Yaapii.Olp.Tmx");

// Tests
var yaapiiTestsModelStation             = Directory("./tests/Yaapii.Station.Tests/Yaapii.Station.Tests.csproj");
var yaapiiTestsModelRobotProgram        = Directory("./tests/Yaapii.Olp.Tests/Yaapii.Olp.Tests.csproj");


var nuGetPackagesWithNetStandard               = new [] 
                                        {
                                            yaapiiModelStation,
                                            yaapiiModelKinematic,
                                            yaapiiModelRobotProgram,
                                            yaapiiModelRobotProgramKuka,
                                            yaapiiInfStationTmx,
                                            yaapiiInfRobotProgramTmx,
                                            yaapiiAtoms
                                        };

var nuGetPackagesWithDotNetFull               = new [] 
                                        {
                                            yaapiiInfStationTmx,
                                            yaapiiInfRobotProgramTmx
                                        };

// Tecnomatix plugins for test purpose
var yaapiiTestsModelStatinTmxPlugin                      = Directory("./tests/Yaapii.Station.Tmx.Tests.Plugin");

var tmxPlugins                  = new[]
                                    {
                                        yaapiiTestsModelStatinTmxPlugin
                                    };




var tests                  = new [] 
                                            { 
                                                yaapiiTestsModelStation,
                                                yaapiiTestsModelRobotProgram 
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

        // build tmx plugins which need full .Net Framework
        settings.Framework = net;
        
        foreach (var project in nuGetPackagesWithDotNetFull)
        {
            DotNetCoreBuild(project,settings);
        }

        // build the plugins an create zip archiv
        foreach (var plugin in tmxPlugins)
        {
            var name = new DirectoryPath(plugin).GetDirectoryName();
            var output = buildArtifacts + Directory(name);
            settings.OutputDirectory = output;

            var zipTarget = new FilePath(output + Directory(name +".zip"));
            DotNetCoreBuild(plugin,settings);
            Zip(output,zipTarget);
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

    foreach (var pack in nuGetPackagesWithDotNetFull)
    {
        DotNetCorePack(pack, settings);
    }
});



Task("Default")
  .IsDependentOn("Build")
  .IsDependentOn("Test")
  .IsDependentOn("Pack");

RunTarget(target);
