FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["StudySharp.Auth/StudySharp.Auth.csproj", "StudySharp.Auth/"]
RUN dotnet restore "StudySharp.Auth/StudySharp.Auth.csproj"
COPY . .
WORKDIR "/src/StudySharp.Auth"
RUN dotnet build "StudySharp.Auth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudySharp.Auth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudySharp.Auth.dll"]
