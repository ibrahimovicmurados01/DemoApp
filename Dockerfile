FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY ./DemoApp.Entities/*.csproj ./DemoApp.Entities/
COPY ./DemoApp.Contracts/*.csproj ./DemoApp.Contracts/
COPY ./DemoApp.Repository/*.csproj ./DemoApp.Repository/
COPY ./DemoApp.Web/*.csproj ./DemoApp.Web/
COPY *.sln .
RUN dotnet Restore
COPY . .
RUN dotnet publish ./DemoApp.Web/*.csproj -o /publish/
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS publish
WORKDIR /app
COPY --from=build /publish .
ENV ASPNETCORE_URLS="http://*:5000"
ENTRYPOINT [ "dotnet","DemoApp.Web.dll" ]
