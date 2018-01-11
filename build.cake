#tool nuget:?package=GitReleaseManager
#tool nuget:?package=OpenCover
#tool nuget:?package=xunit.runner.console
#tool nuget:?package=Codecov
#addin nuget:?package=Cake.Codecov

var target = Argument("target", "Default");
var configuration   = Argument<string>("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////

// we define where the build artifacts should be places
// this is relative to the project root folder
var buildArtifacts      = new DirectoryPath("./artifacts/");
var framework     = "netstandard2.0";
var testFramework = "netcoreapp2.0";
var project = new DirectoryPath("./src/Yaapii.Atoms/Yaapii.Atoms.csproj");

var owner = "icarus-consulting";
var repository = "Yaapii.Atoms";

var username = "";
var password = "";
var codecovToken = "";

var isAppVeyor          = AppVeyor.IsRunningOnAppVeyor;

var version = "0.10.0";


///////////////////////////////////////////////////////////////////////////////
// CLEAN
///////////////////////////////////////////////////////////////////////////////
Task("Clean")
  .Does(() => 
{
  // clean the artifacts folder to prevent old builds be present
  // https://cakebuild.net/dsl/directory-operations/
  CleanDirectories(new DirectoryPath[] { buildArtifacts });
});

///////////////////////////////////////////////////////////////////////////////
// RESTORE
///////////////////////////////////////////////////////////////////////////////
Task("Restore")
  .Does(() =>
{
  // collect all csproj files recusive from the root directory
  // and run a niget restore
	var projects = GetFiles("./**/*.csproj");

	foreach(var project in projects)
	{
	    DotNetCoreRestore(project.FullPath);
  }
});

///////////////////////////////////////////////////////////////////////////////
// Build
///////////////////////////////////////////////////////////////////////////////
Task("Build")
  .IsDependentOn("Clean") // we can define Task`s which a dependet on other task like this
  .IsDependentOn("Restore")
  .Does(() =>
{	
	//main = netstandard2.0, tests = netcoreapp2.0
	var projects = GetFiles("./src/**/*.csproj");	//main project(s)
	var testProjects = GetFiles("./tests/**/*.csproj"); //test project(s)

	foreach(var project in projects)
	{
		DotNetCoreBuild(project.ToString(), new DotNetCoreBuildSettings() {
		  Framework = framework,
		  Configuration = configuration
		});
	}

	foreach(var project in testProjects)
	{
		DotNetCoreBuild(project.ToString(), new DotNetCoreBuildSettings() {
		  Framework = testFramework,
		  Configuration = configuration
		});
	}
});

///////////////////////////////////////////////////////////////////////////////
// Test
///////////////////////////////////////////////////////////////////////////////
Task("Test")
  .IsDependentOn("Build")
  .Does(() =>
{
    var projectFiles = GetFiles("./tests/**/*.csproj");
	//var projectFiles = GetFiles("./tests/Yaapii.Atoms.Tests/bin/Debug/netcoreapp2.0/*.tests.dll");
    
	foreach(var file in projectFiles)
    {
	Information("### Discovering Tests in " + file.FullPath);
        DotNetCoreTest(file.FullPath);
		//XUnit2(file.FullPath);
    }
});

///////////////////////////////////////////////////////////////////////////////
// Code Coverage
///////////////////////////////////////////////////////////////////////////////
Task("Generate-Coverage")
.IsDependentOn("Build")
.Does(() => 
{
	try
	{
		OpenCover(
			tool => 
			{
				tool.DotNetCoreTest("./tests/Yaapii.Atoms.Tests/",
				new DotNetCoreTestSettings
				{
					 Configuration = "Release"
				});
			},
			new FilePath("./coverage.xml"),
			new OpenCoverSettings()
			{
				OldStyle = true
			}
			.WithFilter("+[Yaapii.Atoms]*")
		);
	}
	catch(Exception ex)
	{
		Information("Error: " + ex.ToString());
	}
});

Task("Upload-Coverage")
.IsDependentOn("Generate-Coverage")
.IsDependentOn("GetCredentials")
.WithCriteria(() => isAppVeyor)
.Does(() =>
{
    Codecov("coverage.xml", codecovToken);
});

///////////////////////////////////////////////////////////////////////////////
// Packaging
///////////////////////////////////////////////////////////////////////////////
Task("Pack")
  .IsDependentOn("Build")
  .Does(() => 
{
  
	var settings = new DotNetCorePackSettings()
    {
        Configuration = configuration,
        OutputDirectory = buildArtifacts,
	  	VersionSuffix = ""
    };
   
	settings.ArgumentCustomization = args => args.Append("--include-symbols");

   if (isAppVeyor)
   {

       var tag = BuildSystem.AppVeyor.Environment.Repository.Tag;
       if(!tag.IsTag) 
       {
			settings.VersionSuffix = "build" + AppVeyor.Environment.Build.Number.ToString().PadLeft(5,'0');
         
       } 
	   else 
	   {     
			settings.MSBuildSettings = new DotNetCoreMSBuildSettings().SetVersionPrefix(tag.Name);
       }
   }
	
	DotNetCorePack(
		project.ToString(),
		settings
    );
});

///////////////////////////////////////////////////////////////////////////////
// Version
///////////////////////////////////////////////////////////////////////////////
Task("Version")
  .WithCriteria(() => isAppVeyor && BuildSystem.AppVeyor.Environment.Repository.Tag.IsTag)
  .Does(() => 
{
    version = BuildSystem.AppVeyor.Environment.Repository.Tag.Name;
});

///////////////////////////////////////////////////////////////////////////////
// Release
///////////////////////////////////////////////////////////////////////////////
Task("GetCredentials")
    .Does(() =>
{
    username = EnvironmentVariable("GITHUB_USERNAME");
    password = EnvironmentVariable("GITHUB_PASSWORD");
	codecovToken = EnvironmentVariable("CODECOV_TOKEN");
});

Task("Release")
  .WithCriteria(() => isAppVeyor && BuildSystem.AppVeyor.Environment.Repository.Tag.IsTag)
  .IsDependentOn("Version")
  .IsDependentOn("Pack")
  .IsDependentOn("GetCredentials")
  .Does(() => {
     GitReleaseManagerCreate(username, password, owner, repository, new GitReleaseManagerCreateSettings {
            Milestone         = version,
            Name              = version,
            Prerelease        = false,
            TargetCommitish   = "master"
    });
          
var nugetFiles = string.Join(";", GetFiles("./artifacts/**/*.nupkg").Select(f => f.FullPath) );
Information("Nuget artifacts: " + nugetFiles);

GitReleaseManagerAddAssets(
	username,
	password,
	owner,
	repository,
	version,
	nugetFiles
	);
});

Task("Default")
  .IsDependentOn("Build")
  .IsDependentOn("Test")
  .IsDependentOn("Generate-Coverage")
  .IsDependentOn("Upload-Coverage")
  .IsDependentOn("Pack")
  .IsDependentOn("Release")
  .Does(() =>
{ });

RunTarget(target);
