// include Fake libs
#r "./packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.Testing

RestorePackages()

// Directories
let buildDir  = "./build/"
let deployDir = "./deploy/"
let testDir = "./build/"


// Filesets
let appReferences  =
    !! "/**/*.csproj"
    ++ "/**/*.fsproj"

// version info
let version = "0.1"  // or retrieve from CI server

// Targets
Target "Clean" (fun _ ->
    CleanDirs [buildDir; deployDir]
)

Target "Build" (fun _ ->
    // compile all projects below src/app/
    MSBuildDebug buildDir "Build" appReferences
    |> Log "AppBuild-Output: "
)

Target "Deploy" (fun _ ->
    !! (buildDir + "/**/*.*")
    -- "*.zip"
    |> Zip buildDir (deployDir + "ApplicationName." + version + ".zip")
)
let tests = !! (testDir + "*.Tests.dll")
let nunitRunnerPath = "packages/NUnit.ConsoleRunner/tools/nunit3-console.exe"

Target "NUnitTest" (fun _ -> 
    tests
    |> NUnit3 (fun p ->  
        { p with ToolPath = nunitRunnerPath })
)

// Build order
"Clean"
  ==> "Build"
  ==> "NUnitTest"
  ==> "Deploy"

// start build
RunTargetOrDefault "Build"
