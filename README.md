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
Using this template, you can easily create a multi platform app for communicating with multiple hardware devices, while adhering to the core principles of Clean Architecture.

## Getting Started
The easiest way to get started with this template is to install the [NuGet package][nuget-d-url]

## Prerequisites
- Install the latest .NET 8.x SDK & Tools
- Install the latest version of Visual Studio IDE
- Install the latest .NET MAUI package
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

Because .NET MAUI is packaged by default, `dotnet run` won't work.  
Instead, open your solution in Visual Studio directly and run it from there.
You may need to run `dotnet workload update` via the command line or a rebuild action in Visual Studio if build errors occur.


## Database

### PostgreSQL

The template is configured to use PostgreSQL as a relational database provider by default. If you want to use another provider, you need to exchange `.RegisterPostgreSqlPortServices()`
in the `Presentation` project `MauiProgram.cs` file and the `Infrastructure.PostgreSQLPort` project itself with a corresponding implementation.

The database connection string is set in the `appsettings.json` file in the `Presentation` project.
To start the database with default settings, run:

```bash
docker run -e POSTGRES_PASSWORD=postgres -e POSTGRES_USER=postgres -p 5432:5432 postgres:latest
```

Once you run the application, the database will be created automatically (if necessary) and the latest migrations will be applied.

### InfluxDB

The template uses the InfluxDB time series database by default. The implementation can be changed just like the relational database.

The database connection properties are set in the `appsettings.json` file in the `Presentation` project.
To start the database with default settings, run:

```bash
docker run -d --publish 8086:8086 influxdb:2.7.4
```

Afterwards, complete the initial setup via the InfluxDB UI, which is available via the exposed container port. After completion, fill in a valid InfluxDB API Token for the `InfluxToken` property in the `appsettings.json` file in the `Presentation` project. Be sure that the organization name in your InfluxDB setup matches the `InfluxOrg` property provided in the `appsettings.json` in the `Presentation` project.

Once you run the application, the required buckets will be created automatically (if necessary).

## License

This project is licensed with the [MIT license](LICENSE).

## Support

If you have any problem, please let us know by raising a new issue.

If you have suggestions on how to improve or extend the template, let us know via email.  
Our homepage is linked in the banners at the top.

## Known Error

If an error with the message, that a specified AndroidManifest file can not be found, occurs, copy the file AndroidManifest.xml from the location `\src\<YourProjectName>.Presentation.Maui\Platforms\Android\` to the location `\src\<YourProjectName>.Presentation.Maui\`. You can remove the copied file after building.

## Technologies Used

Main technologies:
* [.NET MAUI](https://learn.microsoft.com/en-us/dotnet/maui/)
* [CommunityToolkit.Maui](https://github.com/CommunityToolkit/Maui)
* [CommunityToolkit.Mvvm](https://learn.microsoft.com/de-de/dotnet/communitytoolkit/mvvm/)
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
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
