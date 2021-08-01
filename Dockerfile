FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY ["StudySharp.Student/StudySharp.Student.csproj", "./StudySharp.Student/"]
COPY ["StudySharp.Teacher/StudySharp.Teacher.csproj", "./StudySharp.Teacher/"]
COPY ["StudySharp.Auth/StudySharp.Auth.csproj", "./StudySharp.Auth/"]
RUN dotnet restore

COPY "StudySharp.Student/." "./StudySharp.Student/"
COPY "StudySharp.Teacher/." "./StudySharp.Teacher/"
COPY "StudySharp.Auth/." "./StudySharp.Auth/"

WORKDIR /app
RUN dotnet publish -c Release -o out
COPY --from=build /app/out ./
CMD ASPNETCORE_URLS=http://*:$PORT dotnet StudySharp.Auth.dll
