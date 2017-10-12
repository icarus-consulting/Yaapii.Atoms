#tool nuget:?package=GitReleaseManager

var target = Argument("target", "Default");
var configuration   = Argument<string>("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////

// we define where the build artifacts should be places
// this is relative to the project root folder
var buildArtifacts      = new DirectoryPath("./artifacts/");
var framework     = "netstandard1.4";
var project = new DirectoryPath("./src/Yaapii.Atoms/Yaapii.Atoms.csproj");

var owner = "icarus-consulting";
var repository = "Yaapii.Atoms";

var username = "";
var password = "";

var isAppVeyor          = AppVeyor.IsRunningOnAppVeyor;


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

    DotNetCoreBuild(project.ToString(),new DotNetCoreBuildSettings() {
      Framework = framework,
      Configuration = configuration
    });
});

///////////////////////////////////////////////////////////////////////////////
// Test
///////////////////////////////////////////////////////////////////////////////
Task("Test")
  .IsDependentOn("Build")
  .Does(() =>
{
    var projectFiles = GetFiles("./tests/**/*.csproj");
    foreach(var file in projectFiles)
    {
		Information("Discovering Tests in " + file.FullPath);
        DotNetCoreTest(file.FullPath);
    }
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
         
       } else {     
         settings.MSBuildSettings = new DotNetCoreMSBuildSettings().SetVersionPrefix(tag.Name);
       }
   }
	
    DotNetCorePack(
                project.ToString(),
                settings
              );
});

///////////////////////////////////////////////////////////////////////////////
// Release
///////////////////////////////////////////////////////////////////////////////
Task("GetCredentials")
    .Does(() =>
{
    username = EnvironmentVariable("GITHUB_USERNAME");
    password = EnvironmentVariable("GITHUB_PASSWORD");
});

Task("Release")
  .WithCriteria(() => isAppVeyor && BuildSystem.AppVeyor.Environment.Repository.Tag.IsTag)
  .IsDependentOn("Pack")
  .IsDependentOn("GetCredentials")
  .Does(() => {
    var version = BuildSystem.AppVeyor.Environment.Repository.Tag.Name;
     GitReleaseManagerCreate(username, password, owner, repository, new GitReleaseManagerCreateSettings {
            Milestone         = version,
            Name              = version,
            Prerelease        = false,
            TargetCommitish   = "master"
    });
          
    var nugetFiles = string.Join(";", GetFiles("./artifacts/**/*.nupkg").Select(f => f.FullPath) );

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
  .IsDependentOn("Pack")
  .IsDependentOn("Release")
  .Does(() =>
{
  
});

RunTarget(target);
