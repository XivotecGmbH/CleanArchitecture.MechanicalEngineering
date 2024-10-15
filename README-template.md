# A Clean Architecture Mechanical Engineering .NET Solution Template

The project was generated using the [Xivotec.CleanArchitecture.MechanicalEngineering.Template](https://github.com/XivotecGmbH/CleanArchitecture.MechanicalEngineering) version 3.1

## Build

Run `dotnet build` to build the solution

## Run
Because .NET MAUI is packaged by default, `dotnet run` won't work.  
Instead, open the solution in Visual Studio and run from there.
You may need to run `dotnet workload update` via the command line or a rebuild action in Visual Studio if build errors occur.

## Test

The template contains all unit tests.


## Help
To learn more about the template go to the [project website](https://github.com/XivotecGmbH/CleanArchitecture.MechanicalEngineering).  
Here you can find additional guidance, request new features, report a bug, and discuss the template with other users.

## Known Error

If an error with the message, that a specified AndroidManifest file can not be found, occurs, copy the file AndroidManifest.xml from the location `\src\<YourProjectName>.Presentation.Maui\Platforms\Android\` to the location `\src\<YourProjectName>.Presentation.Maui\`. You can remove the copied file after building.