# A Clean Architecture Mechanical Engineering .NET Solution Template

The project was generated using the [Xivotec.CleanArchitecture.MechanicalEngineering.Template](https://github.com/XivotecGmbH/CleanArchitecture.MechanicalEngineering) version 3.0.2

## Build

Run `dotnet build` to build the solution.

## Run
This template includes both a .NET MAUI and an Angular Frontend.

[1] .NET MAUI
- With .NET MAUI, `dotnet run` won't work.  
  Instead, open your solution in Visual Studio directly and run the `<YourProjectName>.Presentation.Maui` project from there.

[2] Angular
- To start the application using the Angular frontend, launch the `<YourProjectName>.Presentation.Server` project.
  It uses the default port 4200. You can reach the UI in a browser of your choice at https://localhost:4200.
  You may need to run `npm install` inside the Angular project if any errors occur during startup.

If you encounter any build errors, you may need to run `dotnet workload update` via the command line or a rebuild action in Visual Studio.

## Test

The template contains all unit tests.

## Help
To learn more about the template go to the [project website](https://github.com/XivotecGmbH/CleanArchitecture.MechanicalEngineering).  
Here you can find additional guidance, request new features, report a bug, and discuss the template with other users.

## Known Errors

### MAUI startup
If you encounter an error when starting .Net MAUI stating that the specified AndroidManifest file cannot be found, copy the file AndroidManifest.xml from `\src\<YourProjectName>.Presentation.Maui\Platforms\Android\` to `\src\<YourProjectName>.Presentation.Maui\`. You may delete the copied file after building.