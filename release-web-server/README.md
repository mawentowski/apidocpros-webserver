# Release Web Server

## What is it?

A release build of the associated Angular SPA deposits build artifacts (e.g., JavaScript files, stylesheets,
images, other assets, etc.) in the default web root (wwwroot) of this .NET Core web server project.
In publishing the SPA, those builds artifacts are wrapped with the build artifacts of this web server
project, which are in turn copied to a web host directory associated with a root URL
(e.g., "https://nucwebapp19dev.duke-energy.com/NucRadExpLetters"). HTTP requests for the build artifacts
of the SPA targetting variants of that URL are fielded by an instance of this .NET Core application, which
locates the requested files and serves them to the requester with appropriate cache control headers.

## Why is it intended for release only?

While a local build of this web server may be used for development and testing of the Angular SPA, 
doing so requires creating a release build of the SPA, but with an empty base path (i.e., this
Angular CLI command must be executed: `ng b --configuration=release --base-href='/'`). Also, this
web server applies cache control headers to its HTTP responses that are inappropriate for
development (e.g., "max-age," "immutable," etc.). Also, initiating the Webpack Dev Server with the
`ng serve` command of the Angular CLI is simpler and faster than building and running this project.
The Webpack Dev Server also features live reloading functionality and appends "no-cache" cache
control headers to it HTTP responses, which are ideal for development.

The reasons stated above and the fact that .NET Core solutions feature thoroughly vetted security
measures are why this web server is recommended for release and release only.


[.NET Core Web Server Code Samples](https://github.com/dotnet/AspNetCore.Docs/tree/main/aspnetcore/fundamentals/static-files/samples/3.x/StaticFilesSample)