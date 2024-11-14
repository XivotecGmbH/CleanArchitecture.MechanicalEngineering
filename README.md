<!-- Project Shields & URLs -->
[github_build-shield]: https://img.shields.io/badge/
[license-shield]: https://img.shields.io/github/license/XivotecGmbH/CleanArchitecture.MechanicalEngineering
[license-url]: https://github.com/XivotecGmbH/CleanArchitecture.MechanicalEngineering/blob/master/LICENSE
[contributors-url]: https://github.com/XivotecGmbH/CleanArchitecture.MechanicalEngineering/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/XivotecGmbH/CleanArchitecture.MechanicalEngineering
[forks-url]: https://github.com/XivotecGmbH/CleanArchitecture.MechanicalEngineering/network/members
[issues-shield]: https://img.shields.io/github/issues/XivotecGmbH/CleanArchitecture.MechanicalEngineering
[issues-url]: https://github.com/XivotecGmbH/CleanArchitecture.MechanicalEngineering/issues

[nuget-shield]: https://img.shields.io/nuget/v/XivoBlue.CleanArchitecture.MechanicalEngineering.Template?label=NuGet
[nuget-url]: https://www.nuget.org/packages/XivoBlue.CleanArchitecture.MechanicalEngineering.Template
[nuget-d-shield]: https://img.shields.io/nuget/dt/XivoBlue.CleanArchitecture.MechanicalEngineering.Template?label=Downloads
[nuget-d-url]: https://www.nuget.org/packages/XivoBlue.CleanArchitecture.MechanicalEngineering.Template

[website-shield]: https://img.shields.io/badge/Xivotec-blue
[website-url]: https://xivotec.com/
[instagram-shield]: https://img.shields.io/badge/Xivotec-blue?logo=instagram&logoColor=white
[instagram-url]: https://www.instagram.com/xivotec
[linkedin-shield]: https://img.shields.io/badge/Xivotec-blue?logo=linkedin&logoColor=white
[linkedin-url]: https://de.linkedin.com/company/xivotec

# A Clean Architecture .NET Mechanical Engineering Template
[![License][license-shield]][license-url] [![Forks][forks-shield]][forks-url] [![Issues][issues-shield]][issues-url]

[![NugetLink][nuget-shield]][nuget-url][![NugetDownloads][nuget-d-shield]][nuget-d-url]

[![Website][website-shield]][website-url] [![Instagram][instagram-shield]][instagram-url] [![LinkedIn][linkedin-shield]][linkedin-url]

The goal of this template is to provide a straightforward and efficient approach for application development in the area of mechanical engineering, leveraging the power of Clean Architecture.
Using this template, you can easily create a multi-platform app for communicating with multiple hardware devices, while adhering to the core principles of Clean Architecture.

## Getting Started
The easiest way to get started with this template is to install the [NuGet package][nuget-d-url]

## Prerequisites
- Install the latest .NET 8.x SDK & Tools
- Install the latest version of Visual Studio IDE
- Install the latest .NET MAUI package
- Install Node.js
- Install / have access to a PostgreSQL database (optional, see below)
- Enable Developer Mode on your device (required for debugging .NET MAUI applications)

## Installation
[1] Open the command prompt and run:

```bash
dotnet new install XivoBlue.CleanArchitecture.MechanicalEngineering.Template
```

[2] Once installed, create a new solution in your project folder or from Visual Studio :

```bash
dotnet new xt-came-sln -n <YourProjectName>
```

[3] Choose your preferred startup project:

This template includes both a .NET MAUI and an Angular Frontend.

[3a] .NET MAUI
- With .NET MAUI, `dotnet run` won't work.  
Instead, open your solution in Visual Studio directly and run the `<YourProjectName>.Presentation.Maui` project from there.

[3b] Angular
- To start the application using the Angular frontend, launch the `<YourProjectName>.Presentation.Server` project.
It uses the default port 4200. You can reach the UI in a browser of your choice at https://localhost:4200.
You may need to run `npm install` inside the Angular project if any errors occur during startup.

If you encounter any build errors, you may need to run `dotnet workload update` via the command line or a rebuild action in Visual Studio.

## Database

### PostgreSQL

The template is configured to use PostgreSQL as a relational database provider by default. If you want to use another provider, you need to exchange `.RegisterPostgreSqlPortServices()`
in the startup project and the `Infrastructure.PostgreSQLPort` project itself with a corresponding implementation.

The database connection string is set in the `appsettings.json` file of the startup project.
To start the database with default settings, run:

```bash
docker run -e POSTGRES_PASSWORD=postgres -e POSTGRES_USER=postgres -p 5432:5432 postgres:latest
```

Once the application runs, the database will be created automatically if needed, and the latest migrations will be applied.

### InfluxDB

By default, this template uses the InfluxDB time series database. The implementation can be changed just like the relational database.

The database connection properties are set in the `appsettings.json` file in the startup project.
To start the database with default settings, run:

```bash
docker run -d --publish 8086:8086 influxdb:2.7.4
```

Complete the initial setup via the InfluxDB UI, which is available via the exposed container port. After completion, enter a valid InfluxDB API Token for the `InfluxToken` property in the `appsettings.json` file in the `Presentation` project. Be sure that the organization name in your InfluxDB setup matches the `InfluxOrg` property provided in the `appsettings.json` file in the `Presentation` project.

Once you run the application, the required buckets will be created automatically if needed.

## License

This project is licensed with the [MIT license](LICENSE).

## Support

If you have any problems, please let us know by raising a new issue.

If you have suggestions for improvements or extensions, let us know via email.  
Our homepage is linked in the banners at the top.

## Known Errors

### MAUI startup
If you encounter an error when starting .Net MAUI stating that the specified AndroidManifest file cannot be found, copy the file AndroidManifest.xml from `\src\<YourProjectName>.Presentation.Maui\Platforms\Android\` to `\src\<YourProjectName>.Presentation.Maui\`. You may delete the copied file after building.

## Technologies Used

Main technologies:
* [.NET MAUI](https://learn.microsoft.com/en-us/dotnet/maui/)
* [Angular](https://angular.dev/)
* [CommunityToolkit.Maui](https://github.com/CommunityToolkit/Maui)
* [CommunityToolkit.Mvvm](https://learn.microsoft.com/de-de/dotnet/communitytoolkit/mvvm/)
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
* [InfluxDB](https://www.influxdata.com/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)

Unit Tests:  
* [XUnit](https://xunit.net/)
* [FluentAssertions](https://fluentassertions.com/)
* [NSubstitute](https://nsubstitute.github.io/)

## Learn More

* [.NET MAUI](https://learn.microsoft.com/en-us/dotnet/maui/)
* [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
