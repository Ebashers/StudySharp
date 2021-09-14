FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["StudySharp.API/StudySharp.API.csproj", "StudySharp.API/"]
RUN dotnet restore "StudySharp.API/StudySharp.API.csproj"
COPY . .
WORKDIR "/src/StudySharp.API"
RUN dotnet build "StudySharp.API.csproj" -c Development -o /app/build

FROM build AS publish
RUN dotnet publish "StudySharp.API.csproj" -c Development -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet StudySharp.API.dll
